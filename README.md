# The DyBASIC Interpreter

## Table of Contents
* [Good Practices](#good-practices-with-dybasic)
* [Variables](#variables)
    * [How to declare variables](#to-delcare-variables)
    * [Variable Calling](#variable-calling)
    * [The `RAND` Function](#the-rand-function)
    * [The `MATH` Function](#the-math-function)
* [Control](#control)
    * [The `IF` Function](#the-if-function)
    * [The `GOTO` Function](#the-goto-function)
* [I/O](#io)
    * [The `STRIN` Function](#the-strin-function)
    * [The `INTIN` Function](#the-intin-function)
    * [The `FLTIN` Function](#the-fltin-function)
* [Console](#console)
    * [The `PRINT` Function](#the-print-function)  

## Good Practices with DyBASIC
* Comments work here as they do in BrainF, however it's a good idea to start them with `;;` to make them easily recognizable.
* All "useless" variables (i.e. variables only used for input and nothing more) should be started with an underscore `_`.
* Variables that are meant to be constant should be put at the top and started with a tilda `~`.

## Variables

#### To delcare variables:
* Strings `STR;(name);(value)`
* Intergers `INT;(name);(value)`
* Floats `FLT;(name);(value)`
#### Variable Calling
In most cases, functions only use one variable type and in turn only need the variable name. Some functions can use more than one variable type as input, in that case, use:
* Strings `@`
* Intergers `$`
* Floats `*`
### The `RAND` function
Stores a psuedo-random integer into an `$INT` variable
`RAND;($value)`
### The `MATH` function
Adds, subtracts, multiplies, and divides `$INT` and `*FLT` variables.
`MATH;($*value1);(+,-,*,/);($*value2)`
* Both values **must** be the same type.

## Control
### The `IF` function
Goes to the line number in the code if the two values are equal, greater than, or less than each other.
`IF;(@$*value1);(=,>,<);(@$*value2);(completion line)`
* Both values **must** be the same type.
* `completion line` is the line of the file to go to if the check succeds.
  * It is a good idea to put a `GOTO` function right after the `IF` function fails it's check.
### The `GOTO` function
Goes to the line number in the code that was specifyied.
`GOTO;(value)`
* `value` can take both raw and `$INT` input.

## I/O
### The `STRIN` Function
Stores `@STR` input into a variable
`STRIN;(@value)`
### The `INTIN` Function
Stores `$INT` input into a variable
`STRIN;($value)`
### The `FLTIN` Function
Stores `*FLT` input into a variable
`STRIN;(*value)`

## Console
### The `PRINT` Function
Prints the input(s) onto the terminal
`PRINT:(@$*value1);(@$*value2);(@$*value3);(@$*valueETC..)`
* The `PRINT` function can take in raw `@STR` values.
* The `PRINT` function has infinite input values.
   * if you wanted to print the output of a calculator, you could do `PRINT;Answer is ;$ans`.
