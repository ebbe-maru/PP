set terminal svg background "white"
set key left
set output "D2gdx2.svg"
set xlabel "x"
set ylabel "d^2g(x)/dx^2"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "d^2g(x)/dx^2"

plot\
    "d2gdx2.data" using ($1):($2) with lines linecolor rgb "royalblue" linewidth 3 title "Interpolation", \
    "d2gdx2.data" using ($1):($3) with lines dashtype 2 linecolor rgb "orange" linewidth 3 title "d^2g(x)/dx^2", \
    # "trainingPoints.txt" using ($1):($2) with points linecolor rgb "red" linewidth 3 title "Training points", \
