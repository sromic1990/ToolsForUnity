using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.ButtonRelated;
using Sourav.Engine.Editable.ControllerRelated;
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
			button.onClick.AddListener(() =>
			{
				Core.ControllerRelated.Controller c = App.GetController(ControllerType.ButtonController);
				if(c is ButtonController)
				{
					ButtonController bc = (ButtonController) c;
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
