

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
            str = str.Trim();   
            return str[0].ToString().ToUpper()+str.Substring(1).ToLower();
        }
        public static bool SeekRemove(this List<Persona> list, Persona personaBuscada)
        {
            return list.Remove(personaBuscada);
        }
    }
}
