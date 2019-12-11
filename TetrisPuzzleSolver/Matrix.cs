using System.Collections.Generic;

namespace TetrisPuzzleSolver {
    class Matrix {
        public readonly uint totalCol;
        public readonly List<uint[]> fakeMatrix;

        public Matrix(List<List<uint[]>> allCoords) {
            var offset = G.fieldCol * G.fieldRow;
            totalCol = offset + G.totalTetriminos;
            fakeMatrix = new List<uint[]>();
            var id = 1u;
            for (var i = 0; i < allCoords.Count; i++) {
                foreach (var coord in allCoords[i]) {
                    for (var j = 0u; j < G.tetriminosCount[i]; j++) {
                        var row = new uint[coord.Length + 1];
                        coord.CopyTo(row, 0);
                        row[^1] = offset + id + j;
                        fakeMatrix.Add(row);
                    }
                }
                id += G.tetriminosCount[i];
            }
        }
    }
}
