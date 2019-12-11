using System.Collections.Generic;

namespace TetrisPuzzleSolver {
    internal class G {
        internal static uint fieldCol;
        internal static uint fieldRow;
        internal static uint[] tetriminosCount;
        internal static uint totalTetriminos;

        internal static List<Tetrimino>[] tetriminos;
        internal static string[] tetriminosName;
        internal static string[] tetriminosGraph;

        internal static double timeTaken;

        internal static readonly Dictionary<string, string> prompts = new Dictionary<string, string> {
            {"SIZE", "Size of the field (as col*row): "},
            {"AMOUNT", "Amount of {0} (as shown above): "},
            {"SPLIT", "\n- - - - - - - - - - - - - - - - - - - - - - - -\n"}
        };

        internal static readonly string[,] skin = new string[,] {
            { "    ", "    " }, // 00: [null]
            { null, null },     // 01: r
            { null, null },     // 02: l
            { "────", "    " }, // 03: l+r
            { null, null },     // 04: d
            { "┌───", "│   " }, // 05: d+r
            { "┐   ", "│   " }, // 06: d+l
            { "┬───", "│   " }, // 07: d+l+r
            { null, null },     // 08: u
            { "└───", "    " }, // 09: u+r
            { "┘   ", "    " }, // 10: u+l
            { "┴───", "    " }, // 11: u+l+r
            { "│   ", "│   " }, // 12: u+d
            { "├───", "│   " }, // 13: u+d+r
            { "┤   ", "│   " }, // 14: u+d+l
            { "┼───", "│   " }, // 15: u+d+l+r
            { null, null },     // 16: * [null]
            { null, null },     // 17: * r
            { null, null },     // 18: * l
            { "════", "    " }, // 19: * l+r
            { null, null },     // 20: * d
            { "╔═══", "║   " }, // 21: * d+r
            { "╗   ", "║   " }, // 22: * d+l
            { "╤═══", "│   " }, // 23: * d+l+r
            { null, null },     // 24: * u
            { "╚═══", "    " }, // 25: * u+r
            { "╝   ", "    " }, // 26: * u+l
            { "╧═══", "    " }, // 27: * u+l+r
            { "║   ", "║   " }, // 28: * u+d
            { "╟───", "║   " }, // 29: * u+d+r
            { "╢   ", "║   " }, // 30: * u+d+l
            { null, null },     // 31: * u+d+l+r
        };
    }
}
