using System.Collections;
using Sourav.DebugRelated;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.ButtonRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.ButtonRelated
{
	[RequireComponent(typeof(UnityEngine.UI.Button))]
	public class ButtonScript : GameElement
	{
		[SerializeField] private ButtonType type;
		private UnityEngine.UI.Button button;

		private void Awake()
		{
			button = GetComponent<UnityEngine.UI.Button>();
		}
		
		void Start ()
		{
			StartCoroutine(WaitAndAttachToController());
		}

		private IEnumerator WaitAndAttachToController()
		{
			yield return new WaitForSeconds(0.1f);
			button.onClick.AddListener(() =>
			{
				ButtonController[] c = Resources.FindObjectsOfTypeAll<ButtonController>();
				if(c.Length == 1)
				{
					ButtonController bc = c[0];
					bc.OnButtonPressed(type);
				}
				else
				{
					D.LogError("c is not ButtonController");
				}
			});
		}
	}
}
