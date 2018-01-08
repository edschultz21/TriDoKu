using System.Linq;
using System.Text;
using System.IO;

namespace Triangles
{
    public partial class TriDoKu
    {
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
                    sb.Append(_cells[y, x + j].Value);
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

        private int[] GetValuesFromInput(string line)
        {
            return line.ToCharArray().Select(x => x - '0').ToArray();
        }

        private void PopulateCells(int y, int x, int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                _cells[y, x + i].Value = data[i];
            }
        }

        private void Read(string filename)
        {
            int x = 10, y = 1;

            var lines = File.ReadAllLines(filename);

            for (int i = 0; i < 9; i++)
            {
                PopulateCells(y++, x--, GetValuesFromInput(lines[i]));
            }
        }

        #endregion
    }
}
