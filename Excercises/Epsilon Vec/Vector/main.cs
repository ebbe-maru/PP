using System;
using static System.Console;
using static System.Math;

public static class main{
    public static void Main() {
       vec u =  new vec();
       u.print("u = ");

       vec v = new vec(1, 2, 3);
       v.print("v = ");

       vec u1 = 2 * v;
       u1.print("u1 = 2*v");

       vec v1 = v-u1;
       v1.print("v1 = v - u1 = ");

       double dotp = v.dot(v1);
       WriteLine($"v.dot(v1) = {dotp}");
    }
}