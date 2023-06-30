set terminal gif animate delay 2
set output 'threebody.gif'
stats "threebody.data" nooutput

data = "threebody.data"


# Set the range for the x and y axes
set xrange [-1.5:1.5]
set yrange [-1:1]


#Set up the initial plot
plot data using 2:3 every ::0::0 with points pt 7 lc rgb "forest-green" notitle, \
     data using 6:7 every ::0::0 with points pt 7 lc rgb "orange" notitle, \
     data using 10:11 every ::0::0 with points pt 7 lc rgb "royalblue" notitle, \
     data using 2:3 every ::0::0 with lines lc rgb "forest-green" notitle, \
     data using 6:7 every ::0::0 with lines lc rgb "orange" notitle, \
     data using 10:11 every ::0::0 with lines lc rgb "royalblue" notitle

#loop through the data and update the plot
do for [i=1:int(STATS_records)-1]{
    plot data using 2:3 every ::i::i with points pt 7 ps 2 lc rgb "forest-green" notitle, \
         data using 6:7 every ::i::i with points pt 7 ps 2 lc rgb "orange" notitle, \
         data using 10:11 every ::i::i with points pt 7 ps 2 lc rgb "royalblue" notitle, \
         data using 2:3 every ::0::i with lines lw 2 lc rgb "forest-green" notitle, \
         data using 6:7 every ::0::i with lines lw 2 lc rgb "orange" notitle, \
         data using 10:11 every ::0::i with lines lw 2 lc rgb "royalblue" notitle
}
