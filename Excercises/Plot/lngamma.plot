set terminal svg background "white"
set key bottom right
set output "lngamma.svg"
set xlabel "n"
set ylabel "Exp(lngamma(x))"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set key left 
set title "lnGamma function"
plot [0:7][0:800] \
    "lngamma_calc.data" with lines title "Exp(lnGamma(n+1))"\
    , "factorials.data" using ($1):($2) with points pointtype 4 title "n!"\