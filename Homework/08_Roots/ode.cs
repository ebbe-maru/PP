using System;
using System.Collections.Generic;
using static System.Math;

public static class ode{
    public static (vector, vector) rkstep12(
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

    public static (List<double>,List<vector>) driver(
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
                tol[i]=(acc+eps*Abs(yh[i]))*Sqrt(h/(b-a));
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