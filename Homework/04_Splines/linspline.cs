using System;
public class linspline{
    vector x,y;
    double[] p,dx,dy;
    public linspline(vector xs, vector ys){
        if (xs.size != ys.size) throw new ArgumentException("x and y arrays must have same length");
        if (xs.size < 2) throw new ArgumentException("x and y arrays must have at least two elements");
        for (int i = 1; i < xs.size; i++) {
            if (xs[i] <= xs[i - 1]) throw new ArgumentException("x_i >= x_(i+1)");
        }
        this.x = xs;
        this.y = ys;
        this.p = new double[x.size-1];
        this.dx = new double[x.size-1];
        this.dy = new double[x.size-1];
        for(int i=0; i<x.size-1; i++) {
            dx[i] = x[i+1] - x[i];
            if (!(dx[i]>0)) throw new System.Exception("dx_i < 0");
            dy[i] = y[i+1] - y[i];
            p[i] = dy[i]/dx[i];
        }
    }

    public double evaluate(double z){
        int i = binsearch(x,z);
        return y[i]+p[i]*(z-x[i]);
    }

    public double integral(double z){
        int i = binsearch(x,z);
        double sum = 0;
        for(int j = 0; j<=i; j++){
            if(j!=i){
                sum += y[j]*dx[j] + p[j]*dx[j]*dx[j]/2;
            }
            else {
                sum += y[j]*(z-x[i]) + p[j]*(z-x[i])*(z-x[i])/2;
            }
        }
        return sum;
    }



    public static int binsearch(vector x, double z){/* locates the interval for z by bisection */ 
        if(!(x[0]<=z && z<=x[x.size-1])) throw new Exception("binsearch: z out of range");
        int i=0, j=x.size-1;
        while(j-i>1){
            int mid=(i+j)/2;
            if(z>x[mid]) i=mid; else j=mid;
            }
        return i;
	}

}