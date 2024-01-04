using P05B03.AutoIT;
using P05B03.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05_B03.AutoIT
{
    public class BasePageAutoIt
    {
        public AutoItUtilities Au { get; private set; }
        public CoordenadasImg Coordenadas { get; set; }
        public BasePageAutoIt(AutoItUtilities au) 
        { 
            this.Au = au;
        }
    }
}
