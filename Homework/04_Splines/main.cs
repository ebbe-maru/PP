using static System.Math;
using static System.Console;
using System.IO;
class main{
    public static void Main(){
        testA();
        testB();
        testBintegralDerivative();
    }

    static void testA(){
        vector xs = new vector("0,1,2,3,4,5,6,7,8,9,10");
        vector ys = new vector(xs.size);
        for(int i = 0; i<xs.size; i++){
            ys[i] = Sin(xs[i]);
        }
        linspline spline = new linspline(xs, ys);
        vector zs = new vector("0.5, 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5, 9.9");
        vector nys = new vector(zs.size);
        for(int i = 0; i<zs.size; i++){
            nys[i] = spline.evaluate(zs[i]);
        }
        vector iys = new vector(zs.size);
        for(int i = 0; i<zs.size; i++){
            iys[i] = spline.integral(zs[i]);
        }


        int fieldWidth = 25;
        var outfile = new StreamWriter("linspline.data");
        for (int i = 0; i<xs.size; i++){
            outfile.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}{3,-" + fieldWidth + "}{4,-" + fieldWidth + "}", xs[i], ys[i], zs[i], nys[i], iys[i]));

        }
        outfile.Close();
        WriteLine("A");
        WriteLine("----------");
        WriteLine($"The indicative plot of the linear spline is in linspline.svg");
        WriteLine($"-----------------------------------------------------------");
    }

    static void testB(){
        vector xs = new vector("1,2,3,4,5");
        vector ys1 = new vector(xs.size);
        vector ys2 = new vector(xs.size);
        vector ys3 = new vector(xs.size);
        for(int i = 0; i<xs.size; i++){
            ys1[i] = 1;
            ys2[i] = xs[i];
            ys3[i] = xs[i]*xs[i];
        }
        qspline spline1 = new qspline(xs, ys1);
        qspline spline2 = new qspline(xs, ys2);
        qspline spline3 = new qspline(xs, ys3);
        int N = 20;
        double stepsize = (xs[xs.size-1]-xs[0])/(N-1);
        vector zs = new vector(N);
        for(int i = 0; i < N; i++ ) {
            zs[i] = xs[0]+i*stepsize;
        }
        vector nys1 = new vector(zs.size);
        vector nys2 = new vector(zs.size);
        vector nys3 = new vector(zs.size);
        for(int i = 0; i<zs.size; i++){
            nys1[i] = spline1.evaluate(zs[i]);
            nys2[i] = spline2.evaluate(zs[i]);
            nys3[i] = spline3.evaluate(zs[i]);
        }
        vector iys1 = new vector(zs.size);
        vector iys2 = new vector(zs.size);
        vector iys3 = new vector(zs.size);
        for(int i = 0; i<zs.size; i++){
            iys1[i] = spline1.integral(zs[i]);
            iys2[i] = spline2.integral(zs[i]);
            iys3[i] = spline3.integral(zs[i]);
        }


        int fieldWidth = 20;
        var datapoints = new StreamWriter("datapoints.data");
        var splinedata = new StreamWriter("qspline.data");
        for (int i = 0; i<xs.size; i++){
            datapoints.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}{3,-" + fieldWidth + "}", xs[i], ys1[i], ys2[i], ys3[i]));
        }
        for (int i = 0; i<zs.size; i++){
            splinedata.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}{3,-" + fieldWidth + "}{4,-" + fieldWidth + "}{5,-" + fieldWidth + "}{6,-" + fieldWidth + "}", zs[i], nys1[i], iys1[i], nys2[i], iys2[i], nys3[i], iys3[i]));

        }
        datapoints.Close();
        splinedata.Close();

        WriteLine("B");
        WriteLine("----------");
        WriteLine("The function y=1, y=x and y=x^2 are are plotted with there quadratic splines in qspline.svg");
        WriteLine("A sin(x) function is plotted in sinB.svg with its quadratic spline, integral and derivative.");
        WriteLine("Calculations of b and c for the three functions:");
        WriteLine("y=1");
        WriteLine($"i=1: expected c_1 = 0, calculated c_1 = {spline1.c[0]}, expected b_1 = 0, calculated b_1 = {spline1.b[0]}");
        WriteLine($"i=2: expected c_2 = 0, calculated c_2 = {spline1.c[1]}, expected b_2 = 0, calculated b_2 = {spline1.b[1]}");
        WriteLine($"i=3: expected c_3 = 0, calculated c_3 = {spline1.c[2]}, expected b_3 = 0, calculated b_3 = {spline1.b[2]}");
        WriteLine($"i=3: expected c_4 = 0, calculated c_4 = {spline1.c[3]}, expected b_4 = 0, calculated b_4 = {spline1.b[3]}");

        WriteLine("y=x");
        WriteLine($"i=1: expected c_1 = 0, calculated c_1 = {spline2.c[0]}, expected b_1 = 1, calculated b_1 = {spline2.b[0]}");
        WriteLine($"i=2: expected c_2 = 0, calculated c_2 = {spline2.c[1]}, expected b_2 = 1, calculated b_2 = {spline2.b[1]}");
        WriteLine($"i=3: expected c_3 = 0, calculated c_3 = {spline2.c[2]}, expected b_3 = 1, calculated b_3 = {spline2.b[2]}");
        WriteLine($"i=4: expected c_4 = 0, calculated c_4 = {spline2.c[3]}, expected b_4 = 1, calculated b_4 = {spline2.b[3]}");
        
        WriteLine("y=x^2");
        WriteLine($"i=1: expected c_1 = 1, calculated c_1 = {spline3.c[0]}, expected b_1 = 2, calculated b_1 = {spline3.b[0]}");
        WriteLine($"i=2: expected c_2 = 1, calculated c_2 = {spline3.c[1]}, expected b_2 = 4, calculated b_2 = {spline3.b[1]}");
        WriteLine($"i=3: expected c_3 = 1, calculated c_3 = {spline3.c[2]}, expected b_3 = 6, calculated b_3 = {spline3.b[2]}");
        WriteLine($"i=4: expected c_4 = 1, calculated c_4 = {spline3.c[3]}, expected b_4 = 8, calculated b_4 = {spline3.b[3]}");
    }
    
    static void testBintegralDerivative(){
        int n = 13;
        vector x = new vector(n);
        vector y = new vector(n);
        for (int i=0; i<n; i++){
            x[i] = i/2.0;
            y[i] = Sin(x[i]);
        }
        qspline spline = new qspline(x,y);
        int fieldWidth = 25;
        var sinsplineData = new StreamWriter("sinBsplineData.data");
        var sinData = new StreamWriter("sinBData.data");
        for(int i=0; i<n; i++){
            sinData.WriteLine(string.Format("{0,-"+fieldWidth+"}{1,-"+fieldWidth+"}", x[i], y[i]));
        }
        for(double z=0.0+1/32; z<6; z+=1.0/32){
            sinsplineData.WriteLine(string.Format("{0,-"+fieldWidth+"}{1,-"+fieldWidth+"}{2,-"+fieldWidth+"}{3,-"+fieldWidth+"}", z, spline.evaluate(z), spline.integral(z), spline.derivative(z)));
        }
        sinData.Close();
        sinsplineData.Close();
    }
}