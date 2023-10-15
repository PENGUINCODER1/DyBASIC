using System;
using System.IO;
using System.Collections.Generic;

class Program {
  public static void Main (string[] args) {
    string[] lines = File.ReadAllLines(args[0]);
    int index = 0;
    string[] parts = { "", "" };

    // Strings
    List<string> strNames = new();
    List<string> strVals = new();

    // Ints
    List<string> intNames = new();
    List<int> intVals = new();

    Console.WriteLine("DyBASIC\n");
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
      case "PRINT":
        for (int i = 1; i < parts.Length; i++)
        {
          string part = parts[i];
          if (part.StartsWith("@"))
          {
            Console.Write(strVals[strNames.IndexOf(part.Remove(0,1))]);
          }
          else if (part.StartsWith("$"))
          {
            Console.Write(intVals[intNames.IndexOf(part.Remove(0,1))]);
          }
          else Console.Write(part);
        }
        Console.Write("\n");
        break;
        
      case "CLEAR":
        Console.Clear();
        break;

      // Variable
      case "STR":
        if (strNames.Contains(parts[1])) // Check if the variable already exists
        {
          strVals[strNames.IndexOf(parts[1])] = parts[2];
        }
        else
        {
          strNames.Add(parts[1]);
          strVals.Add(parts[2]);
        }
        break;
        
      case "INT":
        if (intNames.Contains(parts[1])) // Check if the variable already exists
        {
          intVals[intNames.IndexOf(parts[1])] = int.Parse(parts[2]);
        }
        else
        {
          intNames.Add(parts[1]);
          intVals.Add(int.Parse(parts[2]));
        }
        break;
        
      case "DROP":
        if(parts[1].StartsWith("@"))
        {
          strVals.Remove(strVals[strNames.IndexOf(parts[1].Remove(0,1))]);
          strNames.Remove(parts[1].Remove(0,1));
        }
        else if (parts[1].StartsWith("$"))
        {
          intVals.Remove(intVals[intNames.IndexOf(parts[1].Remove(0,1))]);
          intNames.Remove(parts[1].Remove(0,1));
        }
        break;

        // Math
      case "MATH":
        switch(parts[3])
        {
        case "+":
          intVals[intNames.IndexOf(parts[1])] = intVals[intNames.IndexOf(parts[2])] + intVals[intNames.IndexOf(parts[4])];
          break;
          
        case "-":
          intVals[intNames.IndexOf(parts[1])] = intVals[intNames.IndexOf(parts[2])] - intVals[intNames.IndexOf(parts[4])];
          break;
          
        case "*":
          intVals[intNames.IndexOf(parts[1])] = intVals[intNames.IndexOf(parts[2])] * intVals[intNames.IndexOf(parts[4])];
          break;
          
        case "/":
          intVals[intNames.IndexOf(parts[1])] = intVals[intNames.IndexOf(parts[2])] / intVals[intNames.IndexOf(parts[4])];
          break;
        }
        break;

      case "RAND":
        int lower, upper;
        // Check for variable(s)
        if (parts[2].StartsWith("$")) lower = intVals[intNames.IndexOf(parts[2].Remove(0,1))];
        else lower = int.Parse(parts[2]);
        if (parts[3].StartsWith("$")) upper = intVals[intNames.IndexOf(parts[3].Remove(0,1))];
        else upper = int.Parse(parts[3]);
        
        Random random = new();
        intVals[intNames.IndexOf(parts[1])] =  random.Next(lower, upper);
        break;

      // Input
      case "INTIN":
        intVals[intNames.IndexOf(parts[1])] = int.Parse(Console.ReadLine());
        break;
        
      case "STRIN":
        strVals[strNames.IndexOf(parts[1])] = Console.ReadLine();
        break;

      case "IF":
        switch (parts[2])
        {
        case "=": 
          if (intVals[intNames.IndexOf(parts[1])] == intVals[intNames.IndexOf(parts[3])]) 
          {
            index = int.Parse(parts[4]) - 1; 
            goto RSRT;
          }
          break;
        case ">": 
          if (intVals[intNames.IndexOf(parts[1])] > intVals[intNames.IndexOf(parts[3])]) 
          {
            index = int.Parse(parts[4]) - 1; 
            goto RSRT;
          }
          break;
        case "<": 
          if (intVals[intNames.IndexOf(parts[1])] < intVals[intNames.IndexOf(parts[3])]) 
          {
            index = int.Parse(parts[4]) - 1; 
            goto RSRT;
          }
          break;
        }
        break;

      //Other Stuff
      case "GOTO":
        index = int.Parse(parts[1]) - 1;
        goto RSRT;

      case "QUIT":
        goto PRGMEND;

      case "CALL":
        if (parts[1].StartsWith("@")) lines = File.ReadAllLines(strVals[strNames.IndexOf(parts[1].Remove(0,1))] + ".dybsc");
        else lines = File.ReadAllLines(parts[1] + ".dybsc");
        if (parts[2].StartsWith("$")) index = intVals[intNames.IndexOf(parts[2].Remove(0,1))] - 1;
        else index = int.Parse(parts[2]) - 1;
        goto RSRT;
      }
      index++;
    }
    PRGMEND:;
  }
}
