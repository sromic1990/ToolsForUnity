using System.Collections;
using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.Events;

namespace Sourav.UIPresets
{
	public class MoveAndScaleHandler : GameElement
	{
		[SerializeField] private Ease ease;
		[SerializeField] private float time;
		[SerializeField] private float waitAfterComplete;
		[SerializeField] private MoveAndScaleToPosition uiToAffect;

		public UnityEvent events;

		#if ODIN
		[Sirenix.OdinInspector.Button()]
		#else
		[Sourav.Utilities.Scripts.Attributes.Button()]
		#endif
		public void InvokeMovement(bool invokeEvents)
		{
			if (invokeEvents)
			{
				uiToAffect.GoToPosition(time, ease, OnCompleteAnimation);
			}
			else
			{
				uiToAffect.GoToPosition(time, ease);
			}
		}

		private void OnCompleteAnimation()
		{
			StartCoroutine(WaitAfterAnimationComplete());
		}

		private IEnumerator WaitAfterAnimationComplete()
		{
			yield return new WaitForSeconds(waitAfterComplete);
			events?.Invoke();
		}
	}
}
