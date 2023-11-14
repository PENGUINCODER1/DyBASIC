# The DyBASIC (v1.0) Interpreter

## Table of Contents
* [Good Practices](#good-practices-with-dybasic)
* [Variables](#variables)
    * [Variable Declaration](#variable-declaration)
    * [Variable Calling](#variable-calling)
    * [The `RAND` Function](#the-rand-function)
    * [The `MATH` Function](#the-math-function)
    * [The `DROP` Function](#the-drop-function)
* [Control](#control)
    * [The `IF` Function](#the-if-function)
    * [The `GOTO` Function](#the-goto-function)
* [I/O](#io)
    * [The `STRIN` Function](#the-strin-function)
    * [The `INTIN` Function](#the-intin-function)
    * [The `FLTIN` Function](#the-fltin-function)
    * [The `CALL` Function](#the-call-function)
* [Console](#console)
    * [The `PRINT` Function](#the-print-function)
    * [The `PRNTN` Function](#the-prntn-function)
    * [The `CRSR` Function](#the-crsr-function)
    * [The `CLEAR` Function](#the-clear-function)
    * [The `SLEEP` Function](#the-sleep-function)
    * [The `HOLD` Function](#the-hold-function)
    * [The `QUIT` Function](#the-quit-function)

## Good Practices with DyBASIC
* Comments work here as they do in BrainF, however it's a good idea to start them with `;;` to make them easily recognizable.
* All "useless" variables (i.e. variables only used for input and nothing more) should be started with an underscore `_`.
* Variables that are meant to be constant should be put at the top and started with a tilda `~`.
* Although you can have two different types of variables with the same exact name, refrian from doing this unless it makes logical sense.

## Variables
Anything Relating to Variables.
### Variable Declaration
* Strings `STR;(name);([@RAW]value)`
* Intergers `INT;(name);([$RAW]value)`
* Floats `FLT;(name);([*RAW]value)`
### Variable Calling
In most cases, functions only use one variable type and in turn only need the variable name. Some functions can use more than one variable type as input, in that case, use:
* Strings `@`
* Intergers `$`
* Floats `*`
### The `RAND` Function
Stores a psuedo-random integer into an `$INT` variable.
`RAND;($value);($[$RAW]lower);($[$RAW]upper)`
### The `MATH` Function
Adds, subtracts, multiplies, and divides `$INT` and `*FLT` variables.
`MATH;($*[$*RAW]value1);(+,-,*,/);($*[$*RAW]value2)`
* Both values **must** be the same type.
### The `DROP` Function
Drops the variable from memory.
`DROP;@$*variable`

## Control
Anything Related to Control Flow.
### The `IF` Function
Goes to the line number in the code if the two values are equal, greater than, or less than each other.
`IF;(@$*[@$*RAW]value1);(=,>,<);(@$*[@$*RAW]value2);([$RAW]completion line)`
* Both values **must** be the same type.
* `completion line` is the line of the file to go to if the check succeeds.
  * It is a good idea to put a `GOTO` function right after the `IF` function fails it's check.
### The `GOTO` Function
Goes to the line number in the code that was specifyied.
`GOTO;($[$RAW]value)`
* `value` can take both raw and `$INT` input.

## I/O
Anything Related to User Input and Files.
### The `STRIN` Function
Stores `@STR` input into a variable.
`STRIN;(@value)`
### The `INTIN` Function
Stores `$INT` input into a variable.
`STRIN;($value)`
### The `FLTIN` Function
Stores `*FLT` input into a variable.
`STRIN;(*value)`
### The `CALL` Function
Calls a `.dybsc` file and loads it in starting at the `startline`.
`CALL;(@[@RAW]filename);($[$RAW]startline)`
* Do not include the `.dybsc` file extension in `filename`.
* All variables from the previous program are still loaded into memory and can be accessed by the new one.

## Console
Anything Related to the Console/Terminal.
### The `PRINT` Function
Prints the input(s) onto the terminal, and then creates a newline.
`PRINT;(@$*[@RAW]value1);(@$*[@RAW]value2);(@$*[@RAW]value3);(@$*[@RAW]valueETC..)`
* The `PRINT` function can take in raw `@STR` values.
* The `PRINT` function has infinite input values.
   * if you wanted to print the output of a calculator, you could do `PRINT;Answer is ;$ans`.
### The `PRNTN` Function
Prints the input(s) onto the terminal.
`PRNTN;(@$*[@RAW]value1);(@$*[@RAW]value2);(@$*[@RAW]value3);(@$*[@RAW]valueETC..)`
* The `PRNTN` function can take in raw `@STR` values.
* The `PRNTN` function has infinite input values.
   * if you wanted to print the output of a calculator, you could do `PRNTN;Answer is ;$ans`.
### The `CRSR` Function
Moves the cursor to the wanted position.
`CRSR;($[$RAW]x);($[$RAW]y)`
### The `CLEAR` Function
Clears the terminal.
`CLEAR`
### The `SLEEP` Function
Waits for x amount of milliseconds.
`SLEEP;($[$RAW]milliseconds)`
### The `HOLD` Function
Waits until the user presses a key.
`HOLD`
### The `QUIT` Function
Quits the program.
`QUIT`
