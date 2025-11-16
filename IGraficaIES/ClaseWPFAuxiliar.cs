using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGraficaIES
{
    public  class ClaseWPFAuxiliar
    {
        public const string rutaFija = "..\\..\\..\\recursos\\";
        public static List<Control> ObtenerControles(Panel contenedor)
        {
           List<Control> controles = new List<Control>();
            foreach (var item in contenedor.Children)
            {
                if (item is Control)
                {
                    controles.Add((Control)item);
                }
                else if (item is Panel)
                {
                    controles.AddRange(ObtenerControles((Panel)item));
                }
            }
            return controles;
            
        }
        public static void CargarImagen(ref Image img, string nombre)
        {
            img.Source = new ImageSourceConverter().ConvertFromString(rutaFija + nombre) as ImageSource;
 
        }
    }
}
