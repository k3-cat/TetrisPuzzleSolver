using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisPuzzleSolver {
    class Program {
        static void Main() {
            var profile = new Profile();
            profile.AutoRotate();
            while (true) {
                Console.Title = "Tetris Puzzle Solver [by Pix-00]";
                while (true) {
                    Console.Clear();
                    Console.Write(G.prompts["SIZE"]);
                    try {
                        var fieldSize = Array.ConvertAll(Console.ReadLine().Split('*'), uint.Parse);
                        G.fieldCol = fieldSize[0];
                        G.fieldRow = fieldSize[1];
                        Console.WriteLine(G.prompts["SPLIT"]);
                        for (var i = 0; i < G.tetriminosCount.Length; i++) {
                            Console.WriteLine(G.tetriminosGraph[i]);
                            Console.Write(G.prompts["AMOUNT"], G.tetriminosName[i]);
                            G.tetriminosCount[i] = uint.Parse(Console.ReadLine());
                            Console.WriteLine(G.prompts["SPLIT"]);
                        }
                        break;
                    }
                    catch (Exception ex) {
                        if (ex is FormatException || ex is IndexOutOfRangeException || ex is OverflowException) {
                            Console.WriteLine("\nInvalid Input!\nPress any key to retry ...");
                            Console.ReadKey();
                            continue;
                        }
                        throw;
                    }
                }
                G.totalTetriminos = G.tetriminosCount.Aggregate((sum, next) => sum + next);
                Console.Title += string.Format("  /  Stats: {0}*{1}, {2} pieces,", G.fieldCol, G.fieldRow, G.totalTetriminos);
                var matrix = new Matrix(Coordinate.TraversalAllCoords(shuffle: true));
                var dlx = new DancingLinks(matrix);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                if (!dlx.Solve()) {
                    Console.WriteLine("Fail!\nPress any key to continue ...");
                    Console.ReadKey();
                    continue;
                }
                watch.Stop();
                G.timeTaken = watch.ElapsedMilliseconds / 1000.0;
                Console.Title += string.Format(" {0} s", G.timeTaken);
                var answer = new List<uint[]>();
                foreach (var line_no in dlx.GetAns()) {
                    answer.Add(matrix.fakeMatrix[line_no - 1][0..^1]);
                }
                var output = new Output(G.fieldCol, G.fieldRow);
                output.Render(answer);
                output.Print();
                output.Save();
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
            }
        }
    }
}
