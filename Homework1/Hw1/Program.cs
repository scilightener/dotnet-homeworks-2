﻿using Hw1;

Parser.ParseCalcArguments(args, out var arg1, out var operation, out var arg2);
var result = Calculator.Calculate(arg1, operation, arg2);
Console.WriteLine(result);