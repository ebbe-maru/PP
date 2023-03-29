DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/PP/matlib/matrix
Out.txt   :   main.exe
	mono main.exe > Out.txt

main.exe : main.cs QRGS.exe matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

QRGS.exe: QRGS.cs matrix.dll
	mcs -t:exe $(DLLS) $(CODE)

matrix.dll: $(HOME)$(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^