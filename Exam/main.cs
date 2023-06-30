using static System.Math;
using static System.Console;
using static System.Double;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System;
class main{
    static double nsteps;
    public static void Main(){
        nsteps = 0;
        testA();
        testB();

        WriteLine("If we compare the result from homework 09 (Quasi-Newton method) with the");
        WriteLine("result gotten using this modified version, they look quit similar with no significant difference in preccision.");
        WriteLine("However, the Quasi-Newton method takes fewer steps for the 2 function,");
        WriteLine("and the modified takes fewer steps for fitting.");
    }

    static void testA(){
        WriteLine("Implementation of the (modified) Newton's minimization method \nwhere the Hessian matrix is computed numerically using a finite-difference approximation.");
        WriteLine("Equation 48 from the chapter has been used.\n");
        vector guess = new vector(1);
        guess[0] = 10;
        nsteps = 0;
        vector res1 = numNewton((vector x) => 100*(x[0]*x[0]-x[0])+6, guess);
		
        WriteLine($"Minimum of 100*(x^2-x)+6: \nShould be at: x = 0.5 \nFound at: x = {res1[0]} in {nsteps} steps. (Guess at 20)");

        nsteps = 0;
        vector qres1 = qnewton((vector x) => 100*(x[0]*x[0]-x[0])+6, guess);
        WriteLine($"Quasi-Newton method: x = {qres1[0]} in {nsteps} steps.\n");
         
        
        WriteLine("Mimimum of the Himmelblaus function: f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2");
        WriteLine("One mimimum is at: x = 3 and y = 2");
        nsteps = 0;
        vector res2 = numNewton(himmelblau, new vector(3.5,2.5));
        WriteLine($"Mimimum found at: x = {res2[0]} and y = {res2[1]} in {nsteps} steps. (Quess at (3.5, 2.5))");
        WriteLine("Another mimimum can be found using different guess.");
        
        nsteps = 0;
        vector qres2 = qnewton(himmelblau, new vector(3.5,2.5));
        WriteLine($"Quasi-Newton method: x = {qres2[0]} and y = {qres2[1]} in {nsteps} steps. \n");
    }

    static void testB(){
        var energy = new List<double>();
        var signal = new List<double>();
        var error  = new List<double>();
        var separators = new char[] {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        do{
                string line=Console.In.ReadLine();
                if(line==null)break;
                string[] words=line.Split(separators,options);
                energy.Add(double.Parse(words[0]));
                signal.Add(double.Parse(words[1]));
                error .Add(double.Parse(words[2]));
        }while(true);
        
        WriteLine();
        WriteLine("Fitting Breit-Wigner function to the higgs data. Using guesses A=16, m=126, gamma=2:");
		vector guesses = new vector(new double[3]{16,126,2}); 
        nsteps = 0;
        vector param = numFit(breitWigner, guesses, energy, signal, error);
        WriteLine($"Fit concluded with parameters A = {param[0]}, m = {param[1]}, Γ = {param[2]}");
        WriteLine($"Calculated in {nsteps} steps.");
    

        nsteps = 0;
        vector qparam = fit(breitWigner, guesses, energy, signal, error);

        WriteLine($"Quasi-Newton method: A = {qparam[0]}, m = {qparam[1]}, Γ = {qparam[2]}");
        WriteLine($"Calculated in {nsteps} steps. \n");

        var fitData = new StreamWriter("fit.data");
        double fieldWidth = 20;
		for(int i = 0; i < 1000; i++){
            double x = energy[0] + (energy[energy.Count-1] - energy[0])*i/1000.0;
            vector par = new vector(new double[4]{x,param[0],param[1],param[2]});
            double y = breitWigner(par);
            fitData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}", x, y));
		}
        fitData.Close();
        WriteLine("The Fit is plotted in Higgs.svg.");
    }

    public static double breitWigner(vector par){
		double x = par[0];
		double A = par[1];
		double xm = par[2]; 
		double d = par [3];
		return A/( (x-xm)*(x-xm) + d*d/4  );
	}

    static double rosenbrocks(vector x){
        return (Pow(1-x[0],2)+100*Pow(x[1]- x[0]*x[0],2));
    }

    public static double himmelblau(vector x){
		return Pow(x[0]*x[0]+x[1]-11,2)+Pow(x[0]+x[1]*x[1]-7,2);
	}

    public static vector qnewton(Func<vector,double> f, vector start, double acc = 1e-4){
        int n = start.size;
        vector x = start.copy();
        vector grad = new vector(n);
        vector deltax = new vector(n);
        vector s = new vector(n);
        vector u = new vector(n);
        vector y = new vector(n);
        matrix deltaB = new matrix(n,n);
        matrix B = new matrix(n,n);

        double lambda = 1.0;
        grad = gradient(f, x);

        while(grad.norm() > acc){
            nsteps++;
            deltax = -B*grad;
            lambda = 1.0;
            while(true){
                s = lambda*deltax;
                if (f(x+s) < f(x)){ // accept step and update B
                    x = x + s;
                    vector oldGrad = grad;
                    grad = gradient(f, x);
                    // Update B
                    y = grad - oldGrad;
                    u = s - B*y;
                    // deltaB * y = u  =>  deltaB * y * u = u * u 
                    deltaB = matrix.outer(u,u)/(u.dot(y));
                    B += deltaB;
                    break;
                }
                lambda = lambda/2;
                if (lambda < 1.0/Pow(2,16)){ // accept step and reset B
                    x = x + s;
                    grad = gradient(f, x);
                    B.set_identity();
                    break;
                }
            }
        }
        return x;
    }



    public static vector numNewton(Func<vector,double> f, vector start, double acc = 1e-4){
        int n = start.size;
        vector x = start.copy();
        vector grad = new vector(n);
        vector deltax = new vector(n);
        vector s = new vector(n);
        double alpha = 0;
        matrix B = new matrix(n,n);
        matrix H = new matrix(n, n);

        // matrix zero = new matrix(n,n);
        // zero.set_zero();

        double lambda = 1.0;
        grad = gradient(f, x);

        while(grad.norm() > acc){
            nsteps++;
            //WriteLine($"step = {nsteps}");
            //x.print();
            H = Hessian(f, x);
            (matrix Q, matrix R) = decomp(H);
            //H.print();
            B = inverse(Q, R);
            deltax = -B*grad;
            lambda = 1.0;
            while(true) {
                s = lambda*deltax;
                if (f(x+s) < f(x) + alpha*s.dot(grad)){ // accept step and update B
                    x = x + s;
                    grad = gradient(f, x);
                    break;
                }
                lambda = lambda/2;
                if (lambda < 1.0/Pow(2,16)){ // accept step and reset B
                    x = x + s;
                    grad = gradient(f, x);
                    break;
                }
            }    
        }
        return x;
    }

     public static matrix Hessian(Func<vector,double> f, vector x) {
        double eps = Pow(2, -26);
        int n = x.size;
        vector deltax = new vector(n);
        vector c = new vector(n);

        for(int i = 0; i < n; i++) {
            deltax[i] = Abs(x[i])*eps;
            c[i] = 0;
        }
        matrix H = new matrix(n, n);
        for(int i = 0; i < n; i++) {
            vector c_i = c.copy();
            c_i[i] = deltax[i];
            for(int j = 0; j < n; j++) {
                vector c_j = c.copy();
                c_j[j] = deltax[j];
                double divider = 4*c_i[i]*c_j[j];
                H[i,j] = (f(x + c_i + c_j) - f(x - c_i + c_j) - f(x + c_i - c_j) + f(x - c_i - c_j))/divider;
            }
        }
        return H;
    }

    public static vector fit(
        Func<vector,double> f,
        vector guesses,
        List<double> xs,
        List<double> ys,
        List<double> yerrs,
        double acc = 1e-4
    ){
        Func<vector,double> toMin = par => {
            double sum = 0;
            vector parvector = new vector(guesses.size+1);
            for(int i = 0; i < xs.Count; i++){
                parvector[0] = xs[i];
                for(int j = 0; j < guesses.size; j++){
                    parvector[j+1] = par[j];
                }
                sum += Pow( (f(parvector)-ys[i])/ yerrs[i] ,2);
            }
            return sum;
        };
        vector fitparams = qnewton(toMin,guesses,acc);
        return fitparams;
    }

    public static vector numFit(
        Func<vector,double> f,
        vector guesses,
        List<double> xs,
        List<double> ys,
        List<double> yerrs,
        double acc = 1e-4
    ){
        Func<vector,double> toMin = par => {
            double sum = 0;
            vector parvector = new vector(guesses.size+1);
            for(int i = 0; i < xs.Count; i++){
                parvector[0] = xs[i];
                for(int j = 0; j < guesses.size; j++){
                    parvector[j+1] = par[j];
                }
                sum += Pow( (f(parvector)-ys[i])/ yerrs[i] ,2);
            }
            return sum;
        };
        vector fitparams = numNewton(toMin,guesses,acc);
        return fitparams;
    }

    static vector gradient(Func<vector,double> f, vector x){
        int dim = x.size;
        vector grad = new vector(dim);
        vector newx = x.copy();
        for(int i = 0; i<dim; i++){
            double dx = Abs(x[i])*Pow(2,-26);
            newx[i] = x[i] + dx;
            grad[i] = (f(newx) - f(x))/dx;
        }
        return grad;
    }

     public static (matrix, matrix) decomp(matrix A){
        int m = A.size2;
        matrix Q=A.copy() , R=new matrix (m,m) ;
        for(int i=0; i<m; i++){
			// Normalise the i-th column of A and set the j-th diagonal element of R
            R[i,i] = Q[i].norm();
            Q[i]/=R[i,i];

			// Orthogonalize the remaining columns of A against the i-th column
            for(int j = i+1; j<m; j ++){
                R[i,j] = Q[i].dot(Q[j]);
                Q[j] -= Q[i] * R[i,j]; 
            }
        }
        return (Q,R);
    }


    public static vector solve(matrix Q, matrix R, vector b){
		int m = R.size1;
		vector x = Q.T * b; 

		for (int i = m - 1; i >= 0; i--){
			double sum = 0;
			for (int j = i + 1; j < m; j++){
				sum += R[i, j] * x[j];
			}
			x[i] = (x[i] - sum) / R[i,i];
    	}
    	return x;
	}

    public static double det(matrix R){
        int n = R.size1;
        double product = 1;
        for (int i = 0; i<n; i++) {
            product *= R[i,i];
        }
        return product;
    }

	public static matrix inverse(matrix Q, matrix R){
		matrix I = new matrix(Q.size1, Q.size2); // Make indentity matrix
        I.setid();
        matrix B = new matrix(Q.size1, Q.size2);
		for (int i=0; i<Q.size1; i++){
			B[i] = solve(Q,R,I[i]);
		}
		return B;
	}
}

