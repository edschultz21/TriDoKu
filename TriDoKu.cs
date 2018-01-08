using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles
{



    public partial class TriDoKu
    {
        private CellData[,] _cells = new CellData[11, 21];
        private bool[] _hasNumber = new bool[10];

        public void Run()
        {
            InitializeCellSets();
            InitializeCells();

            // Triangles1 - Good
            // Triangles2 - OUTSIDE_NW, OUTSIDE_NE, TRIANGLE_1U
            // Triangles3 - OUTSIDE_BOT, INSIDE_SW, INSIDE_SE, TRIANGLE_7U
            // Triangles4 - INSIDE_TOP, TRIANGLE_3D
            // Triangles5 - INSIDE_SW, INSIDE_SE, TRIANGLE_2U, TRIANGLE_4U, TRIANGLE_5U, TRIANGLE_6D, TRIANGLE_8D, TRIANGLE_9U
            Read("Solvable2.txt");

            DetermineAllowableNumbers();

            var ezs1 = ToString();

            Solve();

            var ezs2 = ToString();
        }

        private void Solve()
        {
            var loops = 0;
            var totalCount = 0;

            var count = SolveByAllowableNumbers();
            while (count != 0)
            {
                loops++;
                totalCount += count;
                count = SolveByAllowableNumbers();
            }
        }

        // Easiest approach. Basically see what number(s) are allowed in a given cell.
        // If we get down to one then we know what the number should be.
        private int SolveByAllowableNumbers()
        {
            var count = 0;
            List<Coordinates> newNumbers = new List<Coordinates>();

            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    var cellData = _cells[y, x];
                    if (cellData.Value == 0)
                    {
                        if (cellData.CountOfAllowableNumbers() == 1)
                        {
                            int allowableNumber = cellData.GetListOfAllowableNumbers().First();
                            cellData.Value = allowableNumber;
                            newNumbers.Add(new Coordinates { Y = y, X = x });

                            count++;
                        }
                    }
                }
            }

            foreach (var coordinate in newNumbers)
            {
                UpdateAllowableNumbers(coordinate.Y, coordinate.X);
            }

            return count;
        }

    }
}
