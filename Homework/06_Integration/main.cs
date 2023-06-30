using static System.Math;
using static System.Console;
using static System.Double;
using System.IO;
using System;
class main{
    public static void Main(){
        testA();
        testB();

    }

    static void testA(){
        WriteLine("Testing of intregator:");

        double resultA = integrator.integrate(sqrt, 0, 1);
        WriteLine($"Integral from 0 to 1 of sqrt(x): should be equal to 2/3 | Calcucated: {resultA} in {integrator.evals} evaluations.");

        double resultB = integrator.integrate(invsqrt, 0, 1);
        WriteLine($"Integral from 0 to 1 of 1/sqrt(x): should be equal to 2 | Calculated: {resultB} in {integrator.evals} evaluations.");

        double resultC = integrator.integrate(foursqrt, 0, 1);
        WriteLine($"Integral from 0 to 1 of 4*sqrt(1-x^2): should be equal to PI | Calculated: {resultC} in {integrator.evals} evaluations.");

        double resultD = integrator.integrate(lninvsqrt, 0, 1);
        WriteLine($"Integral from 0 to 1 of Ln(x)/sqrt(x): should be equal to -4 | Calculated: {resultD} in {integrator.evals} evaluations.");

        var errorData = new StreamWriter("error.data");
        int fieldWidth = 20;
        for (int i = 0; i<1000; i++){
            double x = -3.0 + 6.0*i/1000.0; 
            errorData.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", x, errorfun(x), erfapprox(x)));
        }
        errorData.Close();

        string tabPoints = File.ReadAllText("errorTabulated.data");
        var errorDiff = new StreamWriter("error_diff.data"); 
		string[] lines = tabPoints.Split("\n");
		for(int i = 0; i < lines.Length-1; i++){
			double x = double.Parse(lines[i].Split(" ")[0]);
			double tabVal = double.Parse(lines[i].Split(" ")[1]);
			errorDiff.WriteLine($"{x}\t{Abs(errorfun(x)-tabVal)}\t{Abs(erfapprox(x)-tabVal)}\t{Pow(errorfun(x)-tabVal,2)-Pow(erfapprox(x)-tabVal,2)}");
		}
		errorDiff.Close();

        WriteLine("In figure error_diff.svg we can see that the integral for erf is in general better than erf_aprrox. \nThe absolut and relative accuary are 1e-7 and 1e-6. For larger values, the integral erf might not be more accurate.");


    }

    static void testB(){
        WriteLine();
        WriteLine("Testing number of evalutation \n");

        WriteLine("Integral from 0 to 1 of 1/sqrt(x): should be equal to: 2");
        double resultnormalA = integrator.integrate(invsqrt, 0, 1);
        WriteLine($"Normal adaptive quadratures gives: {resultnormalA} in {integrator.evals} evaluations");
        double resultCCA = integrator.integrateClenCur(invsqrt, 0, 1);
        WriteLine($"Clenshaw-Curtis variable transformation gives: {resultCCA} in {integrator.evals} evaluations");

        WriteLine();
        WriteLine($"Integral from 0 to 1 over 4*sqrt(1-x^2) should be equal to: {-4}");
        double resultnormalB = integrator.integrate(lninvsqrt, 0, 1);
        WriteLine($"Normal adaptive quadratures gives: {resultnormalB} in {integrator.evals} evaluations");
        double resultCCB = integrator.integrateClenCur(lninvsqrt, 0, 1);
        WriteLine($"Clenshaw-Curtis variable transformation gives: {resultCCB} in {integrator.evals} evaluations");

        WriteLine();
        WriteLine("Compared to python they find the results, with the same tolerance, in 231 and 315 evaluations respectivly");
    }



    static double sqrt(double x){
        return Sqrt(x);
    }

    static double invsqrt(double x){
        return 1/Sqrt(x);
    }

    static double foursqrt(double x){
        return 4*Sqrt(1-x*x);
    }

    static double lninvsqrt(double x){
        return Log(x)/Sqrt(x);
    }

    static double errorfun(double z){
        if (z<0){
            return -errorfun(-z);
        }
        else if (1<z){
            return 1 - 2/Sqrt(PI) * integrator.integrate(x => Exp(-Pow(z+(1-x)/x,2))/x/x, 0, 1,1e-7, 1e-6);
        }
        else{
            return 2/Sqrt(PI) * integrator.integrate(x => Exp(-x*x),0, z,1e-7,1e-6);
        }
    }

    static double erfapprox(double x){
        /// single precision error function (Abramowitz and Stegun, from Wikipedia)
        if(x<0) return -erfapprox(-x);
        double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
        double t=1/(1+0.3275911*x);
        double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
        return 1-sum*Exp(-x*x);
    } 
}

public static class integrator{
    public static int evals;
    public static double integrate(Func<double,double> f, double a, double b, double delta=0.001, double eps=0.001, double f2=NaN, double f3=NaN){
        double h=b-a;
        if(IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); evals = 0;} // first call, no points to reuse
        evals += 1;
        double f1= f(a+h/6);
        double f4 = f(a+5*h/6);
        double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
        double q = (f1+f2+f3+f4)/4*(b-a); // lower order rule
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