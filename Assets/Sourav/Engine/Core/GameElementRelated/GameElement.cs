using Sourav.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.GameElementRelated
{
	public abstract class GameElement : MonoBehaviour
	{
		private static ApplicationGame _app;

		[SerializeField] protected bool isInactive;
		
		protected ApplicationGame App
		{
			get
			{
				if (_app == null)
				{
					_app = GameObject.FindObjectOfType<ApplicationGame>();
				}

				return _app;
			}
		}

		private void Start()
		{
			Init();
		}

		public virtual void Init()
		{
			//Init;
		}

		public void SetActivity(bool value)
		{
			isInactive = value;
		}
		public bool GetIsInactive()
		{
			return isInactive;
		}
	}
}
