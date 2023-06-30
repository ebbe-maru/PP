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
    }

    static void testA(){
        WriteLine("Part A:");
        nsteps = 0;
        vector guess = new vector(1);
        guess[0] = 20;
        vector res1 = qnewton((vector x) => 100*(x[0]*x[0]-x[0])+6, guess);
		WriteLine($"Minimum of 100*(x^2-x)+6: \nShould be at: x = 0.5 \nFound at: x = {res1[0]} in {nsteps} steps. (Guess at 20.0) \n");

        WriteLine("Mimimum of the Rosenbroks Valley function: f(x,y)=(1-x)^2+100(y-x^2)^2:");
        WriteLine("Should be at: x = 1 and y = 1");
        nsteps = 0;
        vector res2 = qnewton(rosenbrocks, new vector(2,2));
        WriteLine($"Found at: x = {res2[0]} and y = {res2[1]} in {nsteps} steps. (Guess at (2, 2)) \n");

        WriteLine("Mimimum of the Himmelblaus function: f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2");
        WriteLine("One mimimum is at: x = 3 and y = 2");
        nsteps = 0;
        vector res3 = qnewton(himmelblau, new vector(3.5,2.5));
        WriteLine($"Mimimum found at: x = {res3[0]} and y = {res3[1]} in {nsteps} steps. (Quess at (3.5, 2.5))");
        WriteLine("Another mimimum can be found using different guess.");

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
        WriteLine("Part B");
        WriteLine("Fitting Breit-Wigner function to the higgs data. Using guesses A=16, m=126, gamma=2:");
		vector guesses = new vector(new double[3]{16,126,2}); 
        nsteps = 0;
        vector param = fit(breitWigner, guesses, energy, signal, error);
        WriteLine($"Fit concluded with parameters A = {param[0]}, m = {param[1]}, Γ = {param[2]}");
        WriteLine($"It arrived at these parameters in {nsteps} steps");

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
        return Pow(1-x[0],2)+100*Pow(x[1]- x[0]*x[0],2);
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
        B.set_identity();

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
}
