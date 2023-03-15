using System;
using static System.Console;
using static System.Math;
using static cmath;
using static complex;
class main {
    public static void Main() {
        complex a = new complex(-1, 0);
        WriteLine($"{a}");
        complex sqrt1 = sqrt(a);
        complex sqrt1cp = new complex(0,1);
        complex sqrt1cn = new complex(0,-1);
        WriteLine($"sqrt(-1) is = {sqrt1}");
        WriteLine($"sqrt(-1) should be = {sqrt1cp} or = {sqrt1cn}");
        WriteLine($"approx equal {sqrt1.approx(sqrt1cp)}\n");

        complex logi = log(complex.I);
        complex logic = complex.I*PI/2;
        WriteLine($"log(i) is = {logi}");
        WriteLine($"log(i) should be = {logic}");
        WriteLine($"approx equal {logi.approx(logic)}\n");

        complex sqrti = sqrt(complex.I);
        complex sqrtic = (1+complex.I)/sqrt(2);
        WriteLine($"sqrt(i) is = {sqrti}");
        WriteLine($"sqrt(i) should be = {sqrtic}");
        WriteLine($"approx equal {sqrti.approx(sqrtic)}\n");

        complex powi = complex.I.pow(complex.I);
        complex powic = E.pow(-PI/2);
        WriteLine($"i^i is = {powi}");
        WriteLine($"i^i should be = {powic}");
        WriteLine($"approx equal {powi.approx(powic)}\n");
    }
}