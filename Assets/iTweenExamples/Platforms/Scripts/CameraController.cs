using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform target;
	
	void FixedUpdate ()
	{	
		Vector3 pos = target.position;
		pos.z=-14;
		pos.y=target.position.y+2f;
		iTween.MoveUpdate(gameObject,pos,.8f);
	}
}