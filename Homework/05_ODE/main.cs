using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.IO;
using System;
class main{
    public static void Main(){
        testA();
        testB();
        testC();
    }

    static void testA(){
        List<double> xlist = new List<double>();
        List<vector> ylist = new List<vector>();
        vector ya = new vector(0,1);
        (var xs, var ys) = driver(harmonic, 0, ya, 10, xlist, ylist);
    
        var harmonicdata = new StreamWriter("harmonic.data");
        int fieldWidth = 20;
        for (int i = 0; i<xs.Count; i++){
            harmonicdata.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", xs[i], ys[i][0], ys[i][1]));
        }
        harmonicdata.Close();

        
        xlist = new List<double>();
        ylist = new List<vector>();
        ya = new vector(PI - 0.1, 0.0); 
        (xs, ys) = driver(dampedPendulum, 0, ya, 10, xlist, ylist);
        // Save data
        var dampeddata = new StreamWriter("damped.data");
        for (int i = 0; i<xs.Count; i++){
            dampeddata.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", xs[i], ys[i][0], ys[i][1]));
        }
        dampeddata.Close();
    }

    static void testB(){
        List<double> xlist = new List<double>();
        List<vector> ylist = new List<vector>();
        vector ya = new vector(10.0,5.0);
        (var xs, var ys) = driver(lotkavolterra, 0, ya, 15, xlist:xlist, ylist:ylist);
        
        int fieldWidth = 20;
        var lotkadata = new StreamWriter("lotka.data");
        for (int i = 0; i<xs.Count; i++){
            lotkadata.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", xs[i], ys[i][0], ys[i][1]));
        }
        lotkadata.Close();

        (xs, ys) = driver(lotkavolterra, 0, ya, 15); 
        var lotkaenddata = new StreamWriter("lotkaend.data");
        for (int i = 0; i<xs.Count; i++){
            lotkaenddata.WriteLine(string.Format("{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}", xs[i], ys[i][0], ys[i][1]));
        }
        lotkaenddata.Close();
    }

    static void testC(){
        vector y0 = new vector(new double[]{0.97000436, -0.24308753, 0.93240737/2, 0.86473146/2,
                                            -0.97000436, 0.24308753, 0.93240737/2, 0.86473146/2,
                                            0, 0, -0.93240737, -0.86473146});
        double a = 0;
        double b = 6.32591398;
        List<double> xlist = new List<double>();
        List<vector> ylist = new List<vector>();
        (var xs, var ys) = driver(threeBodyproblem, a, y0, b, xlist:xlist, ylist:ylist);
        var threebodydata = new StreamWriter("threebody.data");
        int fieldWidth = 24;
        for (int i = 0; i<xs.Count; i++){
            string datastringr1 = "{0,-" + fieldWidth + "}{1,-" + fieldWidth + "}{2,-" + fieldWidth + "}{3,-" + fieldWidth + "}";
            string datastringr2 = "{4,-" + fieldWidth + "}{5,-" + fieldWidth + "}{6,-" + fieldWidth + "}{7,-" + fieldWidth + "}";
            string datastringr3 = "{8,-" + fieldWidth + "}{9,-" + fieldWidth + "}{10,-" + fieldWidth + "}{11,-" + fieldWidth + "}";
            threebodydata.WriteLine(string.Format(datastringr1+datastringr2+datastringr3 , xs[i], ys[i][0], ys[i][1], ys[i][2], ys[i][3],
                                                                                                 ys[i][4], ys[i][5], ys[i][6], ys[i][7],
                                                                                                 ys[i][8], ys[i][9], ys[i][10], ys[i][11]));
        }
        threebodydata.Close();

    }

    static vector harmonic(double x, vector ys){
        return new vector(ys[1], -ys[0]);
    }

    static vector dampedPendulum(double x, vector ys){
        double b = 0.25;
        double c = 5.0;
        return new vector(ys[1], -b*ys[1]-c*Sin(ys[0]));
    }

    static vector threeBodyproblem(double t, vector ys){
        double m1 = 1; double m2 = 1; double m3 = 1;

        vector r1 = new vector(ys[0], ys[1]);
        vector dr1dt = new vector(ys[2], ys[3]);
        
        vector r2 = new vector(ys[4], ys[5]);
        vector dr2dt = new vector(ys[6], ys[7]);

        vector r3 = new vector(ys[8], ys[9]);
        vector dr3dt = new vector(ys[10], ys[11]);

        vector ddr1dt2 = gravity(r1, r2, r3, m2, m3);
        vector ddr2dt2 = gravity(r2, r1, r3, m1, m3);
        vector ddr3dt2 = gravity(r3, r1, r2, m1, m2);

        return new vector(new double[]{dr1dt[0], dr1dt[1], ddr1dt2[0], ddr1dt2[1], dr2dt[0], dr2dt[1], ddr2dt2[0], ddr2dt2[1], dr3dt[0], dr3dt[1], ddr3dt2[0], ddr3dt2[1]});

    }

    static vector gravity(vector r1, vector r2, vector r3, double m2, double m3){
        const double G = 1;
        vector ddrt2 = -G*m2*(r1-r2)/Pow((r1-r2).norm(), 3) - G*m3*(r1-r3)/Pow((r1-r3).norm(), 3);
        return ddrt2;
    }


    static vector lotkavolterra(double t, vector ys){
        
        double a = 1.5;
        double b = 1.0;
        double c = 3.0;
        double d = 1.0;
        double x = ys[0];
        double y = ys[1];
        double dxdt = a*x - b*x*y;
        double dydt = -c*y + d*x*y;
        return new vector(dxdt, dydt);
    }


    static (vector, vector) rkstep12(
        Func<double,vector,vector> f, 
        double x,                     
        vector y,                     
        double h                      
        ){
        vector k0 = f(x,y);
        vector k1 = f(x+h/2, y+k0*(h/2));
        vector yh = y + k1*h;
        vector er = (k1-k0)*h;
        return (yh, er);
    }

    static (List<double>,List<vector>) driver(
        Func<double,vector,vector> f, 
        double a,                     
        vector ya,                    
        double b,                     
        List<double> xlist=null,      
        List<vector> ylist=null,      
        double h=0.01,                
        double acc=0.01,              
        double eps=0.01               
        ){
        if(a>b) throw new ArgumentException("driver: starting point, a, is after endpoint, b.");
        double x = a; 
        vector y = ya.copy();
        do{
            vector tol = new vector(y.size);
            vector err = new vector(y.size);
            if(x>=b){
                if(!(xlist == null || ylist == null)){ 
                    return (xlist,ylist);
                }
                else{
                    xlist = new List<double>();
                    ylist = new List<vector>();
                    xlist.Add(x);
                    ylist.Add(y);
                    return (xlist, ylist);
                }
            }
            if(x+h>b) h=b-x;              
            
            var (yh,erv) = rkstep12(f,x,y,h);
            
            for(int i=0;i<y.size;i++){
                tol[i]=Max(acc, eps*Abs(yh[i]))*Sqrt(h/(b-a));
            }
            bool ok=true;
            for(int i=0;i<y.size;i++){
                if(!(err[i]<tol[i])) {
                    ok=false;
                }
            }
            if(ok){ 
                x+=h; y=yh; 
                if(xlist!=null & ylist!=null){
                    xlist.Add(x);
                    ylist.Add(y);
                }
            }
            double factor = tol[0]/Abs(erv[0]);
            for(int i=1;i<y.size;i++){
                factor=Min(factor,tol[i]/Abs(erv[i]));
            }
            h *= Min(Pow(factor,0.25)*0.95, 2);
        }
        while(true);
    }


    public static (List<double> xlist, List<vector> ylist) ImprovedDriver(System.Func<double, vector, vector> f, double a,
        vector ya, double b, double h = 0.01, double acc = 0.01, double eps = 0.01, List<double> xlist = null, List<vector> ylist = null)
    {
        double x = a;
        vector y = ya.copy();
        
        do
        {
            double[] tol = new double[y.size];
            double[] err = new double[y.size];
            
            if (x >= b)
            {
                if (xlist == null && ylist == null)
                {
                    xlist = new List<double>();
                    xlist.Add(x);
                    ylist = new List<vector>();
                    ylist.Add(y);
                    return (xlist, ylist);
                }
                else
                {
                    return (xlist, ylist);
                }
            }
            if (x+h>b)
            {
                h = b - x;
            }
            
            var (yh, erv) = rkstep12(f, x, y, h);
            for (int i = 0; i < y.size; i++)
            {
                tol[i] = System.Math.Max(acc, yh.norm() * eps) * System.Math.Sqrt(h / (b - a));
            }
            bool ok = true;
            for (int i = 0; i < y.size; i++)
            {
                if (!(err[i] < tol[i]))
                {
                    ok = false;
                }
            }
            if (ok)
            {
                x += h;
                y = yh;
                if (xlist != null & ylist != null)
                {
                    xlist.Add(x);
                    ylist.Add(y);
                }
                double factor = tol[0] / System.Math.Abs(erv[0]);
                for (int i = 1; i < y.size; i++)
                {
                    factor = System.Math.Min(factor, tol[i] / System.Math.Abs(erv[i]));
                }
                h *= System.Math.Min(System.Math.Pow(factor, 0.25) * 0.95, 2);
            }
        } while (true);
        
    }
}