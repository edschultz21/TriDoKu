using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles
{
    public enum SolutionType
    {
        ALLOWABLE_NUMBER,
        TRIANGLE_ELIMINATION,
        INSIDE_ELIMINATION
    }

    public class PassResults
    {
        public SolutionType SolutionType { get; set; }
        public int AmountSolved { get; set; }

        public override string ToString()
        {
            return $"{AmountSolved} {SolutionType}";
        }
    }
}
