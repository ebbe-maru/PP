set terminal svg background "white"
set key top left
set output "damped.svg"
set xlabel "t"
set ylabel "Amplitude"
set tics out
set samples 800
set title "Damped oscillator: theta''(0)=PI-0.1 and theta'(0)= 0"

plot [0:10] \
    "damped.data" using 1:2 with lines linecolor rgb "orange" linewidth 3 title "Theta(t)", \
    "damped.data" using 1:3 with lines linecolor rgb "purple" linewidth 3 title "Theta'(t)", \