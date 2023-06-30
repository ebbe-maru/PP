using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.IO;
using System;
using static System.Console;
using static System.Math;

class main{
    public static void Main(){
        vector x = new vector(new double[] {1, 2, 3, 4, 6, 9, 10, 13, 15});
        vector y = new vector(new double[] {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1});
        vector dy = new vector(new double[] {5, 5, 5, 4, 4, 3, 3, 2, 2});
        int n = x.size;
        // Make logy and dlogy vectors
        vector logy = new vector(n);
        for (int i = 0; i<n; i++) logy[i] = Log(y[i]);

        vector logdy = new vector(n);
        for (int i = 0; i<n; i++) logdy[i] = dy[i]/y[i];

        // Find fit coefficients
        Func<double,double>[] func = new Func<double, double>[] {z => 1.0, z => z};
        (vector c, matrix S) = lsfit(func, x, logy, logdy);
        
        WriteLine("Best fit:");
        WriteLine($"y(t) = {Exp(c[0])}*exp({c[1]}*t) \n");

        WriteLine("The halflife:");
        WriteLine($"{-Log(2)/c[1]} days, the known value of 224Ra: 3.631(2) days \"Bé et al., 2004; DDEP, 2018\" \n");

        S.print("The covarience matrix:");
        WriteLine("\nThe uncertainties of the fitting coefficients:");
        WriteLine($"a = {c[0]} ± {Sqrt(S[0,0])}");
        WriteLine($"lambda = {c[1]} ± {Sqrt(S[1,1])}");
        WriteLine($"The halflife with uncertainty: {-Log(2)/c[1]} ± {Abs((Sqrt(S[1,1])/c[1])*(-Log(2)/(c[1])))} days");
        WriteLine($"Within uncertainty it agrees with the modern value.");


        // Output data to file
        var outfile = new StreamWriter("decay.data");
        for (int i = 0; i<n; i++){
            outfile.WriteLine($"{x[i]}  {y[i]}  {logy[i]}   {dy[i]}");
        }
        outfile.Close();

        // Output fit to file
        vector xs = new vector(150);
        for (int i = 0; i<xs.size; i++){
            xs[i] = (double)i*2/16;
        }
        vector fitdata = fitfunc(c, xs);
        vector cminus = new vector(new double[] {c[0]-S[0,0], c[1]-S[1,1]});
        vector cplus = new vector(new double[] {c[0]+S[0,0], c[1]+S[1,1]});
        vector fitdataminus = fitfunc(cminus, xs);
        vector fitdataplus = fitfunc(cplus, xs);
        outfile = new StreamWriter("fit.data");
        for (int i = 0; i<xs.size; i++){
            outfile.WriteLine($"{xs[i]}  {fitdata[i]}   {fitdataminus[i]}   {fitdataplus[i]}");
        }
        outfile.Close();
    }
    
    static vector fitfunc(vector a, vector x){
        vector y = new vector(x.size);
        for (int i = 0; i<x.size; i++){
            y[i] = Exp(a[0]) * Exp(x[i]*a[1]);
        }
        return y;
    }
    
    static (vector, matrix) lsfit(Func<double,double>[] func, vector x, vector y, vector dy){
        int n = x.size;
        int m = func.Length;
        matrix A = new matrix(n,m);
        matrix R = new matrix(m,m);
        vector b = new vector(n);
        // Fill out A matrix
        for(int i=0; i<n; i++){
            b[i] = y[i] / dy[i];
            for (int k = 0; k<m; k++) {
                A[i,k] = func[k](x[i])/dy[i];
            }
        }
        // QR decomp A matrix
        matrix Q = A.copy();
        decomp(ref Q, ref R);
        vector c = solve(Q,R,b);
        matrix cov = inverse(Q.T * Q,R);
        return (c, cov);
    }


    public static void decomp(ref matrix A, ref matrix R){
        int m = A.size2;
        for(int i=0; i<m; i++){
            // Normalise the i-th column of A and set the j-th diagonal element of R
            R[i,i] = A[i].norm();
            A[i]/=R[i,i];

            // Orthogonalize the remaining columns of A against the i-th column
            for(int j = i+1; j<m; j ++){
                R[i,j] = A[i].dot(A[j]);
                A[j] -= A[i] * R[i,j]; 
            }
        }
    }

    public static vector solve(matrix Q, matrix R, vector b){
		// O(n^2)
		vector x = Q.T * b;  // Apply Q.T to b
		// Back-substitution on x. 
        
		for (int i = x.size - 1; i >= 0; i--){
			double sum = 0;
			for (int j = i + 1; j < x.size; j++){
				sum += R[i, j] * x[j];
			}
			x[i] = (x[i] - sum) * 1/R[i,i];
            
    	}
    	return x;
	}
    public static matrix inverse(matrix Q, matrix R){
		// O(n^3)
		matrix B = new matrix(R.size1, R.size2);
		for (int i=0; i<Q.size2; i++){
            vector ei = new vector(Q.size1); // unit vectors
            ei[i] = 1;
			B[i] = solve(Q,R,ei);
		}
		return B;
	}
    
}