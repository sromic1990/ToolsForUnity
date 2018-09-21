using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour{
	public GameObject ticker1;
	public GameObject ticker2;
	public GameObject ticker3;
	float left = -5f;
	float right = 5f;
	public AudioClip slide;
	public AudioClip impact;
	
	void Start(){
		Move("right");
	}
	
	void Move(string direction){
		if(direction == "right"){
			iTween.MoveTo (gameObject, iTween.Hash ("x",right,"easetype",iTween.EaseType.easeInExpo,"time",1.4,"delay",.5,"oncomplete","ApplySlam","oncompleteparams","right"));
			iTween.Stab(gameObject,slide,1.1f);
			iTween.Stab(gameObject,impact,1.85f);
		}else{
			iTween.MoveTo (gameObject, iTween.Hash ("x",left,"easetype",iTween.EaseType.easeInExpo,"time",1.4,"delay",.5,"oncomplete","ApplySlam","oncompleteparams","left"));
			iTween.Stab(gameObject,slide,1.1f);
			iTween.Stab(gameObject,impact,1.85f);
		}
	}
	
	void ApplySlam(string direction){
		if(direction == "left"){
			Move("right");
			ticker1.SendMessage("Slam",45);
			ticker2.SendMessage("Slam",35);
			ticker3.SendMessage("Slam",25);
			iTween.ShakePosition(Camera.main.gameObject,new Vector3(.4f,0,0),.4f);
		}else{
			Move("left");
			ticker1.SendMessage("Slam",-25);
			ticker2.SendMessage("Slam",-35);
			ticker3.SendMessage("Slam",-45);
			iTween.ShakePosition(Camera.main.gameObject,new Vector3(-.4f,0,0),.4f);
		}
	}
}

