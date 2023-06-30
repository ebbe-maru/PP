DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

firstTarget: Out.txt G.svg Dgdx.svg D2gdx2.svg Intg.svg

Out.txt g.data dgdx.data d2gdx2.data intg.data: main.exe
	mono $< 1>Out.txt


main.exe: main.cs library.dll
	mcs -t:exe $(DLLS) $(CODE)

G.svg: g.plot g.data
	gnuplot $<

Dgdx.svg: dgdx.plot dgdx.data
	gnuplot $<

D2gdx2.svg: d2gdx2.plot d2gdx2.data
	gnuplot $<

Intg.svg: intg.plot intg.data
	gnuplot $<

library.dll: vector.cs matrix.cs ann.cs
	mcs -target:library -out:$@ $^

clean:
	rm --force Out.txt main.exe library.dll\
		g.data G.svg dgdx.data Dgdx.svg\
		d2gdx2.data D2gdx2.svg intg.data Intg.svg
