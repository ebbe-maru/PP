DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

firstTarget: Out.txt error.svg error_diff.svg

Out.txt error.data: main.exe
	mono $< 1>Out.txt

main.exe: main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll: vector.cs matrix.cs
	mcs -target:library -out:$@ $^

error.svg: error.plot error.data
	gnuplot $<

error_diff.svg: error_diff.plot error_diff.data
	gnuplot $<

clean:
	rm --force Out.txt main.exe matrix.dll error.data error.svg 