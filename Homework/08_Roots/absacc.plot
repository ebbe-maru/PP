set terminal svg background "white"
set key bottom right
set output "absacc.svg"
set xlabel "Absolute accuracy"
set ylabel "|E_{num}-E_{anal}|/E_{anal}"
set tics out
set samples 800
set title "Convergence as function of absolute accuracy"
set logscale xy

plot \
    "absacc.data" using 1:2 with lines linecolor rgb "royalblue" linewidth 3 title "Abs accuracy convergence"