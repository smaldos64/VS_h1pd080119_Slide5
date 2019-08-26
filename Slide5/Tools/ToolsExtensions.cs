using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slide5.Tools
{
    public static class CharExtensions
    {
        public static int ParseInt32(this char value)
        {
            int i = (int)(value - '0');
            if (i < 0 || i > 9)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            return i;
        }
    }
}
