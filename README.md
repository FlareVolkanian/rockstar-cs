# RockStar CS

### A RockStar interpreter and maybe C# transpiler written in C# and Fastpass-Grammer.

This will by no means be a production grade interpreter but I enjoy writing toy-compilers and interpreters
so it should work reasonably well but it may give somewhat ambigous error messages depending on the type of error.

This is a work in progress and currently only supports a subset of the RockStar language and won't be very usefull at the moment.

Currently I am using a tool I wrote called Fastpass to generate the Parsing/Parser.cs file from Parsing/Grammer.fpg, I will release Fastpass opensource soon.

When I have it working I will make an executable available to bypass the need to compile from source.

___

The idea is to make it work primarily as an interpreter and secondarily as a C# transpiler but if transpilation
proves too difficult it will be scrapped.