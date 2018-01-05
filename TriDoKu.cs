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
        TRIANGLE_8D
    }

    public struct Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    public class CellSet
    {
        public CellSetType CellSetType { get; set; }
        public Coordinates[] Data { get; set; }

        public bool ContainsCoordinate(int x, int y)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i].X == x && Data[i].Y == y)
                {
                    return true;
                }
            }

            return false;
        }

        public CellSet()
        {
            Data = new Coordinates[12];
        }

        public CellSet(CellSetType cellSetType)
        {
            Data = new Coordinates[9];

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

            Data[cell].X = x;
            Data[cell].Y = y;
            cell++;
            for (int i = -1; i <= 1; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y + 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y + 2;
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

            Data[cell].X = x;
            Data[cell].Y = y;
            cell++;
            for (int i = -1; i <= 1; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y - 2;
                cell++;
            }
        }

        private void PopulateOutsides()
        {
            switch (CellSetType)
            {
                case CellSetType.OUTSIDE_NW:
                    Data[0].X = 10;
                    Data[0].Y = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].X = Data[i - 1].X - 1;
                        Data[i].Y = Data[i - 1].Y + 1;
                    }
                    break;
                case CellSetType.OUTSIDE_NE:
                    Data[0].X = 10;
                    Data[0].Y = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].X = Data[i - 1].X + 1;
                        Data[i].Y = Data[i - 1].Y + 1;
                    }
                    break;
                case CellSetType.OUTSIDE_BOT:
                    Data[0].X = 2;
                    Data[0].Y = 9;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].X = Data[i - 1].X + 2;
                        Data[i].Y = Data[i - 1].Y;
                    }
                    break;
            }
        }

        private void PopulateInsides()
        {
            switch (CellSetType)
            {
                case CellSetType.INSIDE_SW:
                    Data[0].X = 6;
                    Data[0].Y = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].X = Data[i - 1].X;
                        Data[i].Y = Data[i - 1].Y + 1;
                        i++;
                        Data[i].X = Data[i - 1].X + 1;
                        Data[i].Y = Data[i - 1].Y;
                    }
                    break;
                case CellSetType.INSIDE_SE:
                    Data[0].X = 14;
                    Data[0].Y = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].X = Data[i - 1].X;
                        Data[i].Y = Data[i - 1].Y + 1;
                        i++;
                        Data[i].X = Data[i - 1].X - 1;
                        Data[i].Y = Data[i - 1].Y;
                    }
                    break;
                case CellSetType.INSIDE_TOP:
                    Data[0].X = 6;
                    Data[0].Y = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].X = Data[i - 1].X + 1;
                        Data[i].Y = Data[i - 1].Y;
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
                Data[cell].X = x + i;
                Data[cell].Y = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Data[cell].X = x + i;
                    Data[cell].Y = y;
                    cell++;
                }
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y + 1;
                cell++;
            }
        }

        private void PopulateHexagonDown(int x, int y)
        {
            int cell = 0;

            for (int i = -2; i <= 2; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Data[cell].X = x + i;
                    Data[cell].Y = y;
                    cell++;
                }
            }
            for (int i = -1; i <= 1; i++)
            {
                Data[cell].X = x + i;
                Data[cell].Y = y + 1;
                cell++;
            }
        }

        #endregion
    }

    public class CellData
    {
        public Coordinates Coordinates { get; set; }
        public List<CellSetType> CellSets { get; set; }
        public CellSet HexagonCellSet { get; set; }
        public int Value { get; set; }
        public bool[] IsNumberAllowed { get; set; }

        public CellData()
        {
            Value = -1;
        }

        public CellData(Dictionary<CellSetType, CellSet> cellSets, int x, int y)
        {
            Coordinates = new Coordinates { X = x, Y = y };
            CellSets = new List<CellSetType>();

            Value = 0;

            IsNumberAllowed = new bool[10];
            for (int i = 1; i < 10; i++)
            {
                IsNumberAllowed[i] = true;
            }

            foreach (var cellSetType in cellSets.Keys)
            {
                var cellSet = cellSets[cellSetType];
                if (cellSet.ContainsCoordinate(x, y))
                {
                    CellSets.Add(cellSetType);
                }
            }

            HexagonCellSet = new CellSet();
            HexagonCellSet.PopulateHexagon(x, y);
        }

        public void DisallowNumber(int number)
        {
            if (number != -1)
            {
                IsNumberAllowed[number] = false;
            }
        }

        public void DisallowAllNumbers()
        {
            for (int i = 1; i < 10; i++)
            {
                IsNumberAllowed[i] = false;
            }
        }

        public int CountOfAllowableNumbers()
        {
            var count = 0;
            for (int i = 0; i < 10; i++)
            {
                count += (IsNumberAllowed[i] ? 1 : 0);
            }

            return count;
        }

        public List<int> GetListOfAllowableNumbers()
        {
            var allowableNumbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                if (IsNumberAllowed[i])
                {
                    allowableNumbers.Add(i);
                }
            }

            return allowableNumbers;
        }

        public override string ToString()
        {
            if (Value == -1)
            {
                return "";
            }

            var cellSetTypes = CellSets.Select(x => x.ToString()).ToList();

            return $"{Value}: ({string.Join(",", cellSetTypes)}";
        }
    }

    public class TriDoKu
    {
        private CellData[,] _cells = new CellData[21, 11];
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
        }

        private void InitializeCells()
        {
            int x, y;

            for (x = 0; x < 21; x++)
            {
                for (y = 0; y < 11; y++)
                {
                    _cells[x, y] = new CellData();
                }
            }

            x = 10;
            y = 1;
            var count = 1;
            do
            {
                for (var i = 0; i < count; i++)
                {
                    _cells[x + i, y] = new CellData(_cellSets, x + i, y);
                }
                x--;
                y++;
                count += 2;
            } while (y != 10);
        }

        private void DetermineAllowableNumbers()
        {
            // Cycle through all the cells.
            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    var cellData = _cells[x, y];
                    if (cellData.Value == 0)
                    {
                        foreach (var cellSetType in cellData.CellSets)
                        {
                            var cellSet = _cellSets[cellSetType];
                            foreach (var coordinate in cellSet.Data)
                            {
                                cellData.DisallowNumber(_cells[coordinate.X, coordinate.Y].Value);
                            }
                        }

                        DetermineAllowableNumbersForHexagon(cellData);
                    }
                    else if (cellData.Value != -1)
                    {
                        cellData.DisallowAllNumbers();
                    }
                }
            }
        }

        private void DetermineAllowableNumbersForHexagon(CellData cellData)
        {
            var cellSet = cellData.HexagonCellSet;
            foreach (var coordinate in cellSet.Data)
            {
                cellData.DisallowNumber(_cells[coordinate.X, coordinate.Y].Value);
            }
        }

        private void UpdateAllowableNumbers(int x, int y)
        {
            var cellData = _cells[x, y];
            var number = cellData.Value;

            foreach (var cellSetType in cellData.CellSets)
            {
                var cellSet = _cellSets[cellSetType];
                foreach (var coordinate in cellSet.Data)
                {
                    _cells[coordinate.X, coordinate.Y].DisallowNumber(number);
                }
            }

            foreach (var coordinate in cellData.HexagonCellSet.Data)
            {
                if (_cells[coordinate.X, coordinate.Y].Value != -1)
                {
                    _cells[coordinate.X, coordinate.Y].DisallowNumber(number);
                }
            }
        }

        #endregion

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
                    var cellData = _cells[x, y];
                    if (cellData.Value == 0)
                    {
                        if (cellData.CountOfAllowableNumbers() == 1)
                        {
                            int allowableNumber = cellData.GetListOfAllowableNumbers().First();
                            cellData.Value = allowableNumber;
                            newNumbers.Add(new Coordinates { X = x, Y = y });

                            count++;
                        }
                    }
                }
            }

            foreach (var coordinate in newNumbers)
            {
                UpdateAllowableNumbers(coordinate.X, coordinate.Y);
            }

            return count;
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
                    sb.Append(_cells[x + j, y].Value);
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

        private int[] GetValuesFromInput(string line)
        {
            return line.ToCharArray().Select(x => x - '0').ToArray();
        }

        private void PopulateCells(int x, int y, int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                _cells[x + i, y].Value = data[i];
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
                Validate(cellSetType);
            }

            ValidateHexagons();
        }

        private void Validate(CellSetType cellSetType)
        {
            Validate(_cellSets[cellSetType].Data);
        }

        private void Validate(Coordinates[] cellSet)
        {
            ResetHasNumber();

            for (int i = 0; i < 9; i++)
            {
                var x = cellSet[i].X;
                var y = cellSet[i].Y;
                var number = _cells[x, y].Value;

                if (number != -1 && number != 0)
                {
                    if (_hasNumber[number])
                    {

                    }
                    _hasNumber[number] = true;
                }
            }
        }

        private void ValidateHexagon(Coordinates[] cellSet, int centerOfHexagon)
        {
            for (int i = 0; i < 12; i++)
            {
                var x = cellSet[i].X;
                var y = cellSet[i].Y;
                var number = _cells[x, y].Value;

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
                    ValidateHexagon(_cells[x + i, y].HexagonCellSet.Data, _cells[x + i, y].Value);
                }
                x--;
                y++;
                count += 2;
            } while (y != 10);
        }

        #endregion
    }
}
