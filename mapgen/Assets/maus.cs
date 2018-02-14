using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maus : MonoBehaviour {


    public Vector3 start, startmy;
    public bool fl;
	// Use this for initialization
	void Start () {
        fl = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(fl == false)
        {
            startmy = gameObject.transform.position;
            start = Input.mousePosition;
        }
        if(Input.GetMouseButtonDown(0))
        {
            fl = true;
        }

        if(fl)
        {

            gameObject.transform.position = startmy + Input.mousePosition - start;
        }

        if(Input.GetMouseButtonUp(0))
        {
            fl = false;
        }
	}
}
