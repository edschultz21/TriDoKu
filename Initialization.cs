using System.Collections.Generic;

namespace Triangles
{
    public partial class TriDoKu
    {
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
            int y, x;

            for (x = 0; x < 21; x++)
            {
                for (y = 0; y < 11; y++)
                {
                    _cells[y, x] = new CellData();
                }
            }

            y = 1;
            x = 10;
            var count = 1;
            do
            {
                for (var i = 0; i < count; i++)
                {
                    _cells[y, x + i] = new CellData(_cellSets, y, x + i);
                }
                y++;
                x--;
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
                    var cellData = _cells[y, x];
                    if (cellData.Value == 0)
                    {
                        foreach (var cellSetType in cellData.CellSets)
                        {
                            var cellSet = _cellSets[cellSetType];
                            foreach (var coordinate in cellSet.Data)
                            {
                                cellData.DisallowNumber(_cells[coordinate.Y, coordinate.X].Value);
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
                cellData.DisallowNumber(_cells[coordinate.Y, coordinate.X].Value);
            }
        }

        private void UpdateAllowableNumbers(int y, int x)
        {
            var cellData = _cells[y, x];
            var number = cellData.Value;

            foreach (var cellSetType in cellData.CellSets)
            {
                var cellSet = _cellSets[cellSetType];
                foreach (var coordinate in cellSet.Data)
                {
                    _cells[coordinate.Y, coordinate.X].DisallowNumber(number);
                }
            }

            foreach (var coordinate in cellData.HexagonCellSet.Data)
            {
                if (_cells[coordinate.Y, coordinate.X].Value != -1)
                {
                    _cells[coordinate.Y, coordinate.X].DisallowNumber(number);
                }
            }
        }

        private void ResetHasNumber()
        {
            for (int i = 0; i < _hasNumber.Length; i++)
            {
                _hasNumber[i] = false;
            }
        }

        #endregion
    }
}

