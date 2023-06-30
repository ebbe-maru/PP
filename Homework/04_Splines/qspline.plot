set terminal svg background "white"
set key top left
set output "qspline.svg"
set xlabel "x"
set ylabel "y"
set tics out
set yrange [0:30]
set samples 800
set title "Quadratic spline"

plot [1:5] \
    "datapoints.data" using 1:2 with linespoints linecolor rgb "red" title "y=1", \
    "datapoints.data" using 1:3 with linespoints linecolor rgb "blue" title "y=x", \
    "datapoints.data" using 1:4 with linespoints linecolor rgb "green" title "y=x^2", \
    "qspline.data" using 1:2 linecolor rgb "red" title "Splined datapoints", \
    "qspline.data" using 1:4 linecolor rgb "blue" title "Splined datapoints", \
    "qspline.data" using 1:6 linecolor rgb "green" title "Splined datapoints", \