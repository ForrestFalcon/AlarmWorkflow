FOR /R "..\" %%G IN (*.sln) DO NuGet.exe restore "%%G"
