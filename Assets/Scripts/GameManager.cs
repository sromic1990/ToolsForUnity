using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Player;
    public Transform Target;

    public float easeDelay = 0.1f;

    private bool start = false;
    
    private void Start()
    {
        Invoke("StartMoving", 2.0f);
    }

    private void StartMoving()
    {
        start = true;
    }

    private void Update()
    {
        if(start)
        {
            Player.position += (Target.position - Player.position) * easeDelay;
        }
    }
}
