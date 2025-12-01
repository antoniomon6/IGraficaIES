

using System.Windows;
using System.Windows.Controls;

namespace _2HerenciaSimpleIES
{
    public static class ExtensionClass
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
            StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public static string FirstLetterToUpper(this String str)
        {
            if (str != null)
            {
                str = str.Trim();
                if (str.Length == 1)
                {
                    str = str.ToUpper();
                }
                else
                {
                    str = str[0].ToString().ToUpper() + str.Substring(1).ToLower();
                }
            }
            return str;
        }
        public static bool SeekRemove(this List<Persona> list, Persona personaBuscada)
        {
            return list.Remove(personaBuscada);
        }
        public static void Habilitar(this IEnumerable<UIElement> lista)
        {
            foreach (UIElement element in lista)
            {
                element.IsEnabled = true;
            }
        }
        public static void Deshabilitar(this IEnumerable<UIElement> lista)
        {
            foreach (UIElement element in lista)
            {
                element.IsEnabled = false;
            }
        }

        public static void On(this UIElement b)
        {
            b.IsEnabled = true;
        }
        public static void Off(this UIElement b)
        {
            b.IsEnabled = false;
        }

    }
}
