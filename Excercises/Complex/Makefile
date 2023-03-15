Out.txt   :   main.exe
	mono main.exe > Out.txt

#DIR = $(HOME)/pra_pro/PP/matlib/complex 
cmath.dll : cmath.cs complex.cs
	mcs cmath.cs complex.cs -target:library -out:cmath.dll

main.exe : main.cs cmath.dll
	mcs main.cs -reference:cmath.dll -target:exe -out:main.exe