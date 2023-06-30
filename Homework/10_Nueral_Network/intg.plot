set terminal svg background "white"
set key left
set output "Intg.svg"
set xlabel "x"
set ylabel "int g(x)"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "int g(x)"

plot\
    "intg.data" using ($1):($2) with lines linecolor rgb "royalblue" linewidth 3 title "Interpolation", \
    "intg.data" using ($1):($3) with lines dashtype 2 linecolor rgb "orange" linewidth 3 title "\int g(x)", \
    # "trainingPoints.txt" using ($1):($2) with points linecolor rgb "red" linewidth 3 title "Training points", \

