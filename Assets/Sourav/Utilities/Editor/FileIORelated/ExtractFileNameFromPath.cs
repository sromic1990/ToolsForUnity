namespace Sourav.Utilities.Editor.FileIORelated
{
    public class ExtractFileNameFromPath : UnityEditor.Editor 
    {
        public static string ExtractName(string path)
        {
            string fileName = "";

            string[] names = path.Split('/');
            fileName = names[names.Length - 1];

            return fileName;
        }
    }
    
}
