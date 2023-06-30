DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

firstTarget: Out.txt error.svg UnitCircle.svg

Out.txt error.data: main.exe
	mono $< 1>Out.txt

main.exe: main.cs vector.dll
	mcs -t:exe $(DLLS) $(CODE)

error.svg: error.plot error.data UnitError.svg
	gnuplot $<

UnitCircle.svg: unitC.plot unitC.data
	gnuplot $<

UnitError.svg: unitError.plot unitC.data
	gnuplot $<

vector.dll: vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm --force Out.txt main.exe vector.dll \
		error.data error.svg