DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
firstTarget: Out.txt harmonic.svg damped.svg lotka.svg threebody.gif

Out.txt harmonic.data damped.data lotka.data threebody.data: main.exe
	mono $< 1>Out.txt

main.exe: main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll: vector.cs matrix.cs
	mcs -target:library -out:$@ $^

harmonic.svg: harmonic.plot harmonic.data
	gnuplot $<

damped.svg: damped.plot damped.data
	gnuplot $<

lotka.svg: lotka.plot lotka.data lotkaend.data
	gnuplot $<

threebody.gif: threebody.plot threebody.data
	gnuplot $<	

clean:
	rm --force Out.txt main.exe matrix.dll harmonic.data harmonic.svg \
	damped.data damped.svg lotka.data lotka.svg lotkaend.data \
	threebody.data threebody.gif