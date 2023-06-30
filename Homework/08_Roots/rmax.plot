set terminal svg background "white"
set key bottom right
set output "rmax.svg"
set xlabel "r_{max}"
set ylabel "|E_{num}-E_{anal}|/E_{anal}"
set tics out
set samples 800
set title "Convergence as function of r_{max}"

plot [6:10] \
    "rmax.data" using 1:2 with lines linecolor rgb "royalblue" linewidth 3 title "r_{max} convergence", \