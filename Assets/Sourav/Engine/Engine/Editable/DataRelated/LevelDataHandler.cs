using System.Collections.Generic;
using UnityEngine;
using GameElement = Sourav.Engine.Core.GameElementRelated.GameElement;

namespace Sourav.Engine.Editable.DataRelated
{
	public class LevelDataHandler : GameElement
	{
		public List<LineData> lineData;
		public Sprite levelImage;
		public int numberOfLines;
		public List<AnswerLetters> lettersOfAnswer;
		public int lengthOfAnswer;
		public List<int> answerTileOrder;
		public List<int> blankSpaceIndex;
		public string levelAnswer;
		public string currentAnswer;
		public List<int> answerIndices;
		public List<int> answeredIndex;
		public List<string> answeredLetters;
		public int nextAnswerPos;
		public int nextAnswerIndex;
		public int levelReward;
		public bool isNormalSafeShow;

		
		public Vector3 nextPosition;

	}

	[System.Serializable]
	public class LineData
	{
		public int lineNumber;
		public string lineContent;
		public List<LineLetterAndIndex> lineLettersIndices;
		public int lineLength;
		public bool isPrefilled;
	}

	[System.Serializable]
	public class LineLetterAndIndex
	{
		public int letterAnswerIndex;
		public string letter;
	}

	[System.Serializable]
	public class AnswerLetters
	{
		public List<int> answerIndex;
		public string letter;
	}
}