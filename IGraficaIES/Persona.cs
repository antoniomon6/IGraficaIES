using _2HerenciaSimpleIES;
using System.Text;
namespace _2HerenciaSimpleIES
{
    public class Persona
    {
        private int edad;
        private string nombre;
        private string apellidos;
        private string email;
        private string rutaFoto;

        public string RutaFoto
        {
            get { return rutaFoto; }
            set { rutaFoto = value; }
        }

        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = FormatearNombreApellido(value); }
        }

        public string Nombre
        {
            
            get { return nombre; }
            set { nombre = FormatearNombreApellido(value); }
        }
        public string Id
        {
            get { return email; }
            set 
            {
                if (value.EndsWith("trass.com"))
                    email = value;
                else 
                    email = generarEmail(); 
            }
        }
        public Persona() { }
        public Persona(int edad, string apellidos, string nombre)
        {
            Edad = edad;
            Apellidos = apellidos;
            Nombre = nombre;
            Id = "x";
        }

        public Persona(string rutaFoto, int edad, string apellidos, string nombre)
        {
            RutaFoto = rutaFoto;
            Edad = edad;
            Apellidos = apellidos;
            Nombre = nombre;
            Id = "x";
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}",
                nombre.PadRight(25),
                apellidos.PadRight(30),
                email.PadRight(25),
                edad.ToString().PadRight(10));
        }

        public override bool Equals(object? obj)
        {
            return obj is Persona persona &&
                   edad == persona.edad &&
                   nombre == persona.nombre &&
                   apellidos == persona.apellidos &&
                   email == persona.email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(edad, nombre, apellidos, email);
        }

        public static bool operator >(Persona p1, Persona p2) => p1.Edad > p2.Edad;
        public static bool operator <(Persona p1, Persona p2) => p1.Edad < p2.Edad;
        public static bool operator >=(Persona p1, Persona p2) => p1.Edad >= p2.Edad;
        public static bool operator <=(Persona p1, Persona p2) => p1.Edad <= p2.Edad;


        private string FormatearNombreApellido(string value)
        {
            string[] split = value.Split(' ','-');
            string cadena = "";
            foreach (string s in split)
            {
                // Condicion Necesaria en caso de que el usuario añada dos o mas espacios entre las palabras
                if (!string.IsNullOrWhiteSpace(s))
                {
                    // Cojo el primer caracter de la cadena y lo pongo mayuscula y el resto lo pongo minuscula
                    cadena += s.FirstLetterToUpper()+ " ";
                }
            }
            return cadena.Trim();
        }
        protected virtual string generarEmail()
        {
            string cadena = "";
            string[] apellidosSplit = apellidos.Split(' ','-');
            // Añado las dos primeras letras del primer apellido
            // Compruebo si tien uno o dos apellidos y en caso de tener solo 1 lo repito
            // Añado el la primera letra del nombre
            cadena = apellidosSplit[0].Substring(0, 2) +
                apellidosSplit[apellidosSplit.Length < 2 ? 0 : 1].Substring(0, 2) +
                nombre[0] +
                "@trass.com";

            return cadena.ToLower();
        }

        
    }
}
