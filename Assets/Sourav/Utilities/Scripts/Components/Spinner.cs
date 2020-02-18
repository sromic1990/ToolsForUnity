using Sirenix.OdinInspector;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Components
{
	public class Spinner : MonoBehaviour
	{
		[SerializeField] private float multiplier;
		[SerializeField] private SpinningDirection direction;
		[SerializeField] private Axis axis;

		[SerializeField] private bool spinOnAwake;
		
		private bool canSpin;

		private void Start()
		{
			if (spinOnAwake)
			{
				canSpin = true;
			}
		}

		public void StartSpinning()
		{
			canSpin = true;
		}
		
		public void StopSpin()
		{
			canSpin = false;
		}
		
		private void FixedUpdate()
		{
			if (!canSpin)
			{
				return;
			}
			
			Vector3 vectorAxis = Vector3.zero;
			switch (axis)
			{
				case Axis.X:
					vectorAxis = Vector3.right;
					break;

				case Axis.Y:
					vectorAxis = Vector3.up;
					break;

				case Axis.Z:
					vectorAxis = Vector3.forward;
					break;
			}

			transform.Rotate(vectorAxis, multiplier * Time.fixedDeltaTime * (int)direction);
		}
	}

	public enum SpinningDirection : int
	{
		Clockwise = -1,
		Anticlockwise = 1
	}
}
