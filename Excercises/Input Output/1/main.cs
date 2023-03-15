using System;
using static System.Console;
using static System.Math;
class main{
    public static int Main(string[] args){
        int result = 0;
        result += exercise1(args);
        return result;
    }   

    static int exercise1(string[] args){
        WriteLine("Task 1");
        WriteLine("______________________");
        foreach(var arg in args){
            var words = arg.Split(':');
            if(words[0]=="-numbers"){
                var numbers=words[1].Split(',');
                foreach(var number in numbers){
                    double x = double.Parse(number);
                    WriteLine($"{x} {Sin(x)} {Cos(x)}");
                }
            }
        }
        WriteLine("");
        return 0;
    }
}