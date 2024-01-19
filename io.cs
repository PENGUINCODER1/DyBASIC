using System;
using Variables;

namespace Functions
{
    public static class IOFunctions
    {
        // CALL located in Main
        public static void Functions(string[] parts)
        {
            switch (parts[0])
            {
                case "INTIN": // Take In INT Input, Store Into Variable
                    IntVar.intVals[IntVar.intNames.IndexOf(parts[1])] = int.Parse(Console.ReadLine());
                    break;

                case "STRIN": // Take In STR Input, Store Into Variable
                    StrVar.strVals[StrVar.strNames.IndexOf(parts[1])] = Console.ReadLine();
                    break;

                case "FLTIN":
                    FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1])] = float.Parse(Console.ReadLine());
                    break;
            }
        }
    }
}
