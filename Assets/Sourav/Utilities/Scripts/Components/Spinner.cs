using UnityEngine;

namespace Sourav.Utilities.Scripts.Components
{
	public class Spinner : MonoBehaviour
	{
		[SerializeField] private float multiplier;
		[SerializeField] private SpinningDirection direction;
		[SerializeField] private Axis axis;

		private void FixedUpdate()
		{
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

	public enum Axis
	{
		X,
		Y,
		Z
	}
}
