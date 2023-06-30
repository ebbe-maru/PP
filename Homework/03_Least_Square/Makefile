DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
firstTarget: Out.txt decay.svg

Out.txt decay.data fit.data: main.exe
	mono $< 1> Out.txt

main.exe: main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll : vector.cs matrix.cs
	mcs -target:library -out:$@ $^

decay.svg: decay.plot decay.data fit.data
	gnuplot $<

clean:
	rm --force Out.txt main.exe matrix.dll fit.data decay.data Decay.svg