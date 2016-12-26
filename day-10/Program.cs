using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_10
{
  class Program
  {
    static void Main(string[] args)
    {
      int count = 50;
      //count = 5;
      string input = "1113222113";
      //input = "1";

      for (int i=0; i<count;i++)
      {
        StringBuilder sb = new StringBuilder();
        int j = 0;
        char c;
        while (j < input.Length)
        {
          int k = j;
          c = input[j++];
          while (j < input.Length && input[j] == c) j++;
          sb.AppendFormat("{0}{1}", j - k, c);
        }
        input = sb.ToString();
        // Console.WriteLine(input);
        Console.WriteLine(sb.Length);
      }
    }
  }
}
