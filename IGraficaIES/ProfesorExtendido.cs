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
        Divorciado = 3,
        Viudo = 4
    }
    public class ProfesorExtendido
    {
        private EstadCivil ecivil;
        private string email;
        private int peso;
        private int estatura;
        private int id;
        public ProfesorExtendido()
        {
        }
        public ProfesorExtendido(int estatura, int peso, string email, EstadCivil eCivil)
        {
            Estatura = estatura;
            Peso = peso;
            ProfesorFuncionarioId = email;
            ECivil = eCivil;
        }
        public ProfesorExtendido(int estatura, int peso, string email,int id, EstadCivil eCivil)
        {
            Estatura = estatura;
            Peso = peso;
            Id = id;
            ProfesorFuncionarioId = email;
            ECivil = eCivil;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public string ProfesorFuncionarioId
        {
            get { return email; }
            set { email = value; }
        }

        public EstadCivil ECivil
        {
            get { return ecivil; }
            set { ecivil = value; }
        }
        public static List<ProfesorExtendido> CrearListaProfesores()
        {

            List<ProfesorExtendido> listaProfesores = new List<ProfesorExtendido>();


            listaProfesores.Add(new ProfesorExtendido(
               150,  75, "gamaa@trass.com", EstadCivil.Casado));

            listaProfesores.Add(new ProfesorExtendido(
            165, 82, "felol@trass.com", EstadCivil.Casado));

            listaProfesores.Add(new ProfesorExtendido(
                160, 68, "sadic@trass.com", EstadCivil.Divorciado));

            listaProfesores.Add(new ProfesorExtendido(
           172, 70, "roruj@trass.com", EstadCivil.Viudo));

            listaProfesores.Add(new ProfesorExtendido(
             185, 91, "pegol@trass.com", EstadCivil.Soltero));

            listaProfesores.Add(new ProfesorExtendido(
            155, 65, "mahem@trass.com", EstadCivil.Casado));

            listaProfesores.Add(new ProfesorExtendido(
             178, 88, "jimos@trass.com", EstadCivil.Soltero));

            listaProfesores.Add(new ProfesorExtendido(
           157, 79, "mogid@trass.com", EstadCivil.Viudo));

            listaProfesores.Add(new ProfesorExtendido(
            185, 85, "varoe@trass.com", EstadCivil.Casado));

            listaProfesores.Add(new ProfesorExtendido(
                153, 73, "natop@trass.com", EstadCivil.Divorciado));

            return listaProfesores;
        }


    }
}
