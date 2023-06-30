set terminal svg background "white"
set key top left
stats "error.data" nooutput
set output "error.svg"
set xlabel "N"
set ylabel "Sigma"
set tics out
set samples 800
set title "Error dependence on samping points"
set logscale xy


plot  \
    "error.data" using 1:2 with lines linecolor rgb "orange" linewidth 3 title "Plain MC error", \
    "error.data" using 1:3 with lines linecolor rgb "royalblue" linewidth 3 title "Quasi random MC error", \
    "error.data" using 1:4 with lines dashtype 2 linecolor rgb "black" linewidth 3 title "1/sqrt(N)"