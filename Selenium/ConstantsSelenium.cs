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
        public enum TipoClick
        {
            SELENIUM,
            JAVASCRIPT,
            ACTIONS
        }
        public static int TIEMPO_ESPERA_DINAMICA = 180; //SEGUNDOS
        public static int TIEMPO_ESPERA_ESTATICA = 1; //SEGUNDOS
        public static int REINTENTOS = 3;
    }
}
