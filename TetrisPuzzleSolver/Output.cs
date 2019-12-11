using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TetrisPuzzleSolver {
    class Output {
        const uint edge = 1u << 4;
        const uint u = ~(1u << 3);
        const uint d = ~(1u << 2);
        const uint l = ~(1u << 1);
        const uint r = ~(1u << 0);

        readonly uint col;
        readonly uint row;
        string result;
        const string indent = "\n      ";

        public Output(uint col, uint row) {
            this.col = col + 1;
            this.row = row + 1;
        }

        uint[] Reverse(uint order) {
            // calculate in original size (screen is larger in both axis)
            var x = (order - 1) % (col - 1) + 1;
            var y = (order - x) / (col - 1) + 1;
            return new uint[] { x, y };
        }

        public void Render(List<uint[]> shapes) {
            var screen = new uint[col + 1, row + 1];
            for (var i = 1; i <= col; i++) {
                for (var j = 1; j <= row; j++) {
                    screen[i, j] = 15;
                }
            }
            for (var i = 1; i <= col; i++) {
                screen[i, 1] |= edge;
                screen[i, 1] &= u;
                screen[i, row] |= edge;
                screen[i, row] &= d;
            }
            for (var j = 1; j <= row; j++) {
                screen[1, j] |= edge;
                screen[1, j] &= l;
                screen[col, j] |= edge;
                screen[col, j] &= r;
            }
            foreach (var shape in shapes) {
                for (var i = 0; i < shape.Length; i++) {
                    var c = Reverse(shape[i]);
                    if (shape.Contains(shape[i] + 1)) {
                        screen[c[0] + 1, c[1]] &= d;
                        screen[c[0] + 1, c[1] + 1] &= u;
                    }
                    if (shape.Contains(shape[i] + col - 1)) {
                        screen[c[0], c[1] + 1] &= r;
                        screen[c[0] + 1, c[1] + 1] &= l;
                    }
                }
            }
            var graph = new StringBuilder();
            for (var j = 1; j <= row; j++) {
                var buffer0 = new StringBuilder(indent);
                var buffer1 = new StringBuilder(indent);
                for (var i = 1; i <= col; i++) {
                    buffer0.Append(G.skin[screen[i, j], 0]);
                    buffer1.Append(G.skin[screen[i, j], 1]);
                }
                graph.Append(buffer0.ToString());
                graph.Append(buffer1.ToString());
            }
            result = graph.ToString();
        }

        public void Print() {
            Console.WriteLine(result);
            Console.WriteLine("");
        }

        public void Save() {
            var content = new StringBuilder(String.Format(
                "Tetris Puzzle Solver [by Pix-00]\n\n// {0}*{1} field, {2} pieces, taken {3} s //\n[ ",
                G.fieldCol, G.fieldRow, G.totalTetriminos, G.timeTaken));
            var temp = new string[G.tetriminosName.Length];
            for (var i = 0; i < G.tetriminosName.Length; i++) {
                temp[i] = (String.Format("{0}: {1}", G.tetriminosName[i], G.tetriminosCount[i]));
            }
            content.Append(String.Join(" | ", temp));
            content.Append(" ] \n");
            content.Append(G.prompts["SPLIT"]);
            content.Append(result);
            File.WriteAllText(DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + ".txt", content.ToString());
        }
    }
}
