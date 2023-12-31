using System;
using System.IO;

using typeAttempt;
using Variables;

class Program {
  public static void Main (string[] args) {
    TypeAttempt attempt = new(); // Check typeAttempt.cs
    string[] lines = File.ReadAllLines(args[0]); // Load Program 
    int index = 0; // Current Line (Index)
    string[] parts = { "", "" }; // Parts From Current Line (Index) null

    while (true)
    {
      RSRT:;
      try
      {
        parts = lines[index].Split(";");
      }
      catch
      {
        goto PRGMEND;
      }

      switch (parts[0])
      {
        // Console
      case "PRINT": // Print Input Onto The Terminal
        for (int i = 1; i < parts.Length; i++)
        {
          string part = parts[i];
          if (part.StartsWith("@"))
          {
            Console.Write(StrVar.strVals[StrVar.strNames.IndexOf(part.Remove(0,1))]);
          }
          else if (part.StartsWith("$"))
          {
            Console.Write(IntVar.intVals[IntVar.intNames.IndexOf(part.Remove(0,1))]);
          }
          else if (part.StartsWith("*"))
          {
            Console.Write(FltVar.fltVals[FltVar.fltNames.IndexOf(part.Remove(0,1))]);
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
            Console.Write(StrVar.strVals[StrVar.strNames.IndexOf(part.Remove(0,1))]);
          }
          else if (part.StartsWith("$"))
          {
            Console.Write(IntVar.intVals[IntVar.intNames.IndexOf(part.Remove(0,1))]);
          }
          else if (part.StartsWith("*"))
          {
            Console.Write(FltVar.fltVals[FltVar.fltNames.IndexOf(part.Remove(0,1))]);
          }
          else Console.Write(part);
        }
        break;
        

      case "CRSR": // Set Cursor POS
        int cursorX = 0, cursorY = 0;
        // Cursor X
        if (parts[1].StartsWith("$")) cursorX = IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0,1))];
        else 
        {
          if (attempt.Int(parts[1])) cursorX = int.Parse(parts[1]);
        }

        // Cursor Y
        if (parts[2].StartsWith("$")) cursorY = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0,1))];
        else
        {
          if (attempt.Int(parts[2])) cursorY = int.Parse(parts[2]);
        }
        Console.SetCursorPosition(cursorX, cursorY);
        break;

      case "SLEEP": // Sleep For (x) Seconds
        int time = 0;
        if (parts[1].StartsWith("$")) time = IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0,1))];
        else 
        {
          if (attempt.Int(parts[1])) time = int.Parse(parts[1]);
        }
        System.Threading.Thread.Sleep(time);
        break;

        
      case "CLEAR": // Clear Terminal
        Console.Clear();
        break;

        // Variable
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
        if(parts[1].StartsWith("@"))
        {
          StrVar.strVals.Remove(StrVar.strVals[StrVar.strNames.IndexOf(parts[1].Remove(0,1))]);
          StrVar.strNames.Remove(parts[1].Remove(0,1));
        }
        else if (parts[1].StartsWith("$"))
        {
          IntVar.intVals.Remove(IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0,1))]);
          IntVar.intNames.Remove(parts[1].Remove(0,1));
        }
        else if (parts[1].StartsWith("*"))
        {
          FltVar.fltVals.Remove(FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1].Remove(0,1))]);
          FltVar.fltNames.Remove(parts[1].Remove(0,1));
        }
        break;

        // Math
      case "MATH": // (num1) (x) (num2), Stores Into Var
        dynamic mLeft = default(float), mRight = default(float);
        float store = 0;
        // Check Left For Vars
        if (parts[2].StartsWith("$")) mLeft = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0,1))];
        else if (parts[2].StartsWith("*")) mLeft = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[2].Remove(0,1))];
        // If No Vars, Attempt To Convert
        else
        {
          if (attempt.Int(parts[2])) mLeft = int.Parse(parts[2]);
          else if (attempt.Float(parts[2])) mLeft = float.Parse(parts[2]);
          else
          {
            Console.WriteLine("MATH: Invalid Input on left");
            Environment.Exit(1);
          }
        }

        // Check Right For Vars
        if (parts[4].StartsWith("$")) mRight = IntVar.intVals[IntVar.intNames.IndexOf(parts[4].Remove(0,1))];
        else if (parts[4].StartsWith("*")) mRight = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[4].Remove(0,1))];
        // If No Vars, Attempt To Convert
        else
        {
          if (attempt.Int(parts[4])) mRight = int.Parse(parts[4]);
          if (attempt.Float(parts[4])) mRight = float.Parse(parts[4]);
          else
          {
            Console.WriteLine("MATH: Invalid Input on right");
            Environment.Exit(1);
          }
        }
        
        switch(parts[3])
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
          IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0,1))] = (int)store;
        }
        else if (parts[1].StartsWith("*"))
        {
          FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1].Remove(0,1))] = store;
        }
        break;

      case "RAND": // Random Number Between (lower) And (upper)
        int lower, upper;
        // Check for variable(s)
        if (parts[2].StartsWith("$")) lower = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0,1))];
        else lower = int.Parse(parts[2]);
        if (parts[3].StartsWith("$")) upper = IntVar.intVals[IntVar.intNames.IndexOf(parts[3].Remove(0,1))];
        else upper = int.Parse(parts[3]);
        
        Random random = new();
        IntVar.intVals[IntVar.intNames.IndexOf(parts[1])] =  random.Next(lower, upper);
        break;

        // Input
      case "INTIN": // Take In INT Input, Store Into Variable
        IntVar.intVals[IntVar.intNames.IndexOf(parts[1])] = int.Parse(Console.ReadLine());
        break;
        
      case "STRIN": // Take In STR Input, Store Into Variable
        StrVar.strVals[StrVar.strNames.IndexOf(parts[1])] = Console.ReadLine();
        break;

      case "FLTIN":
        FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1])] = float.Parse(Console.ReadLine());
        break;

      case "HOLD": // Sleep Until Keypress
        Console.ReadKey();
        break;

      case "IF": // Check If Two Values Are (x)
        dynamic left = null, right = null;
        // Check for vars on left input
        if (parts[1].StartsWith("$")) left = IntVar.intVals[IntVar.intNames.IndexOf(parts[1].Remove(0,1))];
        else if (parts[1].StartsWith("@")) left = StrVar.strVals[StrVar.strNames.IndexOf(parts[1].Remove(0,1))];
        else if (parts[1].StartsWith("*")) left = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[1].Remove(0,1))];
        //If no vars, attempt to convert left input into data type
        else
        {
          if (attempt.Int(parts[1])) left = int.Parse(parts[1]);
          else if (attempt.Float(parts[1])) right = float.Parse(parts[1]);
          else left = parts[1];
        }
        // Check for vars on right input
        if (parts[3].StartsWith("$")) right = IntVar.intVals[IntVar.intNames.IndexOf(parts[3].Remove(0,1))];
        else if (parts[3].StartsWith("@")) right = StrVar.strVals[StrVar.strNames.IndexOf(parts[3].Remove(0,1))];
        else if (parts[3].StartsWith("*")) right = FltVar.fltVals[FltVar.fltNames.IndexOf(parts[3].Remove(0,1))];
        //If no vars, attempt to convert right input into data type
        else
        {
          if (attempt.Int(parts[3])) right = int.Parse(parts[3]);
          else if (attempt.Float(parts[3])) right = float.Parse(parts[3]);
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

        // Other Stuff
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
        
      case "QUIT": // End Program
        goto PRGMEND;

      case "CALL": // Start DyBASIC File From Another File
        if (parts[1].StartsWith("@")) lines = File.ReadAllLines(StrVar.strVals[StrVar.strNames.IndexOf(parts[1].Remove(0,1))] + ".dybsc");
        else lines = File.ReadAllLines(parts[1] + ".dybsc");
        if (parts[2].StartsWith("$")) index = IntVar.intVals[IntVar.intNames.IndexOf(parts[2].Remove(0,1))] - 1;
        else index = int.Parse(parts[2]) - 1;
        goto RSRT;
      }
      index++;
    }
    PRGMEND:;
  }
}
