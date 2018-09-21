using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour{
	public Texture2D deathFlash;
	
	void OnTriggerEnter(Collider other){
		other.GetComponent<Rigidbody>().Sleep();
		other.transform.position=new Vector3(0,8,0);
		other.GetComponent<Rigidbody>().velocity=Vector3.zero;
		other.GetComponent<Rigidbody>().WakeUp();
	}
}

