using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles
{

    public class CellSet
    {
        public CellSetType CellSetType { get; set; }
        public Coordinates[] Data { get; set; }

        public bool ContainsCoordinate(int y, int x)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i].Y == y && Data[i].X == x)
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
                    Data[0].Y = 14;
                    Data[0].X = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].Y = Data[i - 1].Y;
                        Data[i].X = Data[i - 1].X + 1;
                        i++;
                        Data[i].Y = Data[i - 1].Y - 1;
                        Data[i].X = Data[i - 1].X;
                    }
                    break;
                case CellSetType.INSIDE_TOP:
                    Data[0].Y = 5;
                    Data[0].X = 6;
                    for (int i = 1; i < 9; i++)
                    {
                        Data[i].Y = Data[i - 1].Y;
                        Data[i].X = Data[i - 1].X + 1;
                    }
                    break;
            }
        }

        public void PopulateHexagon(int y, int x)
        {
            var number = x + y;
            if (number % 2 == 0)
            {
                PopulateHexagonDown(y, x);
            }
            else
            {
                PopulateHexagonUp(y, x);
            }
        }

        private void PopulateHexagonUp(int y, int x)
        {
            int cell = 0;

            for (int i = -1; i <= 1; i++)
            {
                Data[cell].Y = y - 1;
                Data[cell].X = x + i;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Data[cell].Y = y;
                    Data[cell].X = x + i;
                    cell++;
                }
            }
            for (int i = -2; i <= 2; i++)
            {
                Data[cell].Y = y + 1;
                Data[cell].X = x + i;
                cell++;
            }
        }

        private void PopulateHexagonDown(int y, int x)
        {
            int cell = 0;

            for (int i = -2; i <= 2; i++)
            {
                Data[cell].Y = y - 1;
                Data[cell].X = x + i;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Data[cell].Y = y;
                    Data[cell].X = x + i;
                    cell++;
                }
            }
            for (int i = -1; i <= 1; i++)
            {
                Data[cell].Y = y + 1;
                Data[cell].X = x + i;
                cell++;
            }
        }

        #endregion
    }

}
