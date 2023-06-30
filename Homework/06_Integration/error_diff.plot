set terminal svg background "white"
set key bottom right
set output "error_diff.svg"
set xlabel "x"
set ylabel "|erf_integral - error_tab|^2 - |erf_approx - error_tab|^2"
set tics out
set samples 800
set title "Error functions comparison"

plot [0:3] \
    "error_diff.data" using 1:4 with points pt 7 linecolor rgb "red" linewidth 3