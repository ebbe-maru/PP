set terminal svg background "white"
set key top right
set output "SeveralRadial.svg"
set xlabel "r"
set ylabel "|f^{(k)}(r)|^2"
set tics out

set xzeroaxis
set yzeroaxis
set samples 800
set title "Radial functions"
# set xrange [1.5:0]
# set yrange [-0.6:-0.1]
plot \
    "s_waves.data" using ($1):($2) with lines linecolor rgb "royalblue" linewidth 3 title "1s numerical", \
    "s_waves.data" using ($1):($5) with lines dashtype 2 linecolor rgb "light-cyan" linewidth 3 title "1s theory", \
    "s_waves.data" using ($1):($3) with lines linecolor rgb "red" linewidth 3 title "2s numerical", \
    "s_waves.data" using ($1):($6) with lines dashtype 2 linecolor rgb "purple" linewidth 3 title "2s theory", \

