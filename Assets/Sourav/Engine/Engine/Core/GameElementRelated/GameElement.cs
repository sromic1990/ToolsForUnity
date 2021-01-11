using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.GameElementRelated
{
	public abstract class GameElement : MonoBehaviour
	{
		private static App _app;

		[SerializeField] protected bool isInactive;
		
		protected App AppGame
		{
			get
			{
				if (_app == null)
				{
					App[] app = Resources.FindObjectsOfTypeAll<App>();;
					_app = app[0];
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
