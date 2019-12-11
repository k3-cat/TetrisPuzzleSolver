using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TetrisPuzzleSolver {
    class Profile {
        const string blank = "  ";
        const string dot = "██";

        public Profile() {
            var jsonStr = File.ReadAllText("./config.json");
            var profile = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonStr);
            var count = profile.Count;
            G.tetriminosCount = new uint[count];
            G.tetriminosName = new string[count];
            G.tetriminosGraph = new string[count];
            G.tetriminos = new List<Tetrimino>[count];
            var id = 0;
            foreach (var item in profile) {
                G.tetriminosName[id] = item.Key;
                G.tetriminos[id] = new List<Tetrimino> { Tetrimino.FromString(item.Value) };
                G.tetriminosGraph[id] = GenerateGraph(G.tetriminos[id][0]);
                id++;
            }
        }

        string GenerateGraph(Tetrimino s) {
            var screen = new bool[s.col, s.row];
            var graph = new StringBuilder("\n");
            for (var i = 0; i < s.x.Length; i++) {
                screen[s.x[i] - 1, s.y[i] - 1] = true;
            }
            var indent = new string(' ', (int)(24 - s.col));
            for (var j = 0; j < s.row; j++) {
                graph.Append(indent);
                for (var i = 0; i < s.col; i++) {
                    graph.Append(screen[i, j] ? dot : blank);
                }
                graph.Append("\n");
            }
            graph.Append("\n");
            return graph.ToString();
        }

        public void AutoRotate() {
            foreach (var tetriminoList in G.tetriminos) {
                var r0 = tetriminoList[0];
                var r1 = r0.Rotate90();
                if (r0.serial.SequenceEqual(r1.serial)) {
                    continue;
                }
                tetriminoList.Add(r1);
                var r2 = r1.Rotate90();
                if (r0.serial.SequenceEqual(r2.serial)) {
                    continue;
                }
                tetriminoList.Add(r2);
                tetriminoList.Add(r2.Rotate90());
            }
        }
    }
}
