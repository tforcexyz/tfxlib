// Copyright 2017 T-Force Xyz
// Please refer to LICENSE & CONTRIB files in the project root for license information.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;

namespace Xyz.TForce.Graphs.Paths
{

  public class BreadthFirstSearch
  {

    public ICollection<Edge> FindPath(IGraph graph, string startPoint, string endPoint)
    {

      Queue<string> newNodes = new Queue<string>();
      HashSet<string> visitedNodes = new HashSet<string>();
      newNodes.Enqueue(startPoint);
      Dictionary<string, string> previousNodes = new Dictionary<string, string>();

      while (newNodes.Count != 0)
      {
        string root = newNodes.Dequeue();
        ICollection<Edge> edges = graph.GetAdjacentEdges(root);

        if (root == endPoint)
        {
          string previous = root;
          List<string> backtrack = new List<string>();
          while (previousNodes.ContainsKey(previous))
          {
            backtrack.Add(previous);
            previous = previousNodes[previous];
          }
          backtrack.Add(startPoint);
          backtrack.Reverse();
          ICollection<Edge> path = new List<Edge>();
          for (int i = 0; i < backtrack.Count - 1; i++)
          {
            Edge edge = new Edge(backtrack[i], backtrack[i + 1], 0);
            path.Add(edge);
          }
          return path;
        }

        foreach (Edge edge in edges)
        {
          string child = edge.EndPoint;
          if (visitedNodes.Contains(child))
          {
            continue;
          }
          if (!newNodes.Contains(child))
          {
            previousNodes.Add(child, root);
            newNodes.Enqueue(child);
          }
        }

        _ = visitedNodes.Add(root);
      }

      // Path not found
      return new List<Edge>();
    }
  }
}
