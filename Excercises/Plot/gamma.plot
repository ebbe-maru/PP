set terminal svg background "white"
set key bottom right
set output "gamma.svg"
set xlabel "n"
set ylabel "gamma(x)"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set key left 
set title "Error function"
plot [0:7][0:800] \
    "gamma_calc.data" with lines title "Gamma(n+1)"\
    , "gamma_tab.data" using ($1):($2) with points pointtype 4 title "n!"\