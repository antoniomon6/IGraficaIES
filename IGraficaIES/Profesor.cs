
namespace _2HerenciaSimpleIES
{
    public enum TipoFuncionario : uint
    {
        Interino = 1,
        EnPracticas = 2,
        DeCarrera = 3
    }

    public abstract class Profesor : Persona
    {

		private string materia;
        private TipoFuncionario tipoProfesor;

        protected Profesor()
        {
        }

        protected Profesor(int edad, string apellidos, string nombre) : base(edad, apellidos, nombre)
        {
        }

        protected Profesor(string rutaFoto, int edad, string apellidos, string nombre) : base(rutaFoto, edad, apellidos, nombre)
        {
        }

        public TipoFuncionario TipoProfesor
        {
            get { return tipoProfesor; }
            set { tipoProfesor = value; }
        }

        public string Materia
		{
			get { return materia; }
			set { materia = value; }
		}

        public abstract override string ToString();

        public string ToStringProfesor()
        {
            return base.ToString()+string.Format("{0}{1}",
                materia.PadRight(20),
                tipoProfesor.ToString().PadRight(20));
        }
	}
}
