set terminal svg background "white"
set key left
set output "Dgdx.svg"
set xlabel "x"
set ylabel "dg(x)/dx"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "dg(x)/dx"

plot\
    "dgdx.data" using ($1):($2) with lines linecolor rgb "royalblue" linewidth 3 title "Interpolation", \
    "dgdx.data" using ($1):($3) with lines dashtype 2 linecolor rgb "orange" linewidth 3 title "dg(x)/dx", \
    # "trainingPoints.txt" using ($1):($2) with points linecolor rgb "red" linewidth 3 title "Training points", \