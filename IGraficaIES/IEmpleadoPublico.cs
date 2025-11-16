using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2HerenciaSimpleIES
{
    public enum TipoMed : uint
    {
        SeguridadSocial = 1,
        Muface = 2
    }
    internal interface IEmpleadoPublico
    {

        public TipoMed TipoMedico { get; set; }

        (int anyos, int meses, int dias) TiempoServicio();

        int GetSexenios();

        int GetTrienios();
    }
}
