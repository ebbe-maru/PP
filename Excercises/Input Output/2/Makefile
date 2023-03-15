# Exercise 2
Out.txt: main.exe
	echo 1 2 3 4 5 | mono main.exe > Out.txt

# Alternative to exercise 2
# Out.txt: main.exe
#	echo 1 2 3 4 5 > input.txt
#	mono main.exe < input.txt > Out.txt
# Out.txt: main.exe
# 	mono main.exe -input:input.txt -output:Out.txt

main.exe: main.cs
	mcs main.cs
	

clean:                          
	rm --force Out.txt main.exe