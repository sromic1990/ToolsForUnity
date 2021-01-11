using Sourav.Utilities.Scripts.Attributes;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Camera.Util
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class OrthographicUtilities : MonoBehaviour
	{

		[SerializeField] private Transform Left;
		[SerializeField] private Transform Right;
		
		
		
		[SerializeField]private Vector3 leftLimit;
		public Vector3 LeftLimit
		{
			get { return leftLimit; }
		}

		[SerializeField]private Vector3 rightLimit;
		public Vector3 RightLimit
		{
			get { return rightLimit; }
		}

		[SerializeField]private Vector3 height;
		public Vector3 Height
		{
			get { return height; }
		}

		[SerializeField]private float width;
		public float Width
		{
			get { return width; }
		}

		private UnityEngine.Camera camera;


		private void Awake()
		{
			camera = GetComponent<UnityEngine.Camera>();
			PopulateLimits();
		}

		private void LateUpdate()
		{
			PopulateLimits();
		}

		private void PopulateLimits()
		{
			float heightY = 2 * camera.orthographicSize;
			var position = camera.transform.position;
			height = new Vector3(position.x, position.y + heightY, position.z);
			width = heightY * camera.aspect;
			float halfWidth = width * 0.5f;
			leftLimit = new Vector3(position.x - halfWidth, position.y, position.z);
			rightLimit = new Vector3(position.x + halfWidth, position.y, position.z);
			
			SetupLimits();
		}

		[Button()]
		public void SetupLimits()
		{
			if (Left != null)
			{
				Left.position = leftLimit;
			}

			if (Right != null)
			{
				Right.position = rightLimit;
			}
		}
	}
}
