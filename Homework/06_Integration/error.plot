set terminal svg background "white"
set key bottom right
set output "error.svg"
set xlabel "x"
set ylabel "err(x)"
set tics out
set samples 800
set title "Error function comparison"

plot [-3:3] \
    "error.data" using 1:3 with lines linecolor rgb "royalblue" linewidth 3 title "Errorfuntion approximation", \
    "error.data" using 1:2 with lines linecolor rgb "orange" linewidth 3 title "Errorfunction with integral", \
    "errorTabulated.data" using 1:2 with points pt 7 linecolor rgb "red" linewidth 3 title "Tabulated errorfunction values", \