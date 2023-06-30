set terminal svg background "white"
set key below
set output "Higgs.svg"
set xlabel "E [GeV]"
set ylabel "Signal"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Higgs signal"

plot\
    "Higgs.data" using ($1):($2):($3) with errorbars title "Signal", \
    "fit.data" using 1:2 with lines linecolor rgb "royalblue" linewidth 3 title "Breit-Wigner fit", \