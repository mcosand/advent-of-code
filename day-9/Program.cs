using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day_9
{
  class Program
  {
    class Node
    {
      public string Name { get; set; }
      public Dictionary<string, int> Neighbors { get; set; } = new Dictionary<string, int>();
    }

    static List<Node> nodes = new List<Node>();

    static void Main(string[] args)
    {
      foreach (var line in File.ReadLines("input.txt"))
      {
        var match = Regex.Match(line, "(.*) to (.*) = (.*)");
        var left = nodes.FirstOrDefault(f => f.Name == match.Groups[1].Value) ?? new Node { Name = match.Groups[1].Value };
        if (left.Neighbors.Count == 0) nodes.Add(left);

        var right = nodes.FirstOrDefault(f => f.Name == match.Groups[2].Value) ?? new Node { Name = match.Groups[2].Value };
        if (right.Neighbors.Count == 0) nodes.Add(right);

        var dist = int.Parse(match.Groups[3].Value);
        left.Neighbors.Add(right.Name, dist);
        right.Neighbors.Add(left.Name, dist);
      }

      var answer = FindRoute(nodes);
      Console.WriteLine(answer);
    }

    static int FindRoute(List<Node> nodes)
    {
      int best = int.MinValue;
      Queue<string> states = new Queue<string>(
        nodes.Select(f => "0:" + f.Name)
        );

      while (states.Count > 0)
      {
        var state = states.Dequeue();
        Console.WriteLine(state);
        var parts = state.Split(':');
        var ns = parts[1].Split(',');
        var sofar = int.Parse(parts[0]);

        if (ns.Length == nodes.Count)
        {
          if (sofar > best) best = sofar;
          continue;
        }

        var current = nodes.Single(f => f.Name == ns[0]);

        foreach (var neighbor in current.Neighbors)
        {
          if (!ns.Any(f => f == neighbor.Key))
          {
            states.Enqueue((sofar + neighbor.Value) + ":" + neighbor.Key + "," + parts[1]);
          }
        }
      }
      return best;
    }
  }
}
