using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGraficaIES
{
    public enum EstadCivil : uint
    {
        Soltero = 1,
        Casado = 2,
        Divorciado = 2,
        Viudo = 4
    }
    class ProfesorExtendido
    {
        private EstadCivil ecivil;
        private string email;
        private int peso;
        private int estatura;

        public ProfesorExtendido()
        {
        }
        public ProfesorExtendido(int estatura, int peso, string email, EstadCivil eCivil)
        {
            Estatura = estatura;
            Peso = peso;
            Email = email;
            ECivil = eCivil;
        }

        public int Estatura
        {
            get { return estatura; }
            set { estatura = value; }
        }


        public int Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public EstadCivil ECivil
        {
            get { return ecivil; }
            set { ecivil = value; }
        }
        
        public List<ProfesorExtendido> GetProfesE()
        {
            //TODO hacer metodo
            List<ProfesorExtendido> profesorExtendidos = new List<ProfesorExtendido>();
            return profesorExtendidos;
        }
        

    }
}
