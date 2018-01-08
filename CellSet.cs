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
        public Coordinates[] Coordinates { get; set; }
        public List<CellSetType> IntersectsWith { get; set; }

        public bool ContainsCoordinate(int y, int x)
        {
            for (int i = 0; i < Coordinates.Length; i++)
            {
                if (Coordinates[i].Y == y && Coordinates[i].X == x)
                {
                    return true;
                }
            }

            return false;
        }

        public CellSet()
        {
            Coordinates = new Coordinates[12];
        }

        public CellSet(CellSetType cellSetType)
        {
            Coordinates = new Coordinates[9];

            CellSetType = cellSetType;
            IntersectsWith = new List<CellSetType>();

            PopulateData();

            PopulateIntersections();
        }

        public static CellSetType GetTriangle(List<CellSetType> cellSet)
        {
            foreach (var cellSetType in cellSet)
            {
                switch (cellSetType)
                {
                    case CellSetType.TRIANGLE_1U:
                    case CellSetType.TRIANGLE_2U:
                    case CellSetType.TRIANGLE_4U:
                    case CellSetType.TRIANGLE_5U:
                    case CellSetType.TRIANGLE_7U:
                    case CellSetType.TRIANGLE_9U:
                    case CellSetType.TRIANGLE_3D:
                    case CellSetType.TRIANGLE_6D:
                    case CellSetType.TRIANGLE_8D:
                        return cellSetType;

                    default:
                        break;
                }
            }

            // Will never get here.
            return CellSetType.NONE;
        }

        public static CellSetType GetInside(List<CellSetType> cellSet)
        {
            foreach (var cellSetType in cellSet)
            {
                switch (cellSetType)
                {
                    case CellSetType.INSIDE_SW:
                    case CellSetType.INSIDE_SE:
                    case CellSetType.INSIDE_TOP:
                        return cellSetType;

                    default:
                        break;
                }
            }

            return CellSetType.NONE;
        }

        public static CellSetType GetOutside(List<CellSetType> cellSet)
        {
            foreach (var cellSetType in cellSet)
            {
                switch (cellSetType)
                {
                    case CellSetType.OUTSIDE_NW:
                    case CellSetType.OUTSIDE_NE:
                    case CellSetType.OUTSIDE_BOT:
                        return cellSetType;

                    default:
                        break;
                }
            }

            return CellSetType.NONE;
        }

        #region Populate

        private void PopulateIntersections()
        {
            switch (CellSetType)
            {
                case CellSetType.TRIANGLE_1U:
                    IntersectsWith.Add(CellSetType.OUTSIDE_NW);
                    IntersectsWith.Add(CellSetType.OUTSIDE_NE);
                    break;
                case CellSetType.TRIANGLE_2U:
                    IntersectsWith.Add(CellSetType.OUTSIDE_NW);
                    IntersectsWith.Add(CellSetType.INSIDE_TOP);
                    break;
                case CellSetType.TRIANGLE_4U:
                    IntersectsWith.Add(CellSetType.OUTSIDE_NE);
                    IntersectsWith.Add(CellSetType.INSIDE_TOP);
                    break;
                case CellSetType.TRIANGLE_5U:
                    IntersectsWith.Add(CellSetType.OUTSIDE_NW);
                    IntersectsWith.Add(CellSetType.OUTSIDE_BOT);
                    break;
                case CellSetType.TRIANGLE_7U:
                    IntersectsWith.Add(CellSetType.INSIDE_SW);
                    IntersectsWith.Add(CellSetType.INSIDE_SE);
                    break;
                case CellSetType.TRIANGLE_9U:
                    IntersectsWith.Add(CellSetType.OUTSIDE_NE);
                    IntersectsWith.Add(CellSetType.OUTSIDE_BOT);
                    break;
                case CellSetType.TRIANGLE_3D:
                    IntersectsWith.Add(CellSetType.INSIDE_TOP);
                    break;
                case CellSetType.TRIANGLE_6D:
                    IntersectsWith.Add(CellSetType.INSIDE_SW);
                    break;
                case CellSetType.TRIANGLE_8D:
                    IntersectsWith.Add(CellSetType.INSIDE_SE);
                    break;
                case CellSetType.OUTSIDE_NW:
                    IntersectsWith.Add(CellSetType.TRIANGLE_1U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_2U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_5U);
                    IntersectsWith.Add(CellSetType.OUTSIDE_NE);
                    IntersectsWith.Add(CellSetType.OUTSIDE_BOT);
                    break;
                case CellSetType.OUTSIDE_NE:
                    IntersectsWith.Add(CellSetType.TRIANGLE_1U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_4U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_9U);
                    IntersectsWith.Add(CellSetType.OUTSIDE_NW);
                    IntersectsWith.Add(CellSetType.OUTSIDE_BOT);
                    break;
                case CellSetType.OUTSIDE_BOT:
                    IntersectsWith.Add(CellSetType.TRIANGLE_5U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_7U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_9U);
                    IntersectsWith.Add(CellSetType.OUTSIDE_NW);
                    IntersectsWith.Add(CellSetType.OUTSIDE_NE);
                    break;
                case CellSetType.INSIDE_SW:
                    IntersectsWith.Add(CellSetType.TRIANGLE_2U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_6D);
                    IntersectsWith.Add(CellSetType.TRIANGLE_7U);
                    IntersectsWith.Add(CellSetType.INSIDE_SE);
                    IntersectsWith.Add(CellSetType.INSIDE_TOP);
                    break;
                case CellSetType.INSIDE_SE:
                    IntersectsWith.Add(CellSetType.TRIANGLE_7U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_8D);
                    IntersectsWith.Add(CellSetType.TRIANGLE_7U);
                    IntersectsWith.Add(CellSetType.INSIDE_SW);
                    IntersectsWith.Add(CellSetType.INSIDE_TOP);
                    break;
                case CellSetType.INSIDE_TOP:
                    IntersectsWith.Add(CellSetType.TRIANGLE_2U);
                    IntersectsWith.Add(CellSetType.TRIANGLE_3D);
                    IntersectsWith.Add(CellSetType.TRIANGLE_4U);
                    IntersectsWith.Add(CellSetType.INSIDE_SW);
                    IntersectsWith.Add(CellSetType.INSIDE_SE);
                    break;

                default:
                    break;
            }
        }

        private void PopulateData()
        {
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

            Coordinates[cell].X = x;
            Coordinates[cell].Y = y;
            cell++;
            for (int i = -1; i <= 1; i++)
            {
                Coordinates[cell].X = x + i;
                Coordinates[cell].Y = y + 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                Coordinates[cell].X = x + i;
                Coordinates[cell].Y = y + 2;
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

            Coordinates[cell].X = x;
            Coordinates[cell].Y = y;
            cell++;
            for (int i = -1; i <= 1; i++)
            {
                Coordinates[cell].X = x + i;
                Coordinates[cell].Y = y - 1;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                Coordinates[cell].X = x + i;
                Coordinates[cell].Y = y - 2;
                cell++;
            }
        }

        private void PopulateOutsides()
        {
            switch (CellSetType)
            {
                case CellSetType.OUTSIDE_NW:
                    Coordinates[0].X = 10;
                    Coordinates[0].Y = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Coordinates[i].X = Coordinates[i - 1].X - 1;
                        Coordinates[i].Y = Coordinates[i - 1].Y + 1;
                    }
                    break;
                case CellSetType.OUTSIDE_NE:
                    Coordinates[0].X = 10;
                    Coordinates[0].Y = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Coordinates[i].X = Coordinates[i - 1].X + 1;
                        Coordinates[i].Y = Coordinates[i - 1].Y + 1;
                    }
                    break;
                case CellSetType.OUTSIDE_BOT:
                    Coordinates[0].X = 2;
                    Coordinates[0].Y = 9;
                    for (int i = 1; i < 9; i++)
                    {
                        Coordinates[i].X = Coordinates[i - 1].X + 2;
                        Coordinates[i].Y = Coordinates[i - 1].Y;
                    }
                    break;
            }
        }

        private void PopulateInsides()
        {
            switch (CellSetType)
            {
                case CellSetType.INSIDE_SW:
                    Coordinates[0].X = 6;
                    Coordinates[0].Y = 5;
                    for (int i = 1; i < 9; i++)
                    {
                        Coordinates[i].X = Coordinates[i - 1].X;
                        Coordinates[i].Y = Coordinates[i - 1].Y + 1;
                        i++;
                        Coordinates[i].X = Coordinates[i - 1].X + 1;
                        Coordinates[i].Y = Coordinates[i - 1].Y;
                    }
                    break;
                case CellSetType.INSIDE_SE:
                    Coordinates[0].Y = 5;
                    Coordinates[0].X = 14;
                    for (int i = 1; i < 9; i++)
                    {
                        Coordinates[i].Y = Coordinates[i - 1].Y + 1;
                        Coordinates[i].X = Coordinates[i - 1].X;
                        i++;
                        Coordinates[i].Y = Coordinates[i - 1].Y;
                        Coordinates[i].X = Coordinates[i - 1].X - 1;
                    }
                    break;
                case CellSetType.INSIDE_TOP:
                    Coordinates[0].Y = 5;
                    Coordinates[0].X = 6;
                    for (int i = 1; i < 9; i++)
                    {
                        Coordinates[i].Y = Coordinates[i - 1].Y;
                        Coordinates[i].X = Coordinates[i - 1].X + 1;
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
                Coordinates[cell].Y = y - 1;
                Coordinates[cell].X = x + i;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Coordinates[cell].Y = y;
                    Coordinates[cell].X = x + i;
                    cell++;
                }
            }
            for (int i = -2; i <= 2; i++)
            {
                Coordinates[cell].Y = y + 1;
                Coordinates[cell].X = x + i;
                cell++;
            }
        }

        private void PopulateHexagonDown(int y, int x)
        {
            int cell = 0;

            for (int i = -2; i <= 2; i++)
            {
                Coordinates[cell].Y = y - 1;
                Coordinates[cell].X = x + i;
                cell++;
            }
            for (int i = -2; i <= 2; i++)
            {
                if (i != 0)
                {
                    Coordinates[cell].Y = y;
                    Coordinates[cell].X = x + i;
                    cell++;
                }
            }
            for (int i = -1; i <= 1; i++)
            {
                Coordinates[cell].Y = y + 1;
                Coordinates[cell].X = x + i;
                cell++;
            }
        }

        #endregion
    }

}
