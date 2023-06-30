DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
firstTarget: Out.txt linspline.svg qspline.svg sinB.svg

Out.txt linspline.data qspline.data sinBData.data sinBsplineData.data: main.exe
	mono $< 1>Out.txt

main.exe: main.cs matrix.dll spline.dll
	mcs -t:exe $(DLLS) $(CODE)

spline.dll: linspline.cs qspline.cs matrix.dll
	mcs -target:library -out:$@ $(CODE) \
		$(DLLS)

matrix.dll : vector.cs matrix.cs
	mcs -target:library -out:$@ $^

linspline.svg: linspline.plot linspline.data
	gnuplot $<

qspline.svg: qspline.plot qspline.data
	gnuplot $<

sinB.svg: sinB.plot sinBData.data sinBsplineData.data
	gnuplot $<

clean:
	rm --force Out.txt main.exe matrix.dll spline.dll linspline.data linspline.svg \
	qspline.data qspline.svg sinB.svg sinBData.data sinBsplineData.data