set terminal svg
set terminal svg background "white"
set output "UnitCircle.svg"
set key top right
set xlabel "N"
set logscale x 10
set xzeroaxis
set xrange [-0.1:200000]
set ylabel "Area"
set tics out
set grid
set title "Monte Carlo approximaton of area of unit circle"
plot "unitC.data" using ($2):($3) with point pt 7 ps 0.6 lc rgb "web-blue" title "Pseudo-random"\
, "unitC.data" using ($2):($3) with lines lc rgb "web-blue" notitle\
, "unitC.data" using ($2):($7) with point pt 7 ps 0.6 lc rgb "web-green" title "Quasi-random "\
, "unitC.data" using ($2):($7) with lines lc rgb "web-green" notitle\