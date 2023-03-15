# Exercise 1
Out.txt: main.exe
	mono main.exe -numbers:1,2,3,4,5 -input:input.txt -output:Out.txt > Out.txt

# Alternative to Exercise 1
# Out.txt: main.exe
#	echo "-numbers:1,2,3,4,5" > inputfile
#	mono main.exe $(cat inputfile) > Out.txt

# Exercise 2
# Out.txt: main.exe
#	echo 1 2 3 4 5 | mono main.exe > Out.txt

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