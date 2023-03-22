N=1e8
TIME = time --portability --append --output $@
Out.txt : main.exe Makefile
	> $@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	echo "" >> $@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	echo "" >> $@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:4 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:5 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:6 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:7 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:8 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:9 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:10 >>$@
	echo  >> $@
main.exe : main.cs
	mcs main.cs

outparallelFor.txt: parallelfor.exe
	> $@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	echo "" >> $@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	echo "" >> $@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:4 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:5 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:6 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:7 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:8 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:9 >>$@
	echo  >> $@
	$(TIME) mono $< -terms:$N -threads:10 >>$@
	echo  >> $@
parallelfor.exe : parallelfor.cs
	mcs parallelfor.cs

clean:                          
	rm --force Out.txt main.exe