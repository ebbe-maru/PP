using System;
using static System.Console;
using static System.Math;

public static class main {
    public static void Main() {
        main.max();
        int max = int.MaxValue;
        WriteLine($"Max int = {max}");

        main.min();
        int min = int.MinValue;
        WriteLine($"Min int = {min}\n");

        main.Float();
        double Float = Pow(2,-23);
        WriteLine($"Float = {Float}\n");

        main.Double();
        double Double = Pow(2,-52);
        WriteLine($"Double = {Double}\n");

        int n=(int)1e6;
        double epsilon=Pow(2,-52);
        double tiny=epsilon/2;
        double sumA=0,sumB=0;

        sumA+=1; 
        for(int i=0;i<n;i++){sumA+=tiny;}

        for(int i=0;i<n;i++){sumB+=tiny;} 
        sumB+=1;

        WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
        WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}\n");

        double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
        double d2 = 8*0.1;
        
        WriteLine($"d1={d1:e15}");
        WriteLine($"d2={d2:e15}");
        WriteLine($"d1==d2 ? => {d1==d2}\n");
    }
    public static void max() {
        int i = 1;
        while(i+1>i) {i++;}
        WriteLine($"Max = {i}");
    }
    public static void min() {
        int i = 1;
        while(i-1<i) {i--;}
        WriteLine($"Min = {i}");
    }

    public static void Float() {
        float y=1F; 
        while((float)(1F+y) != 1F){y/=2F;}
        y*=2F;
        WriteLine($"Float precision = {y}");
    }
    public static void Double() {
        double x=1;
        while(1+x!=1){x/=2;}
        x*=2;
        WriteLine($"Double precision = {x}");
    }

    public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9) {
        double d = Abs(a-b);
        if (d < acc ) {
            return true;
        } else if (d < Max(Abs(a), Abs(b))) {
            return true;
        }
        else {
            return false;
        }
    }

}