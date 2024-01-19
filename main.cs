using System;
using System.IO;

using typeAttempt;
using Variables;
using Functions;

class Program
{
    // CALL from I/O is located here
    // Control Functions are located here
    public static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines(args[0]); // Load Program 
        int index = 0; // Current Line (Index)
        string[] parts = { "", "" }; // Parts From Current Line (Index) null

        while (true)
        {
        RSRT:; // Required for any function that needs to return to the start of the loop
               // Mainly used to keep index at current number
            try
            {
                parts = lines[index].Split(";");
            }
            catch
            {
                break;
            }

            switch (parts[0])
            {
                // Control
                case "IF": // Check If Two Values Are (x)
                    dynamic left = null, right = null;
                    // Check for vars on left input
                    if (parts[1].StartsWith("$")) left = IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0, 1))];
                    else if (parts[1].StartsWith("@")) left = StrVar.strVals[StrVar.strNames.IndexOf(parts[1].Remove(0, 1))];
                    else if (parts[1].StartsWith("*")) left = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1].Remove(0, 1))];
                    //If no vars, attempt to convert left input into data type
                    else
                    {
                        if (TypeAttempt.Int(parts[1])) left = int.Parse(parts[1]);
                        else if (TypeAttempt.Float(parts[1])) right = float.Parse(parts[1]);
                        else left = parts[1];
                    }
                    // Check for vars on right input
                    if (parts[3].StartsWith("$")) right = IntVar.intVals[IntVar.intNames.IndexOf(parts[3].Remove(0, 1))];
                    else if (parts[3].StartsWith("@")) right = StrVar.strVals[StrVar.strNames.IndexOf(parts[3].Remove(0, 1))];
                    else if (parts[3].StartsWith("*")) right = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[3].Remove(0, 1))];
                    //If no vars, attempt to convert right input into data type
                    else
                    {
                        if (TypeAttempt.Int(parts[3])) right = int.Parse(parts[3]);
                        else if (TypeAttempt.Float(parts[3])) right = float.Parse(parts[3]);
                        else right = parts[3];
                    }

                    switch (parts[2])
                    {
                        case "=":
                            if (left == right)
                            {
                                index = int.Parse(parts[4]) - 1;
                                goto RSRT;
                            }
                            break;
                        case ">":
                            if (left > right)
                            {
                                index = int.Parse(parts[4]) - 1;
                                goto RSRT;
                            }
                            break;
                        case "<":
                            if (left < right)
                            {
                                index = int.Parse(parts[4]) - 1;
                                goto RSRT;
                            }
                            break;
                    }
                    break;

                case "GOTO": // Goto Line Number
                    index = int.Parse(parts[1]) - 1;
                    goto RSRT;

                case "GLBL": // Goto Label
                    foreach (string line in lines)
                    {
                        index = 0;
                        string[] gParts = line.Split(";");
                        if (gParts[0] == "LBL")
                        {
                            if (gParts[1] == parts[1]) goto RSRT;
                        }
                        index++;
                    }
                    break;

                case "LBL": // Label
                    break;


                // IO
                case "CALL": // Start DyBASIC File From Another File
                    if (parts[1].StartsWith("@")) lines = File.ReadAllLines(StrVar.strVals[StrVar.strNames.IndexOf(parts[1].Remove(0, 1))] + ".dybsc");
                    else lines = File.ReadAllLines(parts[1] + ".dybsc");
                    if (parts[2].StartsWith("$")) index = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0, 1))] - 1;
                    else index = int.Parse(parts[2]) - 1;
                    goto RSRT;
            }
            Functions.VariableFunctions.Functions(parts);
            Functions.ConsoleFunctions.Functions(parts);
            Functions.IOFunctions.Functions(parts);
            index++;
        }
    }
}
