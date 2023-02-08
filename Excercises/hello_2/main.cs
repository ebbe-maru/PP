using System;
using static System.Console;
using static System.Math;
class main {
    public static string s = "class scope s";
    public static void print_s(){WriteLine(s);}

    public static void Main(){
        string s = "method scope s";
        print_s();
        WriteLine(s);
        static_hello.print();
        static_world.print();
        static_hello.greeting = "new hello from main\n";
        static_world.greeting = "new hello from main\n";
        static_hello.print();
        static_world.print();
        Write($"the value of pi in Math is {PI}\n");
        Write("the value of pi in Math is {0}\n", PI);
        Write($"the value of e in Math is {E}\n");

        double sqrt2 = Sqrt(2);
        Write($"Sqrt2^2 = {sqrt2*sqrt2}\n");
        Write($"1/2 = {1/2}\n");
        Write($"1.0/2 = {1.0/2}\n");
    }
}