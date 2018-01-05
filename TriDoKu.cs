using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles
{
    public class TriDoKu
    {
        private int[,] _cells = new int[19, 11];
        private bool[] _hasNumber = new bool[10];

        #region Initialization

        private static int[,] OUTSIDE_NW = new int[,] { { 9, 1 }, { 8, 2 }, { 7, 3 }, { 6, 4 }, { 5, 5 }, { 4, 6 }, { 3, 7 }, { 2, 8 }, { 1, 9 } };
        private static int[,] OUTSIDE_NE = new int[,] { { 9, 1 }, { 10, 2 }, { 11, 3 }, { 12, 4 }, { 13, 5 }, { 14, 6 }, { 15, 7 }, { 16, 8 }, { 17, 9 } };
        private static int[,] OUTSIDE_BOT = new int[,] { { 1, 9 }, { 3, 9 }, { 5, 9 }, { 7, 9 }, { 9, 9 }, { 11, 9 }, { 13, 9 }, { 15, 9 }, { 17, 9 } };
        private static int[,] INSIDE_SW = new int[,] { { 5, 5 }, { 5, 6 }, { 6, 6 }, { 6, 7 }, { 7, 7 }, { 7, 8 }, { 8, 8 }, { 8, 9 }, { 9, 9 } };
        private static int[,] INSIDE_SE = new int[,] { { 13, 5 }, { 13, 6 }, { 12, 6 }, { 12, 7 }, { 11, 7 }, { 11, 8 }, { 10, 8 }, { 10, 9 }, { 9, 9 } };
        private static int[,] INSIDE_TOP = new int[,] { { 5, 5 }, { 6, 5 }, { 7, 5 }, { 8, 5 }, { 9, 5 }, { 10, 5 }, { 11, 5 }, { 12, 5 }, { 13, 5 } };
        private static int[,] TRIANGLE_1U = new int[,] { { 9, 1 }, { 8, 2 }, { 9, 2 }, { 10, 2 }, { 7, 3 }, { 8, 3 }, { 9, 3 }, { 10, 3 }, { 11, 3 } };
        private static int[,] TRIANGLE_2U = new int[,] { { 6, 4 }, { 5, 5 }, { 6, 5 }, { 7, 5 }, { 4, 6 }, { 5, 6 }, { 6, 6 }, { 7, 6 }, { 8, 6 } };
        private static int[,] TRIANGLE_3D = new int[,] { { 9, 6 }, { 8, 5 }, { 9, 5 }, { 10, 5 }, { 7, 4 }, { 8, 4 }, { 9, 4 }, { 10, 4 }, { 11, 4 } };
        private static int[,] TRIANGLE_4U = new int[,] { { 12, 4 }, { 11, 5 }, { 12, 5 }, { 13, 5 }, { 10, 6 }, { 11, 6 }, { 12, 6 }, { 13, 6 }, { 14, 6 } };
        private static int[,] TRIANGLE_5U = new int[,] { { 3, 7 }, { 2, 8 }, { 3, 8 }, { 4, 8 }, { 1, 9 }, { 2, 9 }, { 3, 9 }, { 4, 9 }, { 5, 9 } };
        private static int[,] TRIANGLE_6D = new int[,] { { 6, 9 }, { 5, 8 }, { 6, 8 }, { 7, 8 }, { 4, 7 }, { 5, 7 }, { 6, 7 }, { 7, 7 }, { 8, 7 } };
        private static int[,] TRIANGLE_7U = new int[,] { { 9, 7 }, { 8, 8 }, { 9, 8 }, { 10, 8 }, { 7, 9 }, { 8, 9 }, { 9, 9 }, { 10, 9 }, { 11, 9 } };
        private static int[,] TRIANGLE_8D = new int[,] { { 12, 9 }, { 11, 8 }, { 12, 8 }, { 13, 8 }, { 10, 7 }, { 11, 7 }, { 12, 7 }, { 13, 7 }, { 14, 7 } };
        private static int[,] TRIANGLE_9U = new int[,] { { 15, 7 }, { 14, 8 }, { 15, 8 }, { 16, 8 }, { 13, 9 }, { 14, 9 }, { 15, 9 }, { 16, 9 }, { 17, 9 } };

        private readonly int[][,] _triangles = new int[][,] 
        {
            OUTSIDE_NW, OUTSIDE_NE, OUTSIDE_BOT, INSIDE_SW, INSIDE_SE, INSIDE_TOP,
            TRIANGLE_1U, TRIANGLE_2U, TRIANGLE_3D, TRIANGLE_4U, TRIANGLE_5U, TRIANGLE_6D, TRIANGLE_7U, TRIANGLE_8D, TRIANGLE_9U
        };

        #endregion

        public void Run()
        {
            // Triangles1 - Good
            // Triangles2 - OUTSIDE_NW, OUTSIDE_NE, TRIANGLE_1U
            // Triangles3 - OUTSIDE_BOT, INSIDE_SW, INSIDE_SE, TRIANGLE_7U
            // Triangles4 - INSIDE_TOP, TRIANGLE_3D
            // Triangles5 - INSIDE_SW, INSIDE_SE, TRIANGLE_2U, TRIANGLE_4U, TRIANGLE_5U, TRIANGLE_6D, TRIANGLE_8D, TRIANGLE_9U
            Read("Triangles1.txt");

            Validate();

            // EZSTODO - Validate hexagons.
            // EZSTODO - Implement logic for solving.
            // EZSTODO - Add to git.
            var ezs = ToString();
        }

        #region Startup

        private void ResetHasNumber()
        {
            for (int i = 0; i < _hasNumber.Length; i++)
            {
                _hasNumber[i] = false;
            }
        }

        private string NumSpaces(int count)
        {
            var spaces = "";
            for (int i = 0; i < count; i++)
            {
                spaces += " ";
            }
            return spaces;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int x = 9, y = 1, length = 1;

            for (int i = 0; i < 9; i++)
            {
                sb.Append(NumSpaces(9 - i));

                for (int j = 0; j < length; j++)
                {
                    sb.Append(_cells[x + j, y]);
                }

                x--;
                y++;
                length += 2;

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void ResetCells()
        {
            for (int x = 0; x < 18; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    _cells[x, y] = 0;
                }
            }
        }

        private int[] GetValuesFromInput(string line)
        {
            return line.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        }

        private void PopulateCells(int x, int y, int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                _cells[x + i, y] = data[i];
            }
        }

        private void Read(string filename)
        {
            int x = 9, y = 1;

            var lines = File.ReadAllLines(filename);

            for (int i = 0; i < 9; i++)
            {
                PopulateCells(x--, y++, GetValuesFromInput(lines[i]));
            }
        }

        #endregion

        private void Validate()
        {
            foreach (var triangle in _triangles)
            {
                Validate(triangle);
            }
        }

        private void Validate(int[,] triangle)
        {
            ResetHasNumber();

            for (int i = 0; i < 9; i++)
            {
                var x = triangle[i, 0];
                var y = triangle[i, 1];
                var number = _cells[x, y];

                if (_hasNumber[number])
                {

                }
                _hasNumber[number] = true;
            }
        }
    }
}
