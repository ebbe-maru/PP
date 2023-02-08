Out.txt   :   main.exe
	mono main.exe > Out.txt

spfun.dll : spfun.cs
	mcs spfun.cs -target:library -out:spfun.dll

main.exe : main.cs spfun.dll
	mcs main.cs -reference:spfun.dll -target:exe -out:main.exe