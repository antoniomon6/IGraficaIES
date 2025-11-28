using _2HerenciaSimpleIES;
using IGraficaIES.Context;
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
        public enum EstadoAPP : uint
        {
            SinCargar = 0,
            Listar = 1,
            Inserccion = 2,
            Modificacion = 3,
            Eliminacion = 4
        }

        public static void Habilitar(IEnumerable<UIElement> lista)
        {
            foreach (UIElement element in lista)
            {
                if (element is Panel)
                {
                    Habilitar(ObtenerControles((Panel)element));
                }
                else
                {
                    element.IsEnabled = true;
                }
            }
        }
        public static void Deshabilitar(IEnumerable<UIElement> lista)
        {
            foreach (UIElement element in lista)
            {
                if (element is Panel)
                {
                    Deshabilitar(ObtenerControles((Panel)element));
                }
                else
                {
                    element.IsEnabled = false;
                }
            }
        }
        public static void AlternarFoto(MainWindow window)
        {
            switch (window.txtRutaFoto.Visibility)
            {
                case Visibility.Visible:
                    window.txtRutaFoto.Visibility = Visibility.Hidden;
                    window.lblRutaFoto.Visibility = Visibility.Hidden;
                    window.imgFoto.Visibility = Visibility.Visible;
                    break;
                case Visibility.Hidden:
                    window.txtRutaFoto.Visibility = Visibility.Visible;
                    window.lblRutaFoto.Visibility = Visibility.Visible;
                    window.imgFoto.Visibility = Visibility.Hidden;
                    break;
            }
        }
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
                        if (!(grupo.Key.ToString() == propiedad.GetValue(item).ToString()) || mostrarPropiedadAgrupada)
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
        public static bool CheckCampos(MainWindow window, out string controlErroneo)
        {
            bool valido = true;
            controlErroneo = "";
            // TODO Evitar Hardcodeo
            if (window.rbDeCarrera.IsChecked == false && window.rbEnPracticas.IsChecked == false)
            {
                valido = false;
                controlErroneo = "Tipo Profesor";
            }
            foreach (Control c in ObtenerControles(window.gridCent))
            {
                if (valido && (c.Tag == null ? true : c.Tag.ToString() != "Opcional"))
                {

                    if (c is TextBox)
                    {
                        if (c.Tag == null ? false : c.Tag.ToString() == "Numero")
                        {
                            valido = Int32.TryParse(((TextBox)c).Text, out int result);
                        }
                        else
                        {
                            valido = !string.IsNullOrWhiteSpace(((TextBox)c).Text);
                        }
                    }
                    if (c is ComboBox)
                    {
                        valido = ((ComboBox)c).SelectedValue != null;
                    }
                    if (c is ListBox)
                    {
                        valido = ((ListBox)c).SelectedValue != null; ;
                    }
                    if (!valido)
                    {
                        c.Focus();
                        controlErroneo = c.Name.Substring(3);
                    }
                }
            }
            return valido;
        }
        // Metodo que se usa para rellenar la interfaz cada vez que se cambia de persona
        // Uso el parametro de Windows a modo de Contexto para poder acceder a los controles de la interfaz
        public static void RellenarDatos(ProfesorFuncionario p, MainWindow window)
        {
            if (window.imgFoto.Visibility == Visibility.Hidden)
            {
                AlternarFoto(window);
            }
            window.txtNombre.Text = p.Nombre;
            window.txtApellidos.Text = p.Apellidos;
            window.txtEmail.Text = p.Id;
            window.cmbEdad.SelectedValue = p.Edad;
            window.txtAnioIngreso.Text = p.AnyoIngresoCuerpo.ToString();
            window.chkDestino.IsChecked = p.DestinoDefinitivo;
            window.lsbSegMedico.SelectedValue = p.TipoMedico == TipoMed.SeguridadSocial ? "S.Social" : "Muface";
            window.txtRutaFoto.Text = p.RutaFoto;
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
        public static void BorrarCampos(MainWindow window)
        {
            window.txtNombre.Text = "";
            window.txtApellidos.Text = "";
            window.txtEmail.Text = "";
            window.cmbEdad.SelectedValue = null;
            window.txtAnioIngreso.Text = "";
            window.chkDestino.IsChecked = false;
            window.lsbSegMedico.SelectedValue = null;
            window.txtRutaFoto.Text = "";
            window.rbDeCarrera.IsChecked = false;
            window.rbEnPracticas.IsChecked = false;
        }
        //Metodo para simplificar la carga de imagenes
        public static void CargarImagen(ref Image img, string nombre)
        {
            img.Source = new ImageSourceConverter().ConvertFromString(rutaFija + nombre) as ImageSource;

        }

        public static void InsertarDatos<T>(T o)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Add(o);
                context.SaveChanges();
            }
        }
        public static void InsertarDatos<T>(ref List<T> l)
        {
            using (MyDbContext context = new MyDbContext())
            {
                foreach (T t in l)
                {
                    context.Add(t);
                }
                context.SaveChanges();
            }
        }
        public static void ModificarDatos<T>(T o)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Update(o);
                context.SaveChanges();
            }
        }
        public static void BorrarDatos<T>(T o)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Remove(o);
                context.SaveChanges();
            }
        }

        public static void ControlLista(MainWindow window, int profActual, int profCount)
        {
            
            window.controlesGridBotones.Habilitar();
            if (profActual == 0)
            {
                Deshabilitar([window.btnPrimero, window.btnAnterior]);
            }
            if (profActual == profCount - 1)
            {
                Deshabilitar([window.btnSiguiente, window.btnUltimo]);
            }
            if (profCount==1)
            {
                Deshabilitar([window.btnSiguiente, window.btnUltimo, window.btnPrimero, window.btnAnterior]);
            }
            Deshabilitar([window.btnGuardar, window.btnCancelar]);
        }
        public static List<T> ObternerLista<T>() where T : class
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<T>().ToList();
            }
        }


    }
}
