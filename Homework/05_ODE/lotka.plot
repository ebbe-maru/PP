set terminal svg background "white"
set key top left
set output "lotka.svg"
set xlabel "t"
set ylabel "Population"
set tics out
set samples 800
set title "Lotka-Volterra system"

plot [0:15] \
    "lotka.data" using 1:2 with lines linecolor rgb "orange" linewidth 3 title "x", \
    "lotka.data" using 1:3 with lines linecolor rgb "royalblue" linewidth 3 title "y", \
    "lotkaend.data" using 1:2 with points pt 7 linecolor rgb "orange" linewidth 3 title "Endpoint x", \
    "lotkaend.data" using 1:3 with points pt 7 linecolor rgb "royalblue" linewidth 3 title "Endpoint y", \