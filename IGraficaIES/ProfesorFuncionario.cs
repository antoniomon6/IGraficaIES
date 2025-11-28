using IGraficaIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _2HerenciaSimpleIES
{
    public class ProfesorFuncionario : Profesor, IEmpleadoPublico
    {
        private int anyoIngresoCuerpo;
        private bool definitivo;
        private TipoMed tipoMedico;
        public int AnyoIngresoCuerpo
        {
            get { return anyoIngresoCuerpo; }
            set { anyoIngresoCuerpo = value; }
        }

        public bool DestinoDefinitivo
        {
            get { return definitivo; }
            set { definitivo = value; }
        }
        public TipoMed TipoMedico
        {
            get { return tipoMedico; }
            set { tipoMedico = value; }
        }

        public ProfesorExtendido ProfesorExtendido { get; set; }
        public ProfesorFuncionario()
        {
        }
        public ProfesorFuncionario(int edad, string apellidos, string nombre) : base(edad, apellidos, nombre)
        {
        }

        public ProfesorFuncionario(string nombre,
                                   string apellidos,
                                   int edad,
                                   string materia,
                                   TipoFuncionario tipoProfesor,
                                   int anyoIngreso,
                                   bool definitivo,
                                   TipoMed tipoMedico) : base(edad, apellidos, nombre)
        {
            Materia = materia;
            TipoProfesor = tipoProfesor;
            DestinoDefinitivo = definitivo;
            AnyoIngresoCuerpo = anyoIngreso;
            TipoMedico = tipoMedico;
        }

        public ProfesorFuncionario(string nombre,
                                   string apellidos,
                                   int edad,
                                   string materia,
                                   TipoFuncionario tipoProfesor,
                                   int anyoIngreso,
                                   bool definitivo,
                                   TipoMed tipoMedico,
                                   string rutaFoto) : base(rutaFoto, edad, apellidos, nombre)
        {
            Materia = materia;
            TipoProfesor = tipoProfesor;
            DestinoDefinitivo = definitivo;
            AnyoIngresoCuerpo = anyoIngreso;
            TipoMedico = tipoMedico;

        }

        public ProfesorFuncionario(string rutaFoto, int edad, string apellidos, string nombre) : base(rutaFoto, edad, apellidos, nombre)
        {
        }
        public int GetSexenios()
        {
            return (DateTime.Today.Year - AnyoIngresoCuerpo) / 6;
        }

        public int GetTrienios()
        {
            return (DateTime.Today.Year - AnyoIngresoCuerpo) / 3;
        }

        public (int anyos, int meses, int dias) TiempoServicio()
        {
            DateTime fechaIngreso = new DateTime(AnyoIngresoCuerpo, 9, 1);
            TimeSpan diferencia = DateTime.Now - fechaIngreso;
            int dias = diferencia.Days % 365 % 30;
            int meses = diferencia.Days % 365 / 30;
            int anyos = diferencia.Days / 365;
            return (anyos, meses, dias);
        }

        public override string ToString()
        {
            return base.ToStringProfesor() + string.Format("{0}{1}{2}",
                anyoIngresoCuerpo.ToString().PadRight(10),
                definitivo ? "SI".PadRight(20) : "NO".PadRight(20),
                tipoMedico.ToString().PadRight(20));
        }
    }
}
