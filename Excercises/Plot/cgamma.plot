set dgrid3d 30,30
set hidden3d
set terminal svg background "white"
set key bottom right
set output "cgamma.svg"
set xlabel "Re(Z)"
set ylabel "Im(Z)"
set tics out
set samples 800
set key right 
set title "|Gamma(Z)|"
splot "cgamma.data" u 1:2:3 with lines