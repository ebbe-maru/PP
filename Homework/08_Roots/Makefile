DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

firstTarget: Out.txt Wavefunction.svg rmax.svg rmin.svg absacc.svg epsacc.svg

Out.txt wavefunction.data rmax.data rmin.data absacc.data epsacc.data: main.exe
	mono $< 1>Out.txt

main.exe: main.cs library.dll
	mcs -t:exe $(DLLS) $(CODE)
	
Wavefunction.svg: wavefunction.plot wavefunction.data
	gnuplot $<

rmax.svg: rmax.plot rmax.data
	gnuplot $<

rmin.svg: rmin.plot rmin.data
	gnuplot $<

absacc.svg: absacc.plot absacc.data
	gnuplot $<

epsacc.svg: epsacc.plot epsacc.data
	gnuplot $<

library.dll: vector.cs matrix.cs QRGS.cs ode.cs
	mcs -target:library -out:$@ $^

clean:
	rm --force Out.txt main.exe library.dll\
		wavefunction.data wavefunction.svg\
		rmax.data rmax.svg\
		rmin.data rmin.svg\
		absacc.data absacc.svg\
		epsacc.data epsacc.svg\