set terminal svg background "white"
set key top right
set output "sinB.svg"
set xlabel "x"
set ylabel "y"
set tics out
set yrange [-1.5:2.5]
set samples 800
set title "Quadratic spline of sin(x)"

plot [0:6] \
    "sinBData.data" using 1:2 title "Datapoints", \
    "sinBsplineData.data" using 1:2 with points pt 0 title "Interpolated data", \
    "sinBsplineData.data" using 1:3 with points pt 7 title "Integral", \
    "sinBsplineData.data" using 1:4 with points pt 6 title "Derivative"