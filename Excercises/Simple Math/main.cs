using System;
using static System.Console;
using static System.Math;
class main {
    public static void Main() {
        double sqrt2=Sqrt(2);
        Write($"sqrt2^2 = {sqrt2*sqrt2} (should equal 2)\n");

        double Gamme1 = spfun.Gamma(1);
        WriteLine($"Gamme(1) = {Gamme1}");
        int Fac0 = spfun.Fac(0);
        WriteLine($"0! = {Fac0}");

        double Gamme2 = spfun.Gamma(2);
        WriteLine($"Gamme(2) = {Gamme2}");
        int Fac1 = spfun.Fac(1);
        WriteLine($"1! = {Fac1}");

        double Gamme3 = spfun.Gamma(3);
        WriteLine($"Gamme(3) = {Gamme3}");
        int Fac2 = spfun.Fac(2);
        WriteLine($"2! = {Fac2}");

        double Gamme10 = spfun.Gamma(10);
        WriteLine($"Gamme(10) = {Gamme10}");
        int Fac9 = spfun.Fac(9);
        WriteLine($"9! = {Fac9}");
    }
}