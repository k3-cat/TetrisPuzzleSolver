using System;
using System.Collections.Generic;


namespace TetrisPuzzleSolver {
    class Coordinate {
        readonly uint dxMax;
        readonly uint dyMax;
        readonly uint[] basePoints;

        readonly static Random rng = new Random();

        public Coordinate(Tetrimino t) {
            dxMax = G.fieldCol - t.col;
            dyMax = G.fieldRow - t.row;
            basePoints = new uint[t.x.Length];
            for (var i = 0; i < t.x.Length; i++) {
                basePoints[i] = t.x[i] + G.fieldCol * (t.y[i] - 1);
            }
            Array.Sort(basePoints);
        }

        List<uint[]> GetTraversal() {
            var coords = new List<uint[]>();
            for (var i = 0u; i <= dyMax; i++) {
                for (var j = 0u; j <= dxMax; j++) {
                    var coord = new uint[basePoints.Length];
                    for (var k = 0; k < basePoints.Length; k++) {
                        coord[k] = basePoints[k] + j + G.fieldCol * i;
                    }
                    coords.Add(coord);
                }
            }
            return coords;
        }

        public static List<List<uint[]>> TraversalAllCoords(bool shuffle) {
            var allCoordinates = new List<List<uint[]>>();
            foreach (var tetriminoList in G.tetriminos) {
                var partialCoords = new List<uint[]>();
                foreach (var tetrimino in tetriminoList) {
                    var coord = new Coordinate(tetrimino);
                    partialCoords.AddRange(coord.GetTraversal());
                }
                if (shuffle) {
                    Shuffle(partialCoords);
                }
                allCoordinates.Add(partialCoords);
            }
            return allCoordinates;
        }

        public static void Shuffle(List<uint[]> list) {
            var n = list.Count;
            while (n > 1) {
                n--;
                var k = rng.Next(n + 1);
                var val = list[k];
                list[k] = list[n];
                list[n] = val;
            }
        }
    }
}
