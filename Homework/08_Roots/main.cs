using static System.Math;
using static System.Console;
using static System.Double;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System;
class main{
    public static void Main(){
        testA();
        testB();
        
    }

    static void testA(){
        WriteLine("Test of Newton method of root finding:");
        WriteLine("A simple linear equation is tested: f(x)=x+2");
        WriteLine("The root should be: x = -2");
        vector x1 = newton((vector a1)=>(a1.copy()+ new vector(2.0)), new vector(1.0));
        WriteLine($"The root found: x = {x1[0]} \n");

        WriteLine("The next test is of: f(x,y)=(4x+3y+6, x^2)");
        WriteLine("The root should be: x = 0 and y = -2");
        vector x2 = newton((vector a2) => new vector(4*a2[0] + 3*a2[1] + 6, Pow(a2[0],2)), new vector(0.5,0.5));
        WriteLine($"The root found: x = {x2[0]} and y = {x2[1]} \n");

        WriteLine("The extremum of the Rosenbrock's Valley function: f(x,y) = (1-x)^2 + 100*(y-x^2)^2");
        WriteLine("Should be: (x=1, y=1)");
        vector x3 = newton(rosenbrocks, new vector(1.1,2));
        WriteLine($"The roots of the gradient found is: x = {x3[0]} and y = {x3[1]}");
    }

    static void testB(){
        WriteLine();
        WriteLine("Part B");
        Func<double, double> toMinimize = x => Me(x,0.001,8, 0.01, 0.01);
		double eval = newton(toMinimize,-1,1e-4);
        WriteLine($"With the shooting method, the binding energy of lowest bound S-electron in H is found to be {eval}. Expected value is -0.5");
        
        List<double> rs = new List<double>();
		List<vector> ys = new List<vector>();
		Me(eval,0.001,8, 0.01, 0.01, rs, ys);
		var waveData = new StreamWriter("wavefunction.data");
        double fieldWidth = 25;
		for(int i = 0; i < rs.Count; i++){
            waveData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", rs[i], ys[i][0], rs[i]*Exp(-rs[i])));
		}
        waveData.Close();
		WriteLine($"The corresponding wavefunction are plotted in Wavefunction.svg. It only differs from the analytical solution at the very end vere our solutions is defined to be 0.");

        WriteLine("How r_max, r_min, absolute accuracy and relative accuracy relate to the error are plotted in rmax.svg, rmin.svg, absacc.svg and epsacc.svg");
        
		var rminconvergenceData = new StreamWriter("rmin.data");
		for(int i = 0; i < 100; i++){
			double rmin = 0.001*(i+1);
			toMinimize = x => Me(x,rmin,8, 1e-4, 1e-4);
			eval = newton(toMinimize,-1,1e-4);
            rminconvergenceData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}", rmin, Abs(eval+0.5)/0.5));
		}
        rminconvergenceData.Close();

		var rmaxconvergenceData = new StreamWriter("rmax.data");
		for(int i = 0; i < 120; i++){
			double rmax = 1+0.1*(i);
			toMinimize = x => Me(x,0.01,rmax, 0.01, 0.03);
			eval = newton(toMinimize,-1,1e-4);
            rmaxconvergenceData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}", rmax, Abs(eval+0.5)/0.5));
		}
        rmaxconvergenceData.Close();

        var absaccconvergenceData = new StreamWriter("absacc.data");
		for(int i = 0; i < 150; i+=1){
			double absacc = 1e-4*(i+1);
            // WriteLine($"absacc = {absacc}");
			toMinimize = x => Me(x,0.01,8, absacc, 0.01);
			eval = newton(toMinimize,-1,1e-4);
            absaccconvergenceData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}", absacc, Abs(eval+0.5)/0.5));
		}
        absaccconvergenceData.Close();
		
        var epsaccconvergenceData = new StreamWriter("epsacc.data");
		for(int i = 0; i < 100; i+=1){
			double epsacc = 1e-3*(i+1);
            // WriteLine($"epsacc = {epsacc}");
			toMinimize = x => Me(x, 0.01, 8, 1e-4, epsacc);
			eval = newton(toMinimize,-1,1e-4);
            epsaccconvergenceData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}", epsacc, Abs(eval+0.5)/0.5));
		}
        epsaccconvergenceData.Close();
    }

    static vector rosenbrocks(vector x){
        return new vector(2*x[0]*200*(x[0]*x[0] - x[1]) + 2*x[0]-2, 200*(x[1]-x[0]*x[0]));
    }

    public static double Me(double E, double rmin = 0.001, double rmax = 8, double absacc = 0.01, double epsacc = 0.01, List<double> rs = null, List<vector> ys = null){
                Func<double,vector,vector> schr  = (x,y) => {
                    vector dydr = new vector(2);
                    dydr[0] = y[1];
                    dydr[1] = -2*(E+1/x)*y[0];
                    return dydr;
                };

               
                vector init = new vector(2);
                init[0] = rmin - rmin*rmin;
                init[1] = 1-2*rmin;
                (List<double> xmax, List<vector> ymax) = ode.driver(schr, rmin, init, rmax, rs, ys, 0.01, absacc, epsacc);
                return ymax[0][0]; 
        }


    static double newton(Func<double,double> f, double x, double eps=1e-3){
        Func<vector,vector> fvec = z => new vector(f(z[0]));
        vector xvec = new vector(x);
        return newton(fvec,xvec,eps)[0];
    }

    static vector newton(Func<vector,vector> f, vector x, double eps=1e-3){
        int n = x.size;
        double dx;
        vector fx;
        matrix jmatrix = new matrix(n,n);
        matrix R = new matrix(n,n);
        matrix Q = new matrix(n,n);
        vector newx = new vector(n);   
        double magfx = f(x).norm();
        while (magfx > eps){
            dx = x.norm() * Pow(2,-26);
            if (dx == 0) dx = Pow(2,-26);
            fx = f(x);
            for(int i=0; i < n; i++){
                newx = x.copy();
                newx[i] += dx;
                jmatrix[i] = (f(newx) - fx)/dx;
            }
            (Q, R) = QRGS.decomp(jmatrix);
            newx = QRGS.solve(Q, R, -fx);

            double lambda = 1;
            while(((f(x+lambda*newx)).norm() > (1.0-lambda/2)*fx.norm()) && (lambda > 1.0/1024)){
                lambda /= 2;
            }
            x += lambda*newx;
            magfx = f(x).norm();
        }
        return x;
    }
}