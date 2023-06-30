Ebbe Maru Storm Sørensen
Studienr.:201807087
Au-mail: 201807087@post.au.dk

Exam project: 9
Numeric Hessian matrix in Newton's minimization method

Implement the (modified) Newton's minimization method where the Hessian matrix 
is computed numerically using a finite-difference approximation 
(for example, equation (48) in the Minimization Chapter).

Compare with your quasi-Newton minimizer.

--------------------------------------------------------------------------------------------------------------
The Aim of the project was to make a different implementation of the Newton minimization method, 
compared to the one made in Homework 9. The method is used to calculate extrema or more precisly minima,
but it can easily be modified to find maxima, aswell. The main differnce in this project and the homework, 
is how we calculate the Hessian matrix at different steps. In this implimentation we calculate it using 
equation 48 in Minimization chapter.

The minimization was tested on a couple function which can be seen in the Out.txt file. Here the comparison
with the quasi-Newton model can be seen aswell. Futhermore a fit was made to the measurements for the 
Higgs-particle with this new implimentation, with the data and fit plottet in Higgs.data.

My comparison with the Quasi-model is only in the number of iteration each method have to perform. As a C-part
if I had more time, I would test the time it takes for each method instead. Maybe I would test for more 
variabels aswell since the Hessian matrix is of size (n, n) for n variabels, and it would be interesting to 
see how the computing time scales with n.
---------------------------------------------------------------------------------------------------------------
Evalutation:
For the exam I would say I have completed parts A and B but not C (Reasons stated above).
So I would give myself 9 points. I have evaluated my homework to a total of 92 points.

Total: 0.7*92/10 + 0.3*9 = 9.14
