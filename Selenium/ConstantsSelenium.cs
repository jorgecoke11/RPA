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
            EDGE,
            EXPLORER
        }
        public enum TipoBusqueda
        {
            ID,
            XPATH,
            CSS
        }
        public enum KeysSelenium
        {
            ESCAPE
        }
        public enum TipoWait
        {
            DISPLAYED,
            ENABLED
        }
    }
}
