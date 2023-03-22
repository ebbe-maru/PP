Out.txt: main.exe
	cat input.txt | mono main.exe > Out.txt 

genlist.dll : genlist.cs	
	mcs genlist.cs -target:library -out:genlist.dll
	
main.exe : main.cs genlist.dll
	mcs -reference:genlist.dll main.cs

clean:                           
	rm --force Out.txt main.exe genlist.dll