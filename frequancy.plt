system("mv freq1.dat g.dat")
set yrange[0:1]

set term qt 1 noraise

set grid
set ylabel 'Amplitude (Normalized)' tc rgb 'white'
set xlabel 'Frequency (Hz)' tc rgb 'white'
set border lc rgb 'white'
set key tc rgb 'white'

set linetype 1 lc rgb 'white'



plot "g.dat" using 1:2 w l
pause 0.1
reread
