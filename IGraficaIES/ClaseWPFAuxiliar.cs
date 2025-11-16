using _2HerenciaSimpleIES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace IGraficaIES
{
    public class ClaseWPFAuxiliar
    {
        public const string rutaFija = "..\\..\\..\\recursos\\";

        //Metodo para cargar todos los controles de un Panel (grid, StackPanel, etc)
        public static List<Control> ObtenerControles(Panel contenedor)
        {
            List<Control> controles = new List<Control>();
            foreach (var item in contenedor.Children)
            {
                // Diferencio entre Control y Panel
                if (item is Control)
                {
                    controles.Add((Control)item);
                }
                else if (item is Panel)
                {
                    // Uso recursividad para sacar los metodos que haya en paneles Hijos del principal
                    controles.AddRange(ObtenerControles((Panel)item));
                }
            }
            return controles;

        }
        // Metodo Reflection para Listas genericas
        public static string CrearStringMensaje<T>(IEnumerable<T> profesores)
        {
            string cadena = "";

            System.Reflection.PropertyInfo[] listaPropiedades = typeof(T).GetProperties();

            foreach (var item in profesores)
            {
                //para cada propiedad
                foreach (System.Reflection.PropertyInfo propiedad in listaPropiedades)
                {
                    cadena += string.Format("{0}: {1} \n", propiedad.Name, propiedad.GetValue(item));
                }
                cadena += "\n";

            }

            return cadena;
        }
        // Metodo Reflection para Grupos genericos
        public static string CrearStringMensaje<TClave, TValor>(IEnumerable<IGrouping<TClave, TValor>> grupoProfesores, bool mostrarPropiedadAgrupada)
        {
            StringBuilder sb = new StringBuilder();
            System.Reflection.PropertyInfo[] listaPropiedades = typeof(TValor).GetProperties();
            foreach (var grupo in grupoProfesores)
            {
                sb.AppendFormat("\n----------- Grupo {0} -----------\n", grupo.Key);
                foreach (var item in grupo)
                {
                    //Contador para poner 2 propiedades por linea
                    int cont = 0;
                    
                    foreach (System.Reflection.PropertyInfo propiedad in listaPropiedades)
                    {
                        //Condicion para que no se muestre la propiedad por la que se agrupa y evita redundancia
                        //Añado un booleano para poder indicar si quiero que se muestre o no
                        if (!(grupo.Key.ToString()==propiedad.GetValue(item).ToString())||mostrarPropiedadAgrupada)
                        {
                            sb.AppendFormat("{0}: {1}{2}", propiedad.Name, propiedad.GetValue(item), cont == 0 ? "         " : "\n");
                            cont = (cont + 1) % 2;
                        }
                    }
                    sb.AppendLine("\n");
                }
            }

            return sb.ToString();
        }

        // Metodo que se usa para rellenar la interfaz cada vez que se cambia de persona
        // Uso el parametro de Windows a modo de Contexto para poder acceder a los controles de la interfaz
        public static void RellenarDatos(ProfesorFuncionario p, MainWindow window)
        {
            window.txtNombre.Text = p.Nombre;
            window.txtApellidos.Text = p.Apellidos;
            window.txtEmail.Text = p.Email;
            window.cmbEdad.SelectedValue = p.Edad;
            window.txtAnioIngreso.Text = p.AnyoIngresoCuerpo.ToString();
            window.chkDestino.IsChecked = p.DestinoDefinitivo;
            window.lsbSegMedico.SelectedValue = p.TipoMedico == TipoMed.SeguridadSocial ? "S.Social" : "Muface";
            switch (p.TipoProfesor)
            {
                case TipoFuncionario.Interino:
                    break;
                case TipoFuncionario.EnPracticas:
                    window.rbEnPracticas.IsChecked = true;
                    break;
                case TipoFuncionario.DeCarrera:
                    window.rbDeCarrera.IsChecked = true;
                    break;
                default:
                    break;
            }
            if (p.RutaFoto == "")
            {
                CargarImagen(ref window.imgFoto, "No_imagen_disponible.gif");
            }
            else
            {
                CargarImagen(ref window.imgFoto, p.RutaFoto);
            }
        }
        //Metodo para simplificar la carga de imagenes
        public static void CargarImagen(ref Image img, string nombre)
        {
            img.Source = new ImageSourceConverter().ConvertFromString(rutaFija + nombre) as ImageSource;

        }
    }
}
