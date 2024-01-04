using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotBase.Utilidades
{
    public static class ConstantsSelenium
    {
        public enum NAVEGADOR
        {
            FIREFOX,
            CHROME,
            EDGE
        }
        public enum TipoBusqueda
        {
            ID,
            XPATH,
            CSS
        }
    }
}
