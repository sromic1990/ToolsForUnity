using System;
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
		
		[SerializeField][ReadOnly] private bool canSpin;

		#region SPINNING RELATED
		public void StartSpinning()
		{
			// D.Log("START SPIN");
			canSpin = true;
		}
		
		public void StopSpin()
		{
			// D.Log("STOP SPIN");
			canSpin = false;
		}

		public void Reset()
		{
			StopSpin();
			transform.rotation = Quaternion.identity;
		}

		public bool IsSpinning
		{
			get { return canSpin; }
		}
		#endregion

		#region DIRECTION RELATED
		public void ChangeDirection()
		{
			// D.Log("Change Direction");
			if (direction == SpinningDirection.Clockwise)
			{
				direction = SpinningDirection.Anticlockwise;
			}
			else
			{
				direction = SpinningDirection.Clockwise;
			}
		}

		public SpinningDirection GetDirection()
		{
			return direction;
		}
		#endregion
		
		#region MULTIPIER RELATED
		public float Multiplier
		{
			get { return multiplier; }
			set
			{
				multiplier = value;
			}
		}
		#endregion
		
		#region MONO METHODS
		private void Start()
		{
			if (spinOnAwake)
			{
				StartSpinning();
			}
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

			transform.Rotate(vectorAxis, multiplier * Time.fixedUnscaledDeltaTime * (int)direction);
		}
		#endregion
	}

	public enum SpinningDirection : int
	{
		Clockwise = -1,
		Anticlockwise = 1
	}
}
