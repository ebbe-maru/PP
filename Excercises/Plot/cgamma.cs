using System;
using static System.Console;

class main{
    static void Main(){
        double start = -5.1;
        double x = start, y = start;
        int size = 200;
        double steps = -2*start/(size-1); 
        for (int i = 0; i<size; i++){
            for (int j = 0; j<size; j++){
                complex a = spfun.CGamma(new complex(x, y));
                WriteLine($"{x} {y} {complex.magnitude(a)}");
                y += steps;
            }
            y = start;
            x +=steps;
        }
    }

    public static void arrayPrint(double[,] A) {
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++) {
                Console.Write("{0} ", A[i, j]);
            }
            Console.WriteLine();
        }
    }
}
