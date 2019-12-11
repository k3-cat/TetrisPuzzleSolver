using System;
using System.Linq;

namespace TetrisPuzzleSolver {
    class Tetrimino {
        public readonly uint[] x;
        public readonly uint[] y;
        public readonly uint col;
        public readonly uint row;
        public readonly uint[] serial;

        public static Tetrimino FromString(string pSeq) {
            var points = pSeq.Split('|');
            var xSeq = new uint[points.Length];
            var ySeq = new uint[points.Length];
            for (var i = 0; i < points.Length; i++) {
                var coord = points[i].Split(',');
                xSeq[i] = uint.Parse(coord[0]);
                ySeq[i] = uint.Parse(coord[1]);
            }
            return new Tetrimino(xSeq, ySeq);
        }

        public Tetrimino(uint[] xSeq, uint[] ySeq) {
            x = xSeq;
            y = ySeq;
            col = x.Max();
            row = y.Max();
            serial = new uint[x.Length];
            for (var i = 0; i < x.Length; i++) {
                serial[i] = x[i] + col * (y[i] - 1);
            }
            Array.Sort(serial);
        }

        public Tetrimino Rotate90() {
            var newX = new uint[x.Length];
            var newY = new uint[x.Length];
            for (var i = 0; i < x.Length; i++) {
                newX[i] = y[i];
                newY[i] = col - x[i] + 1;
            }
            return new Tetrimino(newX, newY);
        }
    }
}
