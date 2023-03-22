using System;
using static System.Console;

class main{
    static void Main(){
        for (double x = 1.0; x<=7; x++){
            WriteLine($"{x-1} {spfun.Gamma(x)}");
        }
    }
}