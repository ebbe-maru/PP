using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class QRGS{
    public static void Main() {
        var rnd = new Random();
		WriteLine("TEST FOR QR-DECOMPOSITION WITH RANDOM SIZE MATRICES \n");
			// Create a random n x m matrix
			// A minimum 4 x 4 matrix
			int n = rnd.Next(4,10);
			int m = n - rnd.Next(0,n-1);
            matrix A = matrix.random(n,m,rnd);
			WriteLine($"Matrix A (n={n}, m={m}):");
			A.print();

			// QR-decomposition
            matrix R = A.copy();
            matrix Q = new matrix (m,m);
			(Q, R) = decomp(A);
	
			// Check if R is upper triangular
			for (int j = 0; j < R.size1; j++){
				for (int k = 0; k < j; k++){
					if (Math.Abs(R[j, k]) > 1e-10){
						WriteLine("Error: R is not upper triangular!");
						break;
					}
				}
        	}
			WriteLine("R is upper triangular");
			WriteLine("Matrix R:");
			R.print();
            WriteLine();

			// Check that Q.T()*Q = 1
			matrix I = new matrix(m,m);

			I.setid();
			WriteLine("Matrix Q:");
			Q.print();
            WriteLine();

            matrix QTQ = Q.T*Q;
			if(I.approx(QTQ)){
				WriteLine($"Q.T * Q is equal I");
			}
			else{
				WriteLine("Q.T * Q is not equal to I");
			}
            WriteLine("Matrix Q.T*Q:");
            QTQ.print();
            WriteLine();

			// Check that Q*R = A
			if (A.approx(Q*R)){
				WriteLine("A is equal to Q*R");
			}
			else{
				WriteLine("A is not equal to Q*R");	
			}

			WriteLine("Matrix Q*R:");
			matrix QR = Q*R;
			QR.print();
			WriteLine("------------------------------------------------------------------");
			WriteLine();
			WriteLine();

		WriteLine("------------------------------------------------------------------");
		WriteLine("TEST FOR SQUARE MATRIX \n");

		int n_s = rnd.Next(4,10);
		matrix A_s = matrix.random(n_s,n_s,rnd);
		WriteLine($"Matrix A (n = {n_s}):");
		A_s.print();

        WriteLine();
        
		matrix Q_s = A_s.copy();
		matrix R_s = new matrix(n_s,n_s);
		(Q_s, R_s) = decomp(A_s);
			/// Test for solving linear equation (only works for sqare matrices)
		vector b = new vector(n_s);
		for (int j = 0; j < b.size; j++){
			b[j] = rnd.NextDouble();
		}

		WriteLine("Vector b:");
		b.print();
        WriteLine();
        

		vector x = solve(Q_s, R_s, b);
        vector Ax = A_s*x;

        WriteLine("Vector x:");
		x.print();
        WriteLine();

		if (b.approx(Ax)){
			WriteLine("A*x is equal to b");
		}
		else{		
			WriteLine("A*x is not equal to b");
		}
        WriteLine("Vector Ax:");
		Ax.print();

        WriteLine();
			
		/// Calculate the inverse of A, B, and check A*B=I
		matrix B = inverse(Q_s,R_s);

        WriteLine("Matrix A^-1:");
		B.print();
        WriteLine();

        matrix AB = A_s*B; 
		matrix I_s = new matrix(n_s,n_s);
		I_s.setid();
		
		if (I_s.approx(AB)){
			WriteLine("Matrix A*A^-1 is equal to I");
		}
		else {
			WriteLine("Matrix A*A^-1 is not equal to I");
		}

        WriteLine("Matrix A*A^-1");
		AB.print();
        WriteLine();
        I_s.print();

		WriteLine("------------------------------------------------------------------");
    }

   public static (matrix, matrix) decomp(matrix A){
        int m = A.size2;
        matrix Q=A.copy() , R=new matrix (m,m) ;
        for(int i=0; i<m; i++){
			// Normalise the i-th column of A and set the j-th diagonal element of R
            R[i,i] = Q[i].norm();
            Q[i]/=R[i,i];

			// Orthogonalize the remaining columns of A against the i-th column
            for(int j = i+1; j<m; j ++){
                R[i,j] = Q[i].dot(Q[j]);
                Q[j] -= Q[i] * R[i,j]; 
            }
        }
        return (Q,R);
    }


    public static vector solve(matrix Q, matrix R, vector b){
		int m = R.size1;
		vector x = Q.T * b;  // Apply Q.T to b

		// Back-substitution on x. 
		for (int i = m - 1; i >= 0; i--){
			double sum = 0;
			for (int j = i + 1; j < m; j++){
				sum += R[i, j] * x[j];
			}
			x[i] = (x[i] - sum) / R[i,i];
    	}
    	return x;
	}

    public static double det(matrix R){
        int n = R.size1;
        double product = 1;
        for (int i = 0; i<n; i++) {
            product *= R[i,i];
        }
        return product;
    }

	public static matrix inverse(matrix Q, matrix R){
		matrix I = new matrix(Q.size1, Q.size2); // Make indentity matrix
        I.setid();
        matrix B = new matrix(Q.size1, Q.size2);
		for (int i=0; i<Q.size1; i++){
			B[i] = solve(Q,R,I[i]);
		}
		return B;
	}
}