using System;
using static System.Console;
using static System.Math;
public class spfun {
    public static double Gamma (double x) {
    ///single precision gamma function (formula from Wikipedia)
        if(x<0)return PI/Sin(PI*x)/Gamma(1-x); // Euler's reflection formula
        if(x<9)return Gamma(x+1)/x; // Recurrence relation

        double lnGamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return Exp(lnGamma);
    }

    public static double lnGamma (double x) {
        if(x<9) return Log(Gamma(x));
        
        double lnGamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return lnGamma;
    }
    public static int Fac (int n) { 
        if (n <= 1 ) { 
            return 1;
        } else {
            return n*Fac(n-1);
        }
    }
}