using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class main{
    public static void Main() {
        
    }
}

public static class QRGS{
   public static (matrix, matrix) decomp(matrix A){
        int m = A.size2;
        matrix Q=A.copy() , R=new matrix (m,m) ;
        for(int i=0; i<m; i++){
			// Normalise the i-th column of A and set the j-th diagonal element of R
            R[i,i] = A[i].norm();
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
		matrix I = new matrix(Q.size1, Q.size2); // Make indentity matrix for unit vectors
		I.setid(); 
		matrix B = new matrix(Q.size1, Q.size2);
		for (int i=0; i<Q.size1; i++){
			B[i] = solve(Q,R,I[i]);
		}
		return B;
	}
}