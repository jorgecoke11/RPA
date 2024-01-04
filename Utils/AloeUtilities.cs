using RobotBase.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotBase.Utilidades
{
    public class AloeUtilities : Robot_base
    {
        public AloeUtilities(string NombreTipo, int idRobot, int IdCasoAsignado, int IdOperacionAsignado, int IdProceso) : base(NombreTipo, idRobot)
        {
            this.IdCasoAsignado = IdCasoAsignado;
            this.IdOperacionAsignado = IdOperacionAsignado;
            this.IdProceso = IdProceso;
        }
    }
}
