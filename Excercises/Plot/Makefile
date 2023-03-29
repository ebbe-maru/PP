DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/PP/matlib

#Erf________________________________________
erf.svg: erf.plot erf_tab.data erf_calc.data
	gnuplot $<

erf_calc.data: erf.exe
	mono $< > $@

erf.exe: erf.cs spfun.dll
	mcs -t:exe $(DLLS) $(CODE)

#gamma______________________________________________
gamma.svg: gamma.plot factorials.data gamma_calc.data
	gnuplot $<

gamma_calc.data: gamma.exe
	mono $< > $@

gamma.exe: gamma.cs spfun.dll
	mcs -t:exe $(DLLS) $(CODE)

#lnGamme______________________________________________
lngamma.svg: lngamma.plot factorials.data lngamma_calc.data
	gnuplot $<

lngamma_calc.data: lngamma.exe
	mono $< > $@

lngamma.exe: lngamma.cs spfun.dll
	mcs -t:exe $(DLLS) $(CODE)

#Complex Gamma_________________________________________
cgamma.svg: cgamma.plot cgamma.data
	gnuplot $<

cgamma.data: cgamma.exe
	mono $< > $@

cgamma.exe: cgamma.cs spfun.dll
	mcs -t:exe $(DLLS) $(CODE)

spfun.dll : $(DIR)/spfun.cs $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./spfun.dll $^

clean:
	$(RM) *.exe *.dll *.svg *calc.data main