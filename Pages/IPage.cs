using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotBase.Utilidades;
namespace RobotBase.Pages
{
    public interface IPage
    {
        void Set(SeleniumUtilities su, AloeUtilities aloe);
    }
}
