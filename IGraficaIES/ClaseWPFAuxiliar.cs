using _2HerenciaSimpleIES;
using IGraficaIES.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        // Metodo que recibe una lista y habilita todos sus elementos
        public static void Habilitar(IEnumerable<UIElement> lista)
        {
            foreach (UIElement element in lista)
            {
                // En caso de ser un panel habilita todos sus hijos
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
        // Metodo que recibe una lista y deshabilita todos sus elementos
        public static void Deshabilitar(IEnumerable<UIElement> lista)
        {
            foreach (UIElement element in lista)
            {
                // En caso de ser un panel deshabilita todos sus hijos
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

        // Metodo para altenernar entre el campo txtRutaFoto y imgFoto
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
        // Metodo para verificar la validez de los campos
        // Los campos pueden tener las etiquetas, Opcional y/o Numero. 
        public static bool CheckCampos(List<Control> controles, out string controlErroneo)
        {
            bool valido = true;
            controlErroneo = "";
            // Lista para controlar los Grupos de RadioButton
            List<String> listaGruposRadio = new List<String>();
            foreach (Control c in controles)
            {
                string tag = c.Tag == null ? "" : c.Tag.ToString();

                // A los controles desde el XAML se le pueden añadir etiquetas
                // He usado esto para controlar cuando un campo es opcional o un textBox es un numero
                bool esOpcional = tag.Contains("Opcional");
                bool esArchivo = tag.Contains("Archivo");
                bool esNumero = tag.Contains("Numero");

                // Compruebo si la bandera sigue siendo valida
                if (valido)
                {
                    // Compruebo si es un RadioButton
                    if (c is RadioButton rdb)
                    {
                        // La logica de comprobar si un Grupo de RadioButton es valido es mas compleja
                        // Mi solucion es ir guardando los nombres de los grupos en una lista
                        // Cuando encuentro un RadioButton seleccionado pongo el nombre del grupo en mayuscula
                        string grupo = rdb.GroupName.ToLower();
                        if (grupo.StartsWith("opc") || !listaGruposRadio.Contains(grupo.ToUpper()))
                        {
                            if (!listaGruposRadio.Contains(grupo))
                            {
                                listaGruposRadio.Add(grupo);
                            }
                            if ((bool)rdb.IsChecked)
                            {
                                listaGruposRadio[listaGruposRadio.IndexOf(grupo)] = grupo.ToUpper();
                            }
                        }
                    }
                    // Compruebo si es un textBox
                    if (c is TextBox txt)
                    {
                        // Compruebo si el campo es un numero en caso de serlo compruebo su validez haciendo un parseo
                        if (esNumero)
                        {
                            valido = Int32.TryParse(txt.Text, out int resul);
                        }
                        // Con esto compruebo si es un campo de archivo que el archivo exista y no haya puesto una ruta que NO es valida
                        else if (esArchivo)
                        {
                            valido = File.Exists(rutaFija + txt.Text);
                        }
                        else
                        {
                            valido = !string.IsNullOrWhiteSpace(txt.Text);
                        }
                        // En caso de no ser valido compruebo si era opcional y estaba vacio
                        if (!valido)
                        {
                            valido = esOpcional && string.IsNullOrWhiteSpace(txt.Text);
                        }
                    }
                    // Compruebo si es un ComboBox
                    else if (c is ComboBox cmb)
                    {
                        valido = cmb.SelectedValue != null || esOpcional;
                    }
                    // Compruebo si es un ListBox
                    else if (c is ListBox lsb)
                    {
                        valido = lsb.SelectedValue != null || esOpcional;
                    }
                    // Detecto el control que ha fallado, le pongo el foco y guardo el nombre quitandole el prefijo(txt,cmb,lsb)
                    if (!valido)
                    {
                        c.Focus();
                        controlErroneo = c.Name.Substring(3);
                    }
                }

            }
            // Cuando ya he recorrido todos los controles compruebo cuales son los grupos que estan en minuscula
            // Se le puede añadir el Prefijo OPC para que un grupo sea opcional
            for (int i = 0; listaGruposRadio.Count > i && valido; i++)
            {
                string grupo = listaGruposRadio[i];
                valido = grupo == grupo.ToUpper() || grupo.StartsWith("opc");
                if (!valido)
                {
                    controlErroneo = grupo;
                }
            }
            return valido;
        }
        // Metodo que se usa para rellenar la interfaz cada vez que se cambia de persona
        // Uso el parametro de Windows a modo de Contexto para poder acceder a los controles de la interfaz
        public static void RellenarDatos(ProfesorFuncionario p, MainWindow window)
        {
            // Me aseguro que cuando relleno datos vea siempre la foto en lugar de la ruta
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
                    window.rdbEnPracticas.IsChecked = true;
                    break;
                case TipoFuncionario.DeCarrera:
                    window.rdbDeCarrera.IsChecked = true;
                    break;
                default:
                    break;
            }
            // Si no tiene foto le asigno una por defecto
            if (p.RutaFoto == "")
            {
                CargarImagen(ref window.imgFoto, "No_imagen_disponible.gif");
            }
            else
            {
                CargarImagen(ref window.imgFoto, p.RutaFoto);
            }
        }
        // Metodo para vacias los campos
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
            window.rdbDeCarrera.IsChecked = false;
            window.rdbEnPracticas.IsChecked = false;
            window.imgFoto.Source = null;
        }
        //Metodo para simplificar la carga de imagenes
        public static void CargarImagen(ref Image img, string nombre)
        {
            try
            {
                img.Source = new ImageSourceConverter().ConvertFromString(rutaFija + nombre) as ImageSource;
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha encontrado el archivo de imagen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        // Metodo para insertar un objeto en la base de datos
        // Metodo generico
        public static void InsertarDatos<T>(T o)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Add(o);
                context.SaveChanges();
            }
        }
        // Metodo para insertar varios objetos en la base de datos a traves de una lista
        // Metodo generico
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
        // Metodo para modificar un objeto de la base de datos
        // Metodo generico
        public static void ModificarDatos<T>(T o)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Update(o);
                context.SaveChanges();
            }
        }
        // Metodo para eliminar un objeto de la base de datos
        // Metodo generico
        public static void BorrarDatos<T>(T o)
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Remove(o);
                context.SaveChanges();
            }
        }
        // Metodo para obtener una lista de todos los objetos en una tabla de la base de datos
        // Metodo generico, usamos la sentencia where T : class para asegurarnos de que no es un tipo primitivo
        public static List<T> ObternerLista<T>() where T : class
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<T>().ToList();
            }
        }
        // Metodo para el control de los botones de avanzar y retroceder en al lista
        public static void ControlLista(MainWindow window, int profActual, int profCount)
        {
            window.estado = EstadoAPP.Listar;
            window.controlesGridBotones.Habilitar();
            if (profActual == 0)
            {
                Deshabilitar([window.btnPrimero, window.btnAnterior]);
            }
            if (profActual == profCount - 1)
            {
                Deshabilitar([window.btnSiguiente, window.btnUltimo]);
            }
            if (profCount == 1)
            {
                Deshabilitar([window.btnSiguiente, window.btnUltimo, window.btnPrimero, window.btnAnterior]);
            }
            Deshabilitar([window.btnGuardar, window.btnCancelar]);
        }



    }
}
