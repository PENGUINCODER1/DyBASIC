using System;
using Variables;
using typeAttempt;

namespace Functions
{
    public static class ConsoleFunctions
    {
        public static void Functions(string[] parts)
        {
            switch (parts[0])
            {
                case "PRINT": // Print Input Onto The Terminal
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string part = parts[i];
                        if (part.StartsWith("@"))
                        {
                            Console.Write(StrVar.strVals[StrVar.strNames.IndexOf(part.Remove(0, 1))]);
                        }
                        else if (part.StartsWith("$"))
                        {
                            Console.Write(IntVar.intVals[IntVar.intNames.IndexOf(part.Remove(0, 1))]);
                        }
                        else if (part.StartsWith("*"))
                        {
                            Console.Write(FltVar.fltVals[FltVar.fltNames.IndexOf(part.Remove(0, 1))]);
                        }
                        else Console.Write(part);
                    }
                    Console.Write("\n");
                    break;


                case "PRNTN": // Print without NL
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string part = parts[i];
                        if (part.StartsWith("@"))
                        {
                            Console.Write(StrVar.strVals[StrVar.strNames.IndexOf(part.Remove(0, 1))]);
                        }
                        else if (part.StartsWith("$"))
                        {
                            Console.Write(IntVar.intVals[IntVar.intNames.IndexOf(part.Remove(0, 1))]);
                        }
                        else if (part.StartsWith("*"))
                        {
                            Console.Write(FltVar.fltVals[FltVar.fltNames.IndexOf(part.Remove(0, 1))]);
                        }
                        else Console.Write(part);
                    }
                    break;

                case "CRSR": // Set Cursor POS
                    int cursorX = 0, cursorY = 0;
                    // Cursor X
                    if (parts[1].StartsWith("$")) cursorX = IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0, 1))];
                    else
                    {
                        if (TypeAttempt.Int(parts[1])) cursorX = int.Parse(parts[1]);
                    }

                    // Cursor Y
                    if (parts[2].StartsWith("$")) cursorY = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0, 1))];
                    else
                    {
                        if (TypeAttempt.Int(parts[2])) cursorY = int.Parse(parts[2]);
                    }
                    Console.SetCursorPosition(cursorX, cursorY);
                    break;

                case "SLEEP": // Sleep For (x) Seconds
                    int time = 0;
                    if (parts[1].StartsWith("$")) time = IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0, 1))];
                    else
                    {
                        if (TypeAttempt.Int(parts[1])) time = int.Parse(parts[1]);
                    }
                    System.Threading.Thread.Sleep(time);
                    break;


                case "CLEAR": // Clear Terminal
                    Console.Clear();
                    break;

                case "HOLD": // Sleep Until Keypress
                    Console.ReadKey();
                    break;

                case "QUIT": // End Program
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
