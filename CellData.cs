using System.Collections.Generic;
using System.Linq;

namespace Triangles
{


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

        public CellData(Dictionary<CellSetType, CellSet> cellSets, int y, int x)
        {
            Coordinates = new Coordinates { Y = y, X = x };
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
                if (cellSet.ContainsCoordinate(y, x))
                {
                    CellSets.Add(cellSetType);
                }
            }

            HexagonCellSet = new CellSet();
            HexagonCellSet.PopulateHexagon(y, x);
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

            return $"{Value}: {string.Join(",", cellSetTypes)}";
        }
    }
}
