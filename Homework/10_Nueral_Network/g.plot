set terminal svg background "white"
set key left
set output "G.svg"
set xlabel "x"
set ylabel "y"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "g(x)=cos(5*x-1)*exp(-x*x)"

plot\
    "g.data" using ($1):($2) with lines linecolor rgb "royalblue" linewidth 3 title "Interpolation", \
    "g.data" using ($1):($3) with lines dashtype 2 linecolor rgb "orange" linewidth 3 title "g(x)", \
    "trainingPoints.txt" using ($1):($2) with points linecolor rgb "red" linewidth 3 title "Training points", \