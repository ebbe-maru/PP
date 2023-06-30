set terminal svg
set terminal svg background "white"
set output "UnitError.svg"
set key top right
set xlabel "N"
set logscale x 10
set logscale y 10
set xzeroaxis
set xrange [0.8:120000]
set tics out
set grid
set title "Error dependance of sampling points N for plain Monte Carlo integration"
plot "unitC.data" using ($2):($4) with point pt 7 ps 0.6 lc rgb "web-blue" title "Estimated error"\
, "unitC.data" using ($2):($5) with point pt 7 ps 0.6 lc rgb "purple" title "Real error"\
,"unitC.data" using ($2):($6) with lines lw 3 lc rgb "forest-green" title "1/Sqrt(N)"\