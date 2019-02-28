using System.Collections.Generic;
using System.IO;

namespace Sourav.Utilities.Scripts.ReadCSVFromPath
{
	public class ReadCsv
	{
		private List<string> _lines;

		private List<Line> lines;

		public List<string> ReadFromCsv(string path)
		{
			int lineIndex = 0;
			
			_lines  = new List<string>();
			
			lines = new List<Line>();
			
			StreamReader strReader = new StreamReader(path);
			bool eof = false;

			while (!eof)
			{
				string data = strReader.ReadLine();
				if (data == null)
				{
					eof = true;
				}
				if (data != null)
				{		
					string[] dataFromLine = data.Split(',');
					if (!string.IsNullOrEmpty(dataFromLine[0]))
					{
						Line line = new Line();
						line.line = data;
						line.index = lineIndex;
						for (int i = 0; i < dataFromLine.Length; i++)
						{
							Word word = new Word();
							word.index = i;
							word.word = dataFromLine[i];
							line.words.Add(word);
						}
						_lines.Add(data);
						lines.Add(line);
						lineIndex++;
					}
				}
			}

			return _lines;
		}

		public List<Line> GetLines()
		{
			return lines;
		}
	}
	
	[System.Serializable]
	public class Line
	{
		public string line;
		public int index;
		public List<Word> words;

		public Line()
		{
			words = new List<Word>();
		}
	}
	
	[System.Serializable]
	public class Word
	{
		public int index;
		public string word;
	}
}
