Out.txt   :   main.exe
	mono main.exe > Out.txt

vec.dll: vec.cs
	mcs vec.cs -target:library -out:vec.dll

main.exe : main.cs vec.dll
	mcs main.cs -reference:vec.dll vec.cs -target:exe -out:main.exe