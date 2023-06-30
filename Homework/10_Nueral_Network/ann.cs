using static System.Math;
using static System.Console;
using static System.Double;
using static System.Random;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System;

public class ann{
    public int n;
    public Func<double,double> f = x => x*Exp(-x*x); 
    public Func<double,double> dfdx = x => Exp(-x*x)*(1-2*x*x); 
    public Func<double,double> d2fdx2 = x => 2*Exp(-x*x)*x*(2*x*x-3); 
    public Func<double,double> intf = x => -0.5*Exp(-x*x); 
    public vector p; 
    public int nsteps;

    public ann(int n){
        this.n = n;
        this.p = new vector(n*3); 
    }
    public double response(double x){
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * f((x-ai)/bi);
        }
        return result;
    }

    public double firstDerivativeRespone(double x){
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * dfdx((x-ai)/bi)/bi;
        }
        return result;
    }

    public double secondDerivativeRespone(double x){
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * d2fdx2((x-ai)/bi)/(bi*bi);
        }
        return result;
    }

    public double firstAntiDerivativeRespone(double x, double a){
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * bi * (intf((x-ai)/bi) - intf((a-ai)/bi));
        }
        return result;
    }

    public void train(vector x, vector y){
        var rand = new Random();
        for (int i = 0; i < p.size; i++){
            p[i] = rand.NextDouble() - 0.5;
        }

        Func<vector, double> cost = ps => {
            p = ps;
            double result = 0;
            for (int k = 0; k < x.size; k++){
                result += Pow(response(x[k]) - y[k], 2);
            }
            return result/x.size;
        };        
        var (res, steps) = qnewton(cost,p,1e-4);
        this.nsteps = steps;
        p = res;
    }

    public (vector, int) qnewton(Func<vector,double> f, vector start, double acc = 1e-4){
        int n = start.size;
        vector x = start.copy();
        vector grad = new vector(n);
        vector deltax = new vector(n);
        vector s = new vector(n);
        vector u = new vector(n);
        vector y = new vector(n);
        matrix deltaB = new matrix(n,n);
        matrix B = new matrix(n,n);
        B.set_identity();
        int nsteps = 0;
        double lambda = 1.0;
        grad = gradient(f, x);
        while(grad.norm() > acc & nsteps<100000*5){
            nsteps++;
            deltax = -B*grad;
            lambda = 1.0;
            while(true){
                s = lambda*deltax;
                if (f(x+s) < f(x)){ 
                    x = x + s;
                    vector oldGrad = grad;
                    grad = gradient(f, x);
                    
                    y = grad - oldGrad;
                    u = s - B*y;
                    
                    deltaB = matrix.outer(u,u)/(u.dot(y));
                    B += deltaB;
                    break;
                }
                lambda = lambda/2;
                if (lambda < 1.0/Pow(2,16)){
                    x = x + s;
                    grad = gradient(f, x);
                    B.set_identity();
                    break;
                }
            }
        }
        return (x, nsteps);
    }

    static vector gradient(Func<vector,double> f, vector x){
        int dim = x.size;
        vector grad = new vector(dim);
        vector newx = x.copy();
        for(int i = 0; i<dim; i++){
            double dx = Abs(x[i])*Pow(2,-26);
            newx[i] = x[i] + dx;
            grad[i] = (f(newx) - f(x))/dx;
        }
        return grad;
    }
}