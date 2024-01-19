using System;
using Variables;
using typeAttempt;

namespace Functions
{
    public static class VariableFunctions
    {
        public static void Functions(string[] parts)
        {
            switch (parts[0])
            {
                case "STR": // String Type
                    if (StrVar.strNames.Contains(parts[1])) // Check if the variable already exists
                    {
                        StrVar.strVals[StrVar.strNames.IndexOf(parts[1])] = parts[2];
                    }
                    else
                    {
                        StrVar.strNames.Add(parts[1]);
                        StrVar.strVals.Add(parts[2]);
                    }
                    break;

                case "INT": // Int Type
                    if (IntVar.intNames.Contains(parts[1])) // Check if the variable already exists
                    {
                        IntVar.intVals[IntVar.intNames.IndexOf(parts[1])] = int.Parse(parts[2]);
                    }
                    else
                    {
                        IntVar.intNames.Add(parts[1]);
                        IntVar.intVals.Add(int.Parse(parts[2]));
                    }
                    break;

                case "FLT": // Float Type
                    if (FltVar.fltNames.Contains(parts[1])) // Check if the variable already exists
                    {
                        FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1])] = float.Parse(parts[2]);
                    }
                    else
                    {
                        FltVar.fltNames.Add(parts[1]);
                        FltVar.fltVals.Add(float.Parse(parts[2]));
                    }
                    break;

                case "DROP": // Drops (x) From Memory
                    if (parts[1].StartsWith("@"))
                    {
                        StrVar.strVals.Remove(StrVar.strVals[StrVar.strNames.IndexOf(parts[1].Remove(0, 1))]);
                        StrVar.strNames.Remove(parts[1].Remove(0, 1));
                    }
                    else if (parts[1].StartsWith("$"))
                    {
                        IntVar.intVals.Remove(IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0, 1))]);
                        IntVar.intNames.Remove(parts[1].Remove(0, 1));
                    }
                    else if (parts[1].StartsWith("*"))
                    {
                        FltVar.fltVals.Remove(FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1].Remove(0, 1))]);
                        FltVar.fltNames.Remove(parts[1].Remove(0, 1));
                    }
                    break;

                case "MATH": // (num1) (x) (num2), Stores Into Var
                    dynamic mLeft = default(float), mRight = default(float);
                    float store = 0;
                    // Check Left For Vars
                    if (parts[2].StartsWith("$")) mLeft = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0, 1))];
                    else if (parts[2].StartsWith("*")) mLeft = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[2].Remove(0, 1))];
                    // If No Vars, Attempt To Convert
                    else
                    {
                        if (TypeAttempt.Int(parts[2])) mLeft = int.Parse(parts[2]);
                        else if (TypeAttempt.Float(parts[2])) mLeft = float.Parse(parts[2]);
                        else
                        {
                            Console.WriteLine("MATH: Invalid Input on left");
                            Environment.Exit(1);
                        }
                    }

                    // Check Right For Vars
                    if (parts[4].StartsWith("$")) mRight = IntVar.intVals[IntVar.intNames.IndexOf(parts[4].Remove(0, 1))];
                    else if (parts[4].StartsWith("*")) mRight = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[4].Remove(0, 1))];
                    // If No Vars, Attempt To Convert
                    else
                    {
                        if (TypeAttempt.Int(parts[4])) mRight = int.Parse(parts[4]);
                        if (TypeAttempt.Float(parts[4])) mRight = float.Parse(parts[4]);
                        else
                        {
                            Console.WriteLine("MATH: Invalid Input on right");
                            Environment.Exit(1);
                        }
                    }

                    switch (parts[3])
                    {
                        case "+":
                            store = mLeft + mRight;
                            break;

                        case "-":
                            store = mLeft - mRight;
                            break;

                        case "*":
                            store = mLeft * mRight;
                            break;

                        case "/":
                            store = mLeft / mRight;
                            break;
                    }

                    // Store Data
                    if (parts[1].StartsWith("$"))
                    {
                        IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0, 1))] = (int)store;
                    }
                    else if (parts[1].StartsWith("*"))
                    {
                        FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1].Remove(0, 1))] = store;
                    }
                    break;

                case "RAND": // Random Number Between (lower) And (upper)
                    int lower, upper;
                    // Check for variable(s)
                    if (parts[2].StartsWith("$")) lower = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0, 1))];
                    else lower = int.Parse(parts[2]);
                    if (parts[3].StartsWith("$")) upper = IntVar.intVals[IntVar.intNames.IndexOf(parts[3].Remove(0, 1))];
                    else upper = int.Parse(parts[3]);

                    Random random = new();
                    IntVar.intVals[IntVar.intNames.IndexOf(parts[1])] = random.Next(lower, upper);
                    break;
            }
        }
    }
}
