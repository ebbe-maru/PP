using System;
using static System.Console;

class main{
    static void Main(){
        double dx = 1.0/64, shift = dx/2;
        for (double x = shift; x<=5; x+=dx){
            WriteLine($"{x} {spfun.erf(x)}");
        }
    }
}