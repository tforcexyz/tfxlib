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

using System;
using System.Collections.Generic;
using System.Linq;
using Xyz.TForce.Collections;

namespace Xyz.TForce.Graphs
{

  public class MatrixGraph : IGraph
  {

    private readonly DoubledDictionary<string, int> _vertexDictionary;
    private readonly List<List<double?>> _edgeMatrix;

    public MatrixGraph()
    {
      _vertexDictionary = new DoubledDictionary<string, int>();
      _edgeMatrix = new List<List<double?>>();
    }

    public void AddVertex(string point)
    {
      if (_vertexDictionary.ContainsKey(point))
      {
        throw new InvalidOperationException("Point is already existed.");
      }
      _vertexDictionary[point] = _edgeMatrix.Count;
      foreach (List<double?> row in _edgeMatrix)
      {
        row.Add(null);
      }
      List<double?> newRow = new List<double?>();
      for (int i = 0; i <= _edgeMatrix.Count; i++)
      {
        newRow.Add(null);
      }
      _edgeMatrix.Add(newRow);
    }

    public void AddEdge(string startPoint, string endPoint, double weight = 0)
    {
      if (!_vertexDictionary.ContainsKey(startPoint))
      {
        AddVertex(startPoint);
      }
      if (!_vertexDictionary.ContainsKey(endPoint))
      {
        AddVertex(endPoint);
      }
      int startVertex = _vertexDictionary[startPoint];
      int endVertex = _vertexDictionary[endPoint];
      if (_edgeMatrix[startVertex][endVertex].HasValue)
      {
        throw new InvalidOperationException("Edge is already existed.");
      }
      _edgeMatrix[startVertex][endVertex] = weight;
    }

    public bool HasVertex(string point)
    {
      return _vertexDictionary.ContainsKey(point);
    }

    public bool HasEdge(string startPoint, string endPoint)
    {
      if (!_vertexDictionary.ContainsKey(startPoint))
      {
        return false;
      }
      if (!_vertexDictionary.ContainsKey(endPoint))
      {
        return false;
      }
      int startVertex = _vertexDictionary[startPoint];
      int endVertex = _vertexDictionary[endPoint];
      return _edgeMatrix[startVertex][endVertex].HasValue;
    }

    public ICollection<Edge> GetAdjacentEdges(string point)
    {
      if (!_vertexDictionary.ContainsKey(point))
      {
        throw new InvalidOperationException("Point doesn't exist.");
      }
      int vertex = _vertexDictionary[point];
      ICollection<Edge> edges = new List<Edge>();
      for (int j = 0; j < _edgeMatrix[vertex].Count; j++)
      {
        double? weight = _edgeMatrix[vertex][j];
        if (weight.HasValue)
        {
          string endPoint = _vertexDictionary.GetKeys(j).FirstOrDefault();
          Edge edge = new Edge(point, endPoint, weight.Value);
          edges.Add(edge);
        }
      }
      return edges;
    }
  }
}
