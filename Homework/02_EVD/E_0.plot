set terminal svg background "white"
set key bottom right
set output "E_0.svg"
set xlabel "dr"
set x2label "r_{max}"
set ylabel "\epsilon_0"
set xtics nomirror
set x2tics
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Varying dr and r_{max}"
set xrange [1.5:0]
set yrange [-0.6:-0.1]
set x2range [2:15]
set grid

f(x) = -0.5
plot \
    "dr.data" using 1:2 axis x1y1 with lines title "Varying dr, r_{max}=15", \
    "rmax.data" using 1:2 axis x2y1 with lines title "Varying r_{max}, dr=0.1",  \
    f(x) with lines dt 2 title "Expected value"\