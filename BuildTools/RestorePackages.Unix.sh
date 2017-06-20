#!/bin/sh

for dest in $(find .. -name "*.sln" -print)
do
  mono nuget.exe restore $dest
  #statements
done
