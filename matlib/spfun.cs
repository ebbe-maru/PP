using System;
using static System.Console;
using static System.Math;
using static cmath;
using static complex;


public class spfun {
    public static double Gamma (double x) {
    ///single precision gamma function (formula from Wikipedia)
        if(x<0)return PI/Sin(PI*x)/Gamma(1-x); // Euler's reflection formula
        if(x<9)return Gamma(x+1)/x; // Recurrence relation

        double lnGamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return Exp(lnGamma);
    }

    public static complex CGamma (complex x) {
        double abs = magnitude(x);
        if(abs<0)return PI/sin(PI*x)/CGamma(1-x); // Euler's reflection formula
        if(abs<9)return CGamma(x+1)/x; // Recurrence relation

        complex lnGamma=x*log(x+1/(12*x-1/x/10))-x+log(2*PI/x)/2;
        return exp(lnGamma);
    }

    
    public static double lngamma(double x){
        ///single precision lngamma function (formula from Wikipedia)
        if(x<0) return double.NaN;
        if(x<9) return lngamma(x+1)-Log(x); // Recurrence relation
        return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;  
    }
    

    public static int Fac (int n) { 
        if (n <= 1 ) { 
            return 1;
        } else {
            return n*Fac(n-1);
        }
    }

    public static double erf(double x){
        if(x<0) return -erf(-x);
            double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
            double t=1/(1+0.3275911*x);
            double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
        return 1-sum*Exp(-x*x);
    } 
}