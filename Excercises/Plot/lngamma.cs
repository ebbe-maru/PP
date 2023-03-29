using System;
using static System.Math;
using static System.Console;

class main{
    static void Main(){
        for (double x = 1.0; x<=7; x+=0.1){
            WriteLine($"{x-1} {Exp(spfun.lngamma(x))}");
        }
    }
}