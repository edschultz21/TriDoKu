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
        private Dictionary<CellSetType, CellSet> _cellSets;

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
            var results = new List<PassResults>();
            var resultsCount = -1;

            while (resultsCount != results.Count)
            {
                resultsCount = results.Count;

                var count = Solve(ByAllowableNumbers);
                while (count != 0)
                {
                    results.Add(new PassResults { SolutionType = SolutionType.ALLOWABLE_NUMBER, AmountSolved = count });
                    count = Solve(ByAllowableNumbers);
                }

                count = Solve(ByTriangleElimination);
                if (count != 0)
                {
                    results.Add(new PassResults { SolutionType = SolutionType.TRIANGLE_ELIMINATION, AmountSolved = count });
                }
            }
        }

        private void Solve1()
        {
            var results = new List<PassResults>();
            var resultsCount = -1;

            while (resultsCount != results.Count)
            {
                resultsCount = results.Count;

                var count = Solve(ByAllowableNumbers);
                while (count != 0)
                {
                    results.Add(new PassResults { SolutionType = SolutionType.ALLOWABLE_NUMBER, AmountSolved = count });
                    count = Solve(ByAllowableNumbers);
                }

                count = Solve(ByTriangleElimination);
                while (count != 0)
                {
                    results.Add(new PassResults { SolutionType = SolutionType.TRIANGLE_ELIMINATION, AmountSolved = count });
                    count = Solve(ByTriangleElimination);
                }
            }
        }

        // Easiest approach. Basically see what number(s) are allowed in a given cell.
        // If we get down to one then we know what the number should be.
        private int Solve(Func<CellData, List<Coordinates>, bool> solve)
        {
            var count = 0;
            List<Coordinates> newNumbers = new List<Coordinates>();

            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 21; x++)
                {
                    var cellData = _cells[y, x];
                    if (cellData.Value == 0)
                    {
                        if (solve(cellData, newNumbers))
                        {
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

        private bool ByAllowableNumbers(CellData cellData, List<Coordinates> newNumbers)
        {
            if (cellData.CountOfAllowableNumbers() == 1)
            {
                int allowableNumber = cellData.GetListOfAllowableNumbers().First();
                cellData.Value = allowableNumber;
                newNumbers.Add(new Coordinates { Y = cellData.Coordinates.Y, X = cellData.Coordinates.X });

                return true;
            }

            return false;
        }

        private bool ByTriangleElimination(CellData cellData, List<Coordinates> newNumbers)
        {
            var triangle = CellSet.GetTriangle(cellData.CellSets);
            var triangleCellSet = _cellSets[triangle];
            var allowableNumbers = cellData.GetListOfAllowableNumbers();

            // Get all free cells in each triangle.
            // For each cell.
            // For each number in that cell.
            // See if any of the other free cells allow it.
            // If none of them do, them we have to use that number.
            foreach (var allowableNumber in allowableNumbers)
            {
                var foundOtherNumber = false;
                foreach (var coordinate in triangleCellSet.Coordinates)
                {
                    if ((cellData.Coordinates.Y != coordinate.Y || cellData.Coordinates.X != coordinate.X) && _cells[coordinate.Y, coordinate.X].Value == 0)
                    {
                        if (_cells[coordinate.Y, coordinate.X].IsNumberAllowed[allowableNumber])
                        {
                            foundOtherNumber = true;
                            break;
                        }
                    }
                }
                if (!foundOtherNumber)
                {
                    cellData.Value = allowableNumber;
                    newNumbers.Add(new Coordinates { Y = cellData.Coordinates.Y, X = cellData.Coordinates.X });

                    return true;
                }
            }

            return false;
        }
    }
}
