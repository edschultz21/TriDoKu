using System;

namespace Triangles
{
    public partial class TriDoKu
    {

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
                var y = cellSet[i].Y;
                var x = cellSet[i].X;
                var number = _cells[y, x].Value;

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
                var y = cellSet[i].Y;
                var x = cellSet[i].X;
                var number = _cells[y, x].Value;

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
                    ValidateHexagon(_cells[y, x + i].HexagonCellSet.Data, _cells[y, x + i].Value);
                }
                x--;
                y++;
                count += 2;
            } while (y != 10);
        }

        #endregion
    }
}
