using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chunk{

    public float speed;
    public string keyName;
    public string[] sprite00;
    public string[] sprite01;
    public string[] sprite02;
    public string[] sprite10;
    public string[] sprite11;
    public string[] sprite12;
    public string[] sprite20;
    public string[] sprite21;
    public string[] sprite22;

    List<List<List<string>>> chunkSprites = new List<List<List<string>>>();

    public string GetSprite(int i, int j)
    {
        int rand = Random.Range(0, chunkSprites[i][j].Count);
        return chunkSprites[i][j][rand];
    }

    public Chunk(Chunk g)
    {
        speed = g.speed;
        keyName = g.keyName;
        sprite00 = g.sprite00;
        sprite01 = g.sprite01;
        sprite02 = g.sprite02;
        sprite10 = g.sprite10;
        sprite11 = g.sprite11;
        sprite12 = g.sprite12;
        sprite20 = g.sprite20;
        sprite21 = g.sprite21;
        sprite22 = g.sprite22;
        Init();
    }

    public void Init()
    {
        chunkSprites = new List<List<List<string>>>();
        for (int i = 0; i < 3; i++)
        {
            List<List<string>> buf1 = new List<List<string>>();
            for(int j = 0; j < 3; j++)
            {
                List<string> buf2 = new List<string>();
                buf1.Add(buf2);
            }
            Debug.Log(chunkSprites);
            chunkSprites.Add(buf1);
        }
        Debug.Log(sprite00);
        foreach(var i in sprite00)
        {
            chunkSprites[0][0].Add(i);
        }
        foreach (var i in sprite01)
        {
            chunkSprites[0][1].Add(i);
        }
        foreach (var i in sprite02)
        {
            chunkSprites[0][2].Add(i);
        }
        foreach (var i in sprite10)
        {
            chunkSprites[1][0].Add(i);
        }
        foreach (var i in sprite11)
        {
            chunkSprites[1][1].Add(i);
        }
        foreach (var i in sprite12)
        {
            chunkSprites[1][2].Add(i);
        }
        foreach (var i in sprite20)
        {
            chunkSprites[2][0].Add(i);
        }
        foreach (var i in sprite21)
        {
            chunkSprites[2][1].Add(i);
        }
        foreach (var i in sprite22)
        {
            chunkSprites[2][2].Add(i);
        }
    }
}
