using System.Collections.Generic;
using UnityEngine;

namespace Sourav.ReadCSV
{
    public class ReadTxtFromPath : MonoBehaviour
    {
        [SerializeField] private string Path;
        [SerializeField] private List<Line> lines;
		

        // [Sirenix.OdinInspector.Button()]
        public void PopulateData()
        {
            string path = Application.dataPath + "/" + Path;
            string[] pathElements = path.Split('.');
            if (pathElements[pathElements.Length - 1] != "txt")
            {
                path += ".txt";
            }
			
            ReadTsv rcsv = new ReadTsv();
            rcsv.ReadFromTsv(path);
            lines = rcsv.GetLines();
        }

        public List<Line> GetLines()
        {
            return lines;
        }
    }
}