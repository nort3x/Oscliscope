system("mv time1.dat c.dat")
set yrange[0:5.5]
set grid
set term qt 1 noraise


set xlabel 'Time (sec)' tc rgb 'white'
set ylabel 'Voltage (Volt)' tc rgb 'white'
set border lc rgb 'white'
set key tc rgb 'white'

set linetype 1 lc rgb 'white'

plot "c.dat" using 1:2 with lines lc "white"
pause 0.1
reread
