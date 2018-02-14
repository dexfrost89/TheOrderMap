using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_gen : MonoBehaviour
{
    public bool[][][] map;

    // Use this for initialization
    void Start () {
        int val;
        map = new bool[1000][][];
        for(int i = 0; i < 1000; i++)
        {
            map[i] = new bool[1000][];
            for(int j = 0; j < 1000; j++)
            {
                val = Random.Range(0, 8);
                map[i][j] = new bool[3];
                for(int k = 0; k < 3; k++)
                {
                    map[i][j][k] = false;
                    if(val % 2 == 1)
                    {

                        map[i][j][k] = true;
                    }
                    val /= 2;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
