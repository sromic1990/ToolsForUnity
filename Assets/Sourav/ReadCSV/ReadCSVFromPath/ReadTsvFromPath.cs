using System.Collections.Generic;
using UnityEngine;

namespace Sourav.ReadCSV
{
	public class ReadTsvFromPath : MonoBehaviour
	{
		[SerializeField] private string Path;
		[SerializeField] private List<Line> lines;
		

		[Sirenix.OdinInspector.Button()]
		public void PopulateData()
		{
			string path = Application.dataPath + "/" + Path;
			string[] pathElements = path.Split('.');
			if (pathElements[pathElements.Length - 1] != "tsv")
			{
				path += ".tsv";
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
