using _2HerenciaSimpleIES;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static IGraficaIES.ClaseWPFAuxiliar;

namespace IGraficaIES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public FontFamily fuente = new FontFamily("Arial");
        public double tamaño = 12;
        public List<Control> controlesGridCentral;
        private List<Profesor> profesores= new List<Profesor>();


        public MainWindow()
        {
            InitializeComponent();
            gridCent.IsEnabled= false;
            gridBtn.IsEnabled= false;
            menuFiltros.IsEnabled= false;
            menuAgrupacion.IsEnabled= false;

            controlesGridCentral = ObtenerControles(gridCent);
            
            controlesGridCentral.ForEach(x => { x.FontFamily = fuente;x.FontSize = tamaño; });


            CargarImagen(ref imgPrimero, "first_page.png");
            CargarImagen(ref imgSiguiente, "arrow_forward.png");
            CargarImagen(ref imgAnterior, "arrow_back.png");
            CargarImagen(ref imgUltimo, "last_page.png");
            CargarImagen(ref menuAbrir, "file_open.png");
            CargarImagen(ref menuSalir, "exit.png");
            CargarImagen(ref menuNegrita, "format_bold.png");
            CargarImagen(ref menuCursiva, "format_italic.png");
        }
        public void Abrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            //Nos situamos en el directorio desde el que se ejecuta la aplicación
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            //En el cuadro de diálogo se van a mostrar todos los archivos que sean de texto o //si no, todos
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //Por defecto, cuando se abra, nos va a mostrar los que sean de texto
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true) //Cuando el usuario le dé a Aceptar
            {
                try
                {   //Obtengo una colección de líneas (en cada una están los datos
                    //de un profesor), luego las iré recorriendo con un foreach
                    var lineas = File.ReadLines(openFileDialog.FileName);
                    foreach (var line in lineas)
                    {
                        string[] split = line.Split(";");
                        if (split.Length == 9)
                        {
                            profesores.Add(new ProfesorFuncionario(split[0],
                                split[1],
                                Int32.Parse(split[2]),
                                split[4],
                                (TipoFuncionario)Int32.Parse(split[5]),
                                Int32.Parse(split[6]),
                                split[7] == "true",
                                (TipoMed)Int32.Parse(split[8]))
                            );
                        }
                        else
                        {

                            profesores.Add(new ProfesorFuncionario(split[0],
                                split[1],
                                Int32.Parse(split[2]),
                                split[4],
                                split[5] == "De Carrera" ? TipoFuncionario.DeCarrera : split[5] == "En Practicas" ? TipoFuncionario.EnPracticas : TipoFuncionario.Interino,
                                Int32.Parse(split[6]),
                                split[7] == "true",
                                split[8] == "SS" ? TipoMed.SeguridadSocial : TipoMed.Muface,
                                split[9])
                            );
                        }
                    }
                }catch (Exception ex)
                {

                }
            }
        }
        public void Salir_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Negrita_Checked(object sender, RoutedEventArgs e)
        {
            controlesGridCentral.ForEach(x=>x.FontWeight = FontWeights.Bold);
        }
        public void Negrita_Unchecked(object sender, RoutedEventArgs e)
        {
            controlesGridCentral.ForEach(x => x.FontWeight = FontWeights.Normal);
        }
        public void Cursiva_Checked(object sender, RoutedEventArgs e)
        {
            controlesGridCentral.ForEach(x => x.FontStyle = FontStyles.Italic);
        }
        public void Cursiva_Unchecked(object sender, RoutedEventArgs e)
        {
            controlesGridCentral.ForEach(x => x.FontStyle = FontStyles.Normal);
        }
        public void btnPrimero_Click(object sender, RoutedEventArgs e)
        {

        }
        public void btnAnterior_Click(object sender, RoutedEventArgs e)
        {

        }
        public void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {

        }
        public void btnUltimo_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}