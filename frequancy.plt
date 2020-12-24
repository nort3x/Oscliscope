system("mv freq1.dat g.dat")
plot "g.dat" using 1:2 w l
pause 0.01
reread
