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
gamma.svg: gamma.plot gamma_tab.data gamma_calc.data
	gnuplot $<

gamma_calc.data: gamma.exe
	mono $< > $@

gamma.exe: gamma.cs spfun.dll
	mcs -t:exe $(DLLS) $(CODE)

spfun.dll : $(DIR)/spfun.cs
	mcs -target:library -out:./spfun.dll $^

clean:
	$(RM) *.exe *calc.dll *.svg *.data main