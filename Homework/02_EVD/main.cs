using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class jacobi{
static void Main(){
    testcyclic();
    drVary();
    rmaxVary();
    s_waves();
}

static void testcyclic() {
    Random rnd = new System.Random(10);
    int n = 5;
    matrix A = matrix.randomsym(n, rnd);
    matrix D = A.copy();
    matrix I = matrix.id(n);
    matrix V = matrix.id(n);

    WriteLine("Matrix A");
    A.print();
    WriteLine();

    cyclic(D, V);

    WriteLine("Matrix D");
    D.print();
    WriteLine();

    WriteLine("Matrix V");
    V.print();
    WriteLine();

    if((V.T*A*V).approx(D)) WriteLine("V.T*A*V is equal to D");
    else WriteLine("V.T*A*V is not equal to D");

    if((V*D*V.T).approx(A)) WriteLine("V*D*V.T is equal to A");
    else WriteLine("V*D*V.T is not equal to A");

    if((V*V.T).approx(I)) WriteLine("V*V.T is equal to I");
    else WriteLine("V*V.T is not equal to I");

    if((V.T*V).approx(I)) WriteLine("V.T*V is equal to I");
    else WriteLine("V.T*V is not equal to I");
}

public static void timesJ(matrix A, int p, int q, double theta){
    double c=Cos(theta),s=Sin(theta);
	for(int i=0;i<A.size1;i++){
		double aip=A[i,p], aiq=A[i,q];
		A[i,p]=c*aip-s*aiq;
		A[i,q]=s*aip+c*aiq;
		}
}
public static void Jtimes(matrix A, int p, int q, double theta){
    double c=Cos(theta),s=Sin(theta);
	for(int j=0;j<A.size1;j++){
		double apj=A[p,j],aqj=A[q,j];
		A[p,j]= c*apj+s*aqj;
		A[q,j]= -s*apj+c*aqj;
		}
}
public static void cyclic(matrix A, matrix V){
        bool changed;
        int n = A.size1;
        do{
            changed=false;
            for(int p=0;p<n-1;p++){
                for(int q=p+1;q<n;q++){
                    double apq=A[p,q], app=A[p,p], aqq=A[q,q];
                    double theta=0.5*Atan2(2*apq,aqq-app);
                    double c=Cos(theta),s=Sin(theta);
                    double new_app=c*c*app-2*s*c*apq+s*s*aqq;
                    double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
                    if(new_app!=app || new_aqq!=aqq){ // do rotation
                        changed=true;
                        timesJ(A,p,q, theta); // A←A*J 
                        Jtimes(A,p,q,-theta); // A←JT*A 
                        timesJ(V,p,q, theta); // V←V*J
                    }
                }
            }
        }
        while(changed);

    }

    static (matrix, matrix) makeHamiltonian(double rmax, double dr) {
        int N = (int)(rmax/dr)-1;
        vector r = new vector(N);
        for(int i=0;i<N;i++){
            r[i]=dr*(i+1);
        }
        matrix H = new matrix(N,N);
        for(int i=0;i<N-1;i++){
            H[i,i]  =-2;
            H[i,i+1]= 1;
            H[i+1,i]= 1;
        }
        H[N-1,N-1]=-2;
        matrix.scale(H,-0.5/dr/dr);

        for(int i=0;i<N;i++){
            H[i,i]+=-1/r[i];
        }
        matrix V = matrix.id(N);
        return (H, V);
    }

    static void drVary(){
        var outfile = new StreamWriter("dr.data");
        for (double dr = 1.5+1/32;dr > 0.0;dr -=1.0/64){
            float rmax = 15F;
            (matrix H, matrix V) = makeHamiltonian(rmax, dr); 
            cyclic(H,V);
            outfile.WriteLine($"{dr} {H[0,0]}");
        }
        outfile.Close();
    }

    static void rmaxVary(){
        var outfile = new StreamWriter("rmax.data");
        for (double rmax = 2+1/32; rmax < 15.0; rmax +=1.0/32){
            float dr = 0.1F;
            (matrix H, matrix V) = makeHamiltonian(rmax, dr); 
            cyclic(H,V);
            outfile.WriteLine($"{rmax} {H[0,0]}");
        }
        outfile.Close();
    }

    static void s_waves() {
        var outfile = new StreamWriter("s_waves.data");
        double rmax = 15;
        double dr = 0.1;
        double c = 1/Sqrt(dr);
        (matrix H, matrix V) = makeHamiltonian(rmax, dr); 
        cyclic(H,V);
        double r = dr;

        for(int y = 0; y<V.size1; y+=1) {
            int p = 2;
            double a = 5.29*Pow(10,-11);
            double S1 = Pow(2*a*r*Pow(a, -3/2)*Exp(-r),p);
            double S2 = Pow(a*r*Pow(a, -3/2)*(1-r/2)*Exp(-r/2)/Sqrt(2),p);
            double S3 = Pow(2*a*r*Pow(3*a, -3/2)*(1-2*r/3+2*r*r/27)*Exp(-r/3),p);

            outfile.WriteLine($"{r} {Pow(c*V[y, 0],p)} {Pow(c*V[y, 1],p)} {Pow(c*V[y, 2],p)} {S1} {S2} {S3}");
            r+=dr;
        }

        outfile.Close();
    }
}