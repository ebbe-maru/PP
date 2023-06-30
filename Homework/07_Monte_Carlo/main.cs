using static System.Math;
using static System.Console;
using static System.Double;
using System.Diagnostics;
using System.IO;
using System;
class main{
    public static void Main(){
        testA();
        testB();

        WriteLine("In error.svg the errors for pseudo and quasi integration of x^2*sin(Theta) from 0<=x<=1 and 0<=theta<=2PI.");
        WriteLine("The same can be seen for the area of a unit circle in UnitError.svg.");
        WriteLine("In UnitCircle.svg the calculated area of a unit circle are compared using psuedo and quasi-random monte-carlo integrator.");
    }

    static void testA(){
        WriteLine("Testing plain Monte Carlo integration:");
        // First index in vector is the starting point for the variables
        // Second index in vector is the end point for the variables
        vector a = new vector(0, 0);  
        vector b = new vector(1, PI);
        (double result, double error) = plainmc(x => x[0]*x[0]*Sin(x[1]), a, b, 1000);
        WriteLine($"Integral over r^2sin(theta) with r from 0 to 1 and theta from 0 to 2pi is calculated as: {result} +- {error}" );
		WriteLine($"It should be 1/(2pi) * 4/3 * pi * 1^3 = 2/3 = {2.0/3.0}");
        WriteLine();

        a = new vector(0, 0, 0);  
        b = new vector(PI, PI, PI);
        (result, error) = plainmc(x => 1/(PI*PI*PI) * 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2])), a, b, 10000);
        WriteLine($"Integral over 1-(cos(x)*cos(y)*cos(z))^-1 with x, y and z from 0 to PI is calculated as: {result} +- {error}" );
		WriteLine($"It should be Gamma(1/4)^4 / (5*PI^3) = 1.3932039296856768591842462603255");

        Unitcircle();
    }

    static void Unitcircle()
	{
		vector a = new vector(0,0);
		vector b = new vector(1,2*PI);
		Func<vector,double> UnitCircle = x => x[0]; // area of circle is integral of r dr d(theta), so the function is just r (= x[0])
		int resolution = 60;
		double  min = 1;
		double  max = 1e5;
		double[] realError = new double[resolution];
		int[] Ns = new int[resolution];
		var unitC = new StreamWriter("unitC.data");
		{
			for(int i=0;i<resolution;i++)
			{
				double n = Log10(min) + (Log10(max)-Log10(min))/(resolution-1)*i;
				int N = (int)Pow(10,n);
				Ns[i] = N;
				(double integral, double error) = plainmc(UnitCircle,a,b,N);
				(double qIntegral, double qError) = quasimc(UnitCircle,a,b,N);
				realError[i] = Abs(PI - integral);
				unitC.WriteLine($"{n} {N} {integral} {error} {realError[i]} {1/Sqrt(N)} {qIntegral} {qError}"); 
			}
		}	
        unitC.Close();
    }


    static void testB(){
        WriteLine();
        WriteLine("Testing Quasi Monte Carlo integration");

        // Data for error plot
        var errordata = new StreamWriter("error.data");
        double resultquasi = 0;
        double errorquasi = 0;
        int fieldWidth = 20;
        vector a = new vector(0, 0);  
        vector b = new vector(1, PI);
        for (int i = 10; i<10000; i+=2){
            (double result, double error) = plainmc(x => x[0]*x[0]*Sin(x[1]), a, b, i);
            (resultquasi, errorquasi) = quasimc(x => x[0]*x[0]*Sin(x[1]), a, b, i);
            double sqrnN = 1/Sqrt(i);
            errordata.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}{3,-" + fieldWidth + "}", i, error, errorquasi, sqrnN));
        }
        errordata.Close();

    }
    static (double,double) plainmc(Func<vector,double> f, vector a, vector b, int N){
        int dim = a.size; 
        double V = 1; 
        for(int i=0; i<dim; i++){
            V *= b[i]-a[i];
        }
        double sum = 0;
        double sum2 = 0;
        var x = new vector(dim);
        var rnd = new Random();
        for(int i=0; i<N; i++){
            for(int k=0; k<dim; k++) {
                x[k] = a[k]+rnd.NextDouble()*(b[k]-a[k]);
            }
            double fx = f(x); 
            sum += fx; 
            sum2 += fx*fx;
        }
        double mean = sum/N;
        double sigma = Sqrt(sum2/N-mean*mean);
        var result = (mean*V, sigma*V/Sqrt(N));
        return result;
    }

    static (double, double) quasimc(Func<vector,double> f, vector a, vector b, int N){
        int dim = a.size; 
        double V = 1; 
        for(int i=0; i<dim; i++){
            V *= b[i]-a[i];
        }
        double sum = 0;
        double sum2 = 0;
        var x = new vector(dim);
        var x2 = new vector(dim);
        for(int i=0; i<N; i++){
            vector halton1 = halton(i, dim);
            vector halton2 = halton(i, dim+1);
            for(int k=0; k<dim; k++) {
                x[k] = a[k] + halton1[k]*(b[k]-a[k]);
            }
            for(int k=0; k<dim; k++) {
                x2[k] = a[k] + halton2[k]*(b[k]-a[k]);
            }
            sum += f(x); 
            sum2 += f(x2);
        }
        double mean = sum/N;
        double sigma = Sqrt(sum2/N-mean*mean);
        var result = (mean*V, sigma*V/Sqrt(N));
        return result;
    }

    static double corput(int n, int b){ 
        double q=0;
        double bk=(double)1/b; 
        while(n>0){ 
            q += (n % b)*bk;
            n /= b; 
            bk /= b; 
        } 
        return q; 
    }

    static vector halton(int n, int d){ 
        int[] baseVals = new int[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61};
        int maxd = baseVals.Length; 
        if (d > maxd) throw new ArgumentException("To many dimensions");
        vector xs = new vector(d);
        for(int i=0;i<d; i++){
            xs[i]=corput(n, baseVals[i]); 
        } 
        return xs;
    }
}