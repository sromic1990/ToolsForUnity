using UnityEngine;
using System.Collections;

public class PathExample : MonoBehaviour{
	public Transform[] path;
	private bool buttonActivated;
	
	void Start(){
		tween();
	}
	
	void OnGUI ()
    {
		if(buttonActivated)
        {
			if(GUILayout.Button ("Reset"))
            {
				reset();
				tween();
			}
		}
	}
	
	void OnDrawGizmos()
    {
		iTween.DrawPath(path);
	}
	
	void tween()
    {
		iTween.MoveTo(gameObject,iTween.Hash("path",path,"time",7,"orienttopath",true,"looktime",.6,"easetype","easeInOutSine","oncomplete","complete"));	
	}
	
	void reset()
    {
		buttonActivated=false;
		transform.position=new Vector3(0,0,0);
		transform.eulerAngles=new Vector3(0,0,0);
	}
	
	void complete(){
		buttonActivated=true;
	}
}

