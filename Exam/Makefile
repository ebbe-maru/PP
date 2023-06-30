DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

firstTarget: Out.txt Higgs.svg

Out.txt fit.data: main.exe
	mono $< < Higgs.data 1> Out.txt

main.exe: main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

Higgs.svg: Higgs.plot Higgs.data fit.data
	gnuplot $<

matrix.dll: vector.cs matrix.cs
	mcs -target:library -out:$@ $^

clean:
	rm --force Out.txt main.exe matrix.dll\
			Higgs.svg fit.data