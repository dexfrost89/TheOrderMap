using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class get_debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public GameObject a, b, c;

    public void get()
    {
        bool[] localmap;
        localmap = c.GetComponent<map_gen>().map[Convert.ToInt32(a.GetComponent<InputField>().text)][Convert.ToInt32(b.GetComponent<InputField>().text)];
        int val = 0;
        if (localmap[0])
        {
            val += 1;
        }
        if (localmap[1])
        {
            val += 2;
        }
        if (localmap[2])
        {
            val += 4;
        }
        gameObject.GetComponent<Text>().text = val.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
