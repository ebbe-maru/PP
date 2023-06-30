using static System.Math;
using static System.Console;
using static System.Double;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System;
class main{
    static ann myNetwork;
    public static void Main(){
        testA();
        testB();
    }

    public static double g(double x){
        return Cos(5.0*x-1.0)*Exp(-x*x);
    }

    public static double dgdx(double x){
        return -2.0*Exp(-x*x)*(2.5*Sin(5*x-1) + x * Cos(5*x-1));
    }

    public static double d2gdx2(double x){
        return Exp(-x*x)*((4*x*x-27)*Cos(5*x-1) + 20*x*Sin(5*x-1));
    }

    public static double intg(double x, double a){
        return integrator.integrate(g, a, x);
    }


    static void testA(){
        WriteLine("Part A");
        // Initialize training set
        int k = 50;
        vector xs = new vector(k);
        vector ys = new vector(k);
        string toWrite = "";
        for(int i = 0; i<k; i++){
            double x = -1 + i*2.0/k;
            xs[i] = x;
            ys[i] = g(x);
			toWrite += $"{xs[i]}\t{ys[i]}\n";
        }
		File.WriteAllText("trainingPoints.txt",toWrite);
        
        // Training ... 
        int n = 10;
        WriteLine($"Testing with {n} neurons...");
        myNetwork = new ann(n);
        myNetwork.train(xs, ys);
        WriteLine($"Done training in {myNetwork.nsteps} steps");

        var gData = new StreamWriter("g.data");
        double fieldWidth = 20;
		for(int i = 0; i < 1000; i++){
            double x = -1.0 + i*2.0/1000;
            gData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", x, myNetwork.response(x), g(x)));
		}
        gData.Close();
        WriteLine("Interpolation can be seen in g.svg");

    }

    static void testB(){
        WriteLine("______________________");
        WriteLine("Part B");

        var dgdxData = new StreamWriter("dgdx.data");
        double fieldWidth = 20;
		for(int i = 0; i < 1000; i++){
            double x = -1.0 + i*2.0/1000;
            dgdxData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", x, myNetwork.firstDerivativeRespone(x), dgdx(x)));
		}
        dgdxData.Close();

        var d2gdx2Data = new StreamWriter("d2gdx2.data");
		for(int i = 0; i < 1000; i++){
            double x = -1.0 + i*2.0/1000;
            d2gdx2Data.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", x, myNetwork.secondDerivativeRespone(x), d2gdx2(x)));
		}
        d2gdx2Data.Close();

        var intgData = new StreamWriter("intg.data");
		for(int i = 0; i < 1000; i++){
            double x = -1.0 + i*2.0/1000;
            intgData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", x, myNetwork.firstAntiDerivativeRespone(x, -1), intg(x, -1)));
		}
        intgData.Close();

        WriteLine("The first-, second- derivative and anti-derivatives can be seen in dgdx.svg, d2gdx2.svg and intg.svg respectivly.");

    } 

}


public static class integrator{
    public static int evals;
    public static double integrate(Func<double,double> f, double a, double b, double delta=0.001, double eps=0.001, double f2=NaN, double f3=NaN){
        double h=b-a;
        if(IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); evals = 0;}
        evals += 1;
        double f1= f(a+h/6);
        double f4 = f(a+5*h/6);
        double Q = (2*f1+f2+f3+2*f4)/6*(b-a);
        double q = (f1+f2+f3+f4)/4*(b-a); 
        double err = Abs(Q-q);
        if (err <= delta+eps*Abs(Q)) return Q;
        else return integrate(f,a,(a+b)/2,delta/Sqrt(2),eps,f1,f2)+
                    integrate(f,(a+b)/2,b,delta/Sqrt(2),eps,f3,f4);
    }

    public static double integrateClenCur(Func<double,double> f, double a, double b, double delta=0.001, double eps=0.001, double f2=NaN, double f3=NaN){
        Func<double,double> ftransformed = x => f((a+b)/2 + (b-a)/2*Cos(x)) * Sin(x)*(b-a)/2;
        return integrate(ftransformed, 0, PI, delta, eps, f2, f3);
    }
}