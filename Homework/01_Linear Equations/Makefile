DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/PP/matlib/matrix

Out.txt   :   main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^