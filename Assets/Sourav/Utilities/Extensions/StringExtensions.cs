using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Utilities.Extensions
{
	public static class StringExtensions 
	{
		public static List<int> GetSubWordIndices(this string word, string subWord)
		{
			List<int> indices = new List<int>();
			List<char> buffer = new List<char>();
			List<int> bufferIndices = new List<int>();

			int subindex = 0;
			bool subWordStarted = false;

			
			//ALARM //ARM
			for (int i = 0; i < word.Length; i++)
			{
				Debug.Log("word = " + word[i]);
				Debug.Log("subword = "+subWord[subindex]);
				
				if (word[i] == subWord[subindex]) // w = A, sw = A, si = 0, i = 0 true, w = L, sw = R false, w = A, sw = A
				{
					if (subindex < subWord.Length) //sb.l = 3, si = 0, true
					{
						if (!subWordStarted)
						{
							subWordStarted = true; //true
						}
					}
				}
				else //subwordStarted = false, subindex = 0
				{
					if (word[i] == subWord[0])
					{
						subindex = 0;
						buffer.Clear();
						bufferIndices.Clear();
						
						if (!subWordStarted)
						{
							subWordStarted = true; //true
						}
					}
					else 
					{
						if (subWordStarted)
						{
							subWordStarted = false;
							subindex = 0;
						}
					}
				}
				

				if (subWordStarted) //true
				{
					buffer.Add(word[i]); //A 
					bufferIndices.Add(i); //0

					subindex++; //1 
				}
				else
				{
					buffer.Clear();
					bufferIndices.Clear();
				}

				if (subindex == subWord.Length) //true
				{
					string str = ""; // ""
					for (int j = 0; j < buffer.Count; j++)
					{
						str += buffer[j].ToString();
					}
					//str = out

					if (str == subWord) //true
					{
						for (int j = 0; j < bufferIndices.Count; j++)
						{
							indices.Add(bufferIndices[j]);
						}
						//0 1 2 5 6 7 
					}
					bufferIndices.Clear();
					buffer.Clear();
					subindex = 0;
					subWordStarted = false;
				}
			}
			
			return indices;
		}
	}
	
	//out
	
	//outbydo
	//dooutby
	//dobytou
	//dobyout
	//outbyoutbyou
}
