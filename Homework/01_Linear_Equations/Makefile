DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)


Out.txt   :   main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll: matrix.cs vector.cs
	mcs -target:library -out:./matrix.dll $^

clean:
	rm --force Out.txt main.exe matrix.dll\