set terminal svg background "white"
set key bottom right
set output "rmin.svg"
set xlabel "r_{min}"
set ylabel "|E_{num}-E_{anal}|/E_{anal}"
set tics out
set samples 800
set title "Convergence as function of r_{min}"
set logscale y

plot [0:0.1] \
    "rmin.data" using 1:2 with lines linecolor rgb "royalblue" linewidth 3 title "r_{min} convergence", \