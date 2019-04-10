using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Components
{
	public class Blinker : MonoBehaviour
	{
		public float timeInBetween;
		[SerializeField] private GameObject target;
		
		[SerializeField]private bool _isEnabled;
		private bool _show;
		private float _elapsed;

		public void SetTarget(GameObject target)
		{
			this.target = target;
		}
		
		#if ODIN
		[Sirenix.OdinInspector.Button()]
		#else
		[Sourav.Utilities.Scripts.Attributes.Button()]
		#endif
		public void StartBlinking(bool show)
		{
			_isEnabled = true;
			_elapsed = 0;
			_show = show;
		}

		public void StopBlinking(bool hide = true)
		{
//			Debug.Log("StopBlinking");
			
			_isEnabled = false;

			if (hide)
			{
				target.Hide();
			}
			else
			{
				target.Show();
			}
		}

		private void FixedUpdate()
		{
			if (_isEnabled)
			{
				_elapsed += Time.fixedDeltaTime;
	
				if (_elapsed > timeInBetween)
				{
					_show = !_show;
					_elapsed = 0;
				}
	
				if (_show)
				{
					target.Show();
				}
				else
				{
					target.Hide();
				}
			}
		}
	}
}
