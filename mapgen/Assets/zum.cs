using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zum : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            gameObject.transform.localScale += new Vector3(1, 1);
        }
        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            gameObject.transform.localScale += new Vector3(-1, -1);
        }
        gameObject.transform.localScale -= new Vector3(-1, -1) * (Input.GetAxis("Mouse ScrollWheel"));
        
    }
}
