using System.Collections.Generic;
using System.IO;

namespace Sourav.ReadCSV
{
	public class ReadTsv
	{
		private List<string> _lines;

		private List<Line> lines;

		public List<string> ReadFromTsv(string path)
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
					string[] dataFromLine = data.Split('\t');
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
}
