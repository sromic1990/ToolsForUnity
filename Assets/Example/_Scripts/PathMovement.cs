using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public Transform[] path;
    public float time;
    public float delay;
    public iTween.EaseType easetype;
    #region CHANGE
    public GameObject test;
    #endregion

    // Use this for initialization
    void Start ()
    {
        GetCorrectPositionsForMovement();
        //StartTween();	
    }

    private void GetCorrectPositionsForMovement()
    {
        Vector3[] newPaths = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            newPaths[i] = Vector3.zero;     
        }

        newPaths[0] = transform.position;
        for(int i = 1; i < path.Length; i++)
        {
            newPaths[i] = path[i].position - path[i - 1].position;
            newPaths[i] += newPaths[i - 1];
        }

        for (int i = 0; i < path.Length; i++)
        {
            path[i].position = newPaths[i];
        }
    }

    public void StartTween()
    {
        transform.position = path[0].transform.position;
        iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", time, "delay", delay, "easetype", easetype));
    }

    void OnDrawGizmos()
    {
        iTween.DrawPath(path);
    }
}
