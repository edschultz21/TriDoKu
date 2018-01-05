using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles
{
    public enum CellSetType
    {
        OUTSIDE_NW,
        OUTSIDE_NE,
        OUTSIDE_BOT,

        INSIDE_SW,
        INSIDE_SE,
        INSIDE_TOP,

        TRIANGLE_1U,
        TRIANGLE_2U,
        TRIANGLE_4U,
        TRIANGLE_5U,
        TRIANGLE_7U,
        TRIANGLE_9U,

        TRIANGLE_3D,
        TRIANGLE_6D,
        TRIANGLE_8D,

        HEXAGON
    }

    public class CellSet
    {
        public CellSetType CellSetType { get; set; }
        public int[,] Data { get; set; }

        public CellSet(CellSetType cellSetType)
        {
            if (cellSetType == CellSetType.HEXAGON)
            {
                Data = new int[12, 2];
            }
            else
            {
                Data = new int[9, 2];
            }

            CellSetType = cellSetType;

            switch (CellSetType)
            {
                case CellSetType.TRIANGLE_1U:
                case CellSetType.TRIANGLE_2U:
                case CellSetType.TRIANGLE_4U:
                case CellSetType.TRIANGLE_5U:
                case CellSetType.TRIANGLE_7U:
                case CellSetType.TRIANGLE_9U:
                    PopulateTriangleUp();
                    break;

                case CellSetType.TRIANGLE_3D:
                case CellSetType.TRIANGLE_6D:
                case CellSetType.TRIANGLE_8D:
                    PopulateTriangleDown();
                    break;

                case CellSetType.OUTSIDE_NW:
                case CellSetType.OUTSIDE_NE:
                case CellSetType.OUTSIDE_BOT:
                    PopulateOutsides();
                    break;

                case CellSetType.INSIDE_SW:
                case CellSetType.INSIDE_SE:
                case CellSetType.INSIDE_TOP:
                    PopulateInsides();
                    break;

                default:
                    break;
            }
        }

        #region Populate

        private void PopulateTriangleUp()
        {
            int x = 0, y = 0, cell = 0;

            switch (CellSetType)
            {
                case CellSetType.TRIANGLE_1U: x = 10; y = 1; break;
                case CellSetType.TRIANGLE_2U: x = 7; y = 4; break;
                case CellSetType.TRIANGLE_4U: x = 13; y = 4; break;
                case CellSetType.TRIANGLE_5U: x = 4; y = 7; break;
                case CellSetType.TRIANGLE_7U: x = 10; y = 7; break;
                case CellSetType.TRIANGLE_9U: x = 16; y = 7; break;
            }

            Data[cell, 0] = x;
            Data[cell, 1] = y;
            cell++;
            for (int i = -1; i <= 1; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y + 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y + 2;
                cell++;
            }
        }

        private void PopulateTriangleDown()
        {
            int x = 0, y = 0, cell = 0;

            switch (CellSetType)
            {
                case CellSetType.TRIANGLE_3D: x = 10; y = 6; break;
                case CellSetType.TRIANGLE_6D: x = 7; y = 9; break;
                case CellSetType.TRIANGLE_8D: x = 13; y = 9; break;
            }

            Data[cell, 0] = x;
            Data[cell, 1] = y;
            cell++;
            for (int i = -1; i <= 1; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y - 2;
                cell++;
            }
        }

        private void PopulateOutsides()
        {
            switch (CellSetType)
            {
                case CellSetType.OUTSIDE_NW:
                    Data[0, 0] = 10;
                    Data[0, 1] = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i, 0] = Data[i - 1, 0] - 1;
                        Data[i, 1] = Data[i - 1, 1] + 1;
                    }
                    break;
                case CellSetType.OUTSIDE_NE:
                    Data[0, 0] = 10;
                    Data[0, 1] = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i, 0] = Data[i - 1, 0] + 1;
                        Data[i, 1] = Data[i - 1, 1] + 1;
                    }
                    break;
                case CellSetType.OUTSIDE_BOT:
                    Data[0, 0] = 2;
                    Data[0, 1] = 9;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i, 0] = Data[i - 1, 0] + 2;
                        Data[i, 1] = Data[i - 1, 1];
                    }
                    break;
            }
        }

        private void PopulateInsides()
        {
            switch (CellSetType)
            {
                case CellSetType.INSIDE_SW:
                    Data[0, 0] = 6;
                    Data[0, 1] = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i, 0] = Data[i - 1, 0];
                        Data[i, 1] = Data[i - 1, 1] + 1;
                        i++;
                        Data[i, 0] = Data[i - 1, 0] + 1;
                        Data[i, 1] = Data[i - 1, 1];
                    }
                    break;
                case CellSetType.INSIDE_SE:
                    Data[0, 0] = 14;
                    Data[0, 1] = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i, 0] = Data[i - 1, 0];
                        Data[i, 1] = Data[i - 1, 1] + 1;
                        i++;
                        Data[i, 0] = Data[i - 1, 0] - 1;
                        Data[i, 1] = Data[i - 1, 1];
                    }
                    break;
                case CellSetType.INSIDE_TOP:
                    Data[0, 0] = 6;
                    Data[0, 1] = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i, 0] = Data[i - 1, 0] + 1;
                        Data[i, 1] = Data[i - 1, 1];
                    }
                    break;
            }
        }

        public void PopulateHexagon(int x, int y)
        {
            var number = x + y;
            if (number % 2 == 0)
            {
                PopulateHexagonDown(x, y);
            }
            else
            {
                PopulateHexagonUp(x, y);
            }
        }

        private void PopulateHexagonUp(int x, int y)
        {
            int cell = 0;

            for (int i = -1; i <= 1; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Data[cell, 0] = x + i;
                    Data[cell, 1] = y;
                    cell++;
                }
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y + 1;
                cell++;
            }
        }

        private void PopulateHexagonDown(int x, int y)
        {
            int cell = 0;

            for (int i = -2; i <= 2; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Data[cell, 0] = x + i;
                    Data[cell, 1] = y;
                    cell++;
                }
            }
            for (int i = -1; i <= 1; i++)
            {
                Data[cell, 0] = x + i;
                Data[cell, 1] = y + 1;
                cell++;
            }
        }

        #endregion
    }

    public class TriDoKu
    {
        private int[,] _cells = new int[21, 11];
        private bool[] _hasNumber = new bool[10];

        #region Initialization

        private Dictionary<CellSetType, CellSet> _cellSets;

        private void InitializeCellSets()
        {
            _cellSets = new Dictionary<CellSetType, CellSet>();

            _cellSets.Add(CellSetType.OUTSIDE_NW, new CellSet(CellSetType.OUTSIDE_NW));
            _cellSets.Add(CellSetType.OUTSIDE_NE, new CellSet(CellSetType.OUTSIDE_NE));
            _cellSets.Add(CellSetType.OUTSIDE_BOT, new CellSet(CellSetType.OUTSIDE_BOT));
            _cellSets.Add(CellSetType.INSIDE_SW, new CellSet(CellSetType.INSIDE_SW));
            _cellSets.Add(CellSetType.INSIDE_SE, new CellSet(CellSetType.INSIDE_SE));
            _cellSets.Add(CellSetType.INSIDE_TOP, new CellSet(CellSetType.INSIDE_TOP));
            _cellSets.Add(CellSetType.TRIANGLE_1U, new CellSet(CellSetType.TRIANGLE_1U));
            _cellSets.Add(CellSetType.TRIANGLE_2U, new CellSet(CellSetType.TRIANGLE_2U));
            _cellSets.Add(CellSetType.TRIANGLE_3D, new CellSet(CellSetType.TRIANGLE_3D));
            _cellSets.Add(CellSetType.TRIANGLE_4U, new CellSet(CellSetType.TRIANGLE_4U));
            _cellSets.Add(CellSetType.TRIANGLE_5U, new CellSet(CellSetType.TRIANGLE_5U));
            _cellSets.Add(CellSetType.TRIANGLE_6D, new CellSet(CellSetType.TRIANGLE_6D));
            _cellSets.Add(CellSetType.TRIANGLE_7U, new CellSet(CellSetType.TRIANGLE_7U));
            _cellSets.Add(CellSetType.TRIANGLE_8D, new CellSet(CellSetType.TRIANGLE_8D));
            _cellSets.Add(CellSetType.TRIANGLE_9U, new CellSet(CellSetType.TRIANGLE_9U));
            _cellSets.Add(CellSetType.HEXAGON, new CellSet(CellSetType.HEXAGON));
        }
        #endregion

        public void Run()
        {
            InitializeCellSets();

            // Triangles1 - Good
            // Triangles2 - OUTSIDE_NW, OUTSIDE_NE, TRIANGLE_1U
            // Triangles3 - OUTSIDE_BOT, INSIDE_SW, INSIDE_SE, TRIANGLE_7U
            // Triangles4 - INSIDE_TOP, TRIANGLE_3D
            // Triangles5 - INSIDE_SW, INSIDE_SE, TRIANGLE_2U, TRIANGLE_4U, TRIANGLE_5U, TRIANGLE_6D, TRIANGLE_8D, TRIANGLE_9U
            Read("Triangles1.txt");

            var ezs = ToString();

            Validate();

            // EZSTODO - Validate hexagons.
            // EZSTODO - Implement logic for solving.

        }

        #region Startup

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int x = 10, y = 1, length = 1;

            for (int i = 0; i < 9; i++)
            {
                sb.Append(NumSpaces(10 - i));

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

        private string NumSpaces(int count)
        {
            var spaces = "";
            for (int i = 0; i < count; i++)
            {
                spaces += " ";
            }
            return spaces;
        }

        private void ResetHasNumber()
        {
            for (int i = 0; i < _hasNumber.Length; i++)
            {
                _hasNumber[i] = false;
            }
        }

        private void ResetCells()
        {
            int x, y;

            for (x = 0; x < 18; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    _cells[x, y] = -1;
                }
            }

            x = 10;
            y = 1;
            var count = 1;
            do
            {
                for (var i = 0; i < count; i++)
                {
                    _cells[x + i, y] = 0;
                }
                x--;
                y++;
                count += 2;
            } while (y != 10);
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
            int x = 10, y = 1;

            var lines = File.ReadAllLines(filename);

            for (int i = 0; i < 9; i++)
            {
                PopulateCells(x--, y++, GetValuesFromInput(lines[i]));
            }
        }

        #endregion

        #region Validation

        private void Validate()
        {
            foreach (CellSetType cellSetType in Enum.GetValues(typeof(CellSetType)))
            {
                if (cellSetType != CellSetType.HEXAGON)
                {
                    Validate(cellSetType);
                }
            }

            ValidateHexagons();
        }

        private void Validate(CellSetType cellSetType)
        {
            Validate(_cellSets[cellSetType].Data);
        }

        private void Validate(int[,] cellSet)
        {
            ResetHasNumber();

            for (int i = 0; i < 9; i++)
            {
                var x = cellSet[i, 0];
                var y = cellSet[i, 1];
                var number = _cells[x, y];

                if (number != -1 && number != 0)
                {
                    if (_hasNumber[number])
                    {

                    }
                    _hasNumber[number] = true;
                }
            }
        }

        private void ValidateHexagon(int[,] cellSet, int centerOfHexagon)
        {
            for (int i = 0; i < 12; i++)
            {
                var x = cellSet[i, 0];
                var y = cellSet[i, 1];
                var number = _cells[x, y];

                if (number == centerOfHexagon)
                {

                }
            }
        }

        private void ValidateHexagons()
        {
            int x = 10, y = 1, count = 1;
            do
            {
                for (var i = 0; i < count; i++)
                {
                    _cellSets[CellSetType.HEXAGON].PopulateHexagon(x + i, y);
                    ValidateHexagon(_cellSets[CellSetType.HEXAGON].Data, _cells[x + i, y]);
                }
                x--;
                y++;
                count += 2;
            } while (y != 10);
        }

        #endregion
    }
}
