set terminal svg background "white"
set key left
set output "Decay.svg"
set xlabel "Time in days"
set ylabel "Activity in percent"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Decay of element ThX"

plot [0:17] \
    "decay.data" using ($1):($2):($3) with errorbars title "Measured activity", \
    "fit.data" using 1:2 with lines title "Fitted function", \
    "fit.data" using 1:3 with lines title "Fitted function -uncertainty", \
    "fit.data" using 1:4 with lines title "Fitted function +uncertainty"