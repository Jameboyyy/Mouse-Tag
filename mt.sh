#/!/bash

echo "First remove old binary files"
rm *.dll
rm *.exe

echo "View the list of source files"
ls -l

echo "Compile mousetagform.cs to create the file: mousetagform.dll"
mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:mousetagform.dll mousetagform.cs

echo "Compile mousetagmain.cs and link the previously created dll file to create an exe file"
mcs -r:System -r:System.Windows.Forms -r:mousetagform.dll -out:rats.exe mousetagmain.cs

echo "========= Program Running! ========="
./rats.exe

echo "========= Program Finished! ========="
