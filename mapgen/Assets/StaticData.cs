using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticData : MonoBehaviour {

    public static string LoadResourceTextfile(string name)
    {
        string filePath = "JsonData/" + name;

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }
    
    [Serializable]
    public class Chunks
    {
        public List<Chunk> list;
    }
    
    
    public Chunks chunks;

    private bool ArrayHasTag(string[] array, string tag)
    {
        foreach(var i in array)
        {
            if(i.ToLower() == tag.ToLower())
            {
                return true;
            }
        }
        return false;
    }
    

    // Use this for initialization
    void Awake () {

        string json;
        DontDestroyOnLoad(this.gameObject);
        
        
        

        json = LoadResourceTextfile("Chunk");
        json = "{\"list\":" + json + "}";
        chunks = JsonUtility.FromJson<Chunks>(json);
        

        foreach (var i in chunks.list)
        {
            Debug.Log(i + " " + i.keyName);
            i.Init();
        }

    }
}
