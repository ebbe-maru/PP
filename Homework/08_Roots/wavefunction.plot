set terminal svg background "white"
set key below
set output "Wavefunction.svg"
set xlabel "r"
set ylabel "F_E(r)"
set tics out
set samples 800
set title "Hydrogen S-wave F_E(r_{max})=0"

plot [0:10] \
    "wavefunction.data" using 1:2 with lines linecolor rgb "royalblue" linewidth 3 title "r_{max}=8", \
    "wavefunction.data" using 1:3 with lines linecolor rgb "orange" linewidth 3 title "Analytical", \