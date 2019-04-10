using System;
using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.UIPresets
{
	public class MoveAndScaleToPosition : GameElement 
	{
		[SerializeField] private Transform initialTransform;
		[SerializeField] private Transform gotoTransform;
		[SerializeField] private Vector3 initialScale;
		[SerializeField] private Vector3 goToScale;

		public void GoToPosition(float time, Ease ease, Action onComplete = null)
		{
			if (onComplete != null)
			{
				transform.DOLocalMove(gotoTransform.localPosition, time).SetEase(ease).OnComplete(() => { onComplete();});
			}
			else
			{
				transform.DOLocalMove(gotoTransform.localPosition, time).SetEase(ease);
			}
			transform.DOScale(goToScale, time).SetEase(ease);
		}

		public void ReturnToOriginalPosition()
		{
			transform.localPosition = initialTransform.localPosition;
			transform.localScale = initialScale;
		}
	}
}
