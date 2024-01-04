using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace P05B03.AutoIT
{
    public class CoordenadasImg
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public int status { get; private set; }
        public CoordenadasImg(int x, int y, int status)
        {
            this.x = x;
            this.y = y;
            this.status = status;
        }
    }
}
