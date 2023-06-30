set terminal svg background "white"
set key bottom right
set output "epsacc.svg"
set xlabel "Relative accuracy"
set ylabel "|E_{num}-E_{anal}|/E_{anal}"
set tics out
set samples 800
set title "Convergence as function of relative accuracy"
set logscale xy

plot \
    "epsacc.data" using 1:2 with lines linecolor rgb "royalblue" linewidth 3 title "Relative accuracy convergence"