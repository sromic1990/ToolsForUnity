using UnityEngine;
using System.Collections;

public class CallbacksExample : MonoBehaviour{
	private bool buttonActivated;
	private string text;

	void Start(){
		tween();
	}
	
	void OnGUI (){
		GUILayout.Label (text);
		if(buttonActivated){
			if(GUILayout.Button ("Replay")){
				reset();
				tween();
			}
		}
	}
	
	void reset(){
		transform.position=new Vector3(0,0,0);
		transform.rotation=Quaternion.identity;
		text="";
		buttonActivated=false;
	}
	
	void tween(){
		iTween.MoveBy(gameObject,iTween.Hash("y",2,"time",2,"easetype","easeinoutexpo","onstart","segmentStarted","onstartparams","Rise started...","onupdate","segmentUpdated","onupdateparams","move","oncomplete","segmentComplete","oncompleteparams","Rise stopped."));
		iTween.RotateBy(gameObject,iTween.Hash("z",.25,"time",1,"easetype","easeinoutback","delay",2.5,"onstart","segmentStarted","onstartparams","Rotate started...","onupdate","segmentUpdated","onupdateparams","rotate","oncomplete","segmentComplete","oncompleteparams","Rotate stopped."));
		iTween.MoveTo(gameObject,iTween.Hash("y",0,"time",.9,"easetype","easeInExpo","delay",4,"onstart","segmentStarted","onstartparams","Fall started...","onupdate","segmentUpdated","onupdateparams","move","oncomplete","segmentComplete","oncompleteparams","Fall stopped."));
		
		//Notice that since we ae putting shake on the camera we need to tell the callbacks which target contains the method they need to call,
		iTween.ShakePosition(Camera.main.gameObject,iTween.Hash("y",.5,"time",.8,"delay",4.95,"onstart","segmentStarted","onstartparams","Shake started...","onstarttarget",gameObject,"onupdate","segmentUpdated","onupdateparams","shake","onupdatetarget",gameObject,"oncomplete","allComplete","oncompletetarget",gameObject));
	}
	
	void segmentStarted(string textToDisplay){
		text+=textToDisplay + "\n";
	}
	
	void segmentUpdated(string method){
		switch(method){
		case "move":
			Debug.Log("Height at, " + transform.position.y);
			break;
			
		case "rotate":
			Debug.Log("Rotation at, " + transform.eulerAngles.z);
			break;
			
		case "shake":
			Debug.Log("Shake at, " + Camera.main.transform.position.y);
			break;
		}
	}
	
	void segmentComplete(string textToDisplay){
		text+=textToDisplay + "\n";
	}
	
	void allComplete(){
		buttonActivated=true;	
		text += "All done animating!";
	}
}

