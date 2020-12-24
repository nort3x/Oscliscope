system("mv time1.dat c.dat")
set yrange[0:5.5]
plot "c.dat" using 1:2 with lines
pause 0.1
reread
