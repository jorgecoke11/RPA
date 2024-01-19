using P05B03.AutoIT;
using P05B03.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05_B03.Utilidades
{
    public static class MySimUtils
    {
        public static void SacarMenu()
        {
            AutoIt.AutoItX.MouseMove(200, 400);
            AutoIt.AutoItX.Sleep(1000);
            AutoIt.AutoItX.MouseMove(150, 400);
        }
        
    }
}
