DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
#DIR = ../../matrix
firstTarget: Out.txt E_0.svg s_waves.svg

Out.txt   :   main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll: matrix.cs vector.cs
	mcs -target:library -out:./matrix.dll $^

E_0.svg: E_0.plot dr.data rmax.data
	gnuplot $<

s_waves.svg: s_waves.plot s_waves.data
	gnuplot $<


clean:
	rm --force Out.txt main.exe matrix.dll dr.data E_0.svg