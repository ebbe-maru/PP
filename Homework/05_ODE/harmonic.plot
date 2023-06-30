set terminal svg background "white"
set key top left
set output "harmonic.svg"
set xlabel "t"
set ylabel "Amplitude"
set tics out
set samples 800
set title "Harmonic oscillator u''=-u with u(0)=0, u'(0)=1"
set yrange [-1.2 : 1.2]
plot [0:10] \
    "harmonic.data" using 1:2 with lines linecolor rgb "red" linewidth 3 title "Solution"
