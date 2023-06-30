set terminal svg background "white"
set key bottom right
set output "linspline.svg"
set xlabel "x"
set ylabel "y"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Linear spline"

plot [0:10] \
    "linspline.data" using 1:2 with linespoints title "Original data", \
    "linspline.data" using 3:4 title "Splined datapoints", \
    "linspline.data" using 3:5 with linespoints title "Integration", \