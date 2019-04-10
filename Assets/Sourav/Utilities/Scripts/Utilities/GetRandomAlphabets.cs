using UnityEngine;

namespace Sourav.Utilities.Scripts.Utilities
{
	public static class GetRandomAlphabets
	{
		private static char[] _capitalletters = new[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
			'V', 'W', 'X', 'Y', 'Z'
		};

		private static char[] _smallLetters = new[]
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
			'v', 'w', 'x', 'y', 'z'
		};

		public static string GetRandomLetterAsString(LetterGetType type)
		{
			int random = Random.Range(0, _capitalletters.Length);

			string letter = "";

			switch (type)
			{
				case LetterGetType.Small:
					letter =  _smallLetters[random].ToString();
					break;
				
				case LetterGetType.Capital:
					letter = _capitalletters[random].ToString();
					break;
				
				case LetterGetType.Either:
					int randomChoose = Random.Range(0, 2);
					if (randomChoose == 0)
					{
						letter =  _smallLetters[random].ToString();
					}
					else
					{
						letter = _capitalletters[random].ToString();
					}

					break;
			}

			return letter;
		}
		
		public static char GetRandomLetterAsCharacter(LetterGetType type)
		{
			int random = Random.Range(0, _capitalletters.Length);

			char letter = ' ';

			switch (type)
			{
				case LetterGetType.Small:
					letter = _smallLetters[random];
					break;
				
				case LetterGetType.Capital:
					letter = _capitalletters[random];
					break;
				
				case LetterGetType.Either:
					int randomChoose = Random.Range(0, 2);
					if (randomChoose == 0)
					{
						letter =  _smallLetters[random];
					}
					else
					{
						letter = _capitalletters[random];
					}

					break;
			}

			return letter;
		}
	}

	public enum LetterGetType
	{
		Capital,
		Small,
		Either
	}
}
