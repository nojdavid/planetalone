using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Emotion{
    public float rating = 0;
    public List<Tuple> dialogue = new List<Tuple>();
    Dictionary<string, List<Tuple>> dict;
    AudioClip[] sources;
    AssetDatabase asd;
    Dialogue data;

    public Emotion(int id) {
        data = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Dialogue>();
        for (int i = 0; i < data.database.Count; ++i)
        {
            if(data.database[i].Emotion == id)
            {
                dialogue.Add(new Tuple(data.database[i].Sources, 0));
            }
        }
    }
}
