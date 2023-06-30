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
        WriteLine($"0! = {Fac0}\n");

        double Gamme2 = spfun.Gamma(2);
        WriteLine($"Gamme(2) = {Gamme2}");
        int Fac1 = spfun.Fac(1);
        WriteLine($"1! = {Fac1}\n");

        double Gamme3 = spfun.Gamma(3);
        WriteLine($"Gamme(3) = {Gamme3}");
        int Fac2 = spfun.Fac(2);
        WriteLine($"2! = {Fac2}\n");

        double Gamme10 = spfun.Gamma(10);
        WriteLine($"Gamme(10) = {Gamme10}");
        int Fac9 = spfun.Fac(9);
        WriteLine($"9! = {Fac9}\n");

        WriteLine($"lngamma(1) = {sfuns.lngamma(1)}");
        WriteLine($"lngamma(2) = {sfuns.lngamma(2)}");
        WriteLine($"lngamma(3) = {sfuns.lngamma(3)}");
        WriteLine($"lngamma(10) = {sfuns.lngamma(10)}\n");

        WriteLine($"Exp(lngamma(1)) = {Exp(sfuns.lngamma(1))}");
        WriteLine($"Exp(lngamma(2)) = {Exp(sfuns.lngamma(2))}");
        WriteLine($"Exp(lngamma(3)) = {Exp(sfuns.lngamma(3))}");
        WriteLine($"Exp(lngamma(10)) = {Exp(sfuns.lngamma(10))}");
    }
}