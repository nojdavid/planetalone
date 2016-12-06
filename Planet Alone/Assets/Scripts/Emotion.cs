using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Emotion{
    AudioClip[] sources;
    AssetDatabase asd;
    Dialogue data;
    const int friend = 0;
    const int foe = 1;
    AudioSource audiosource;
    Utility utility;
    public float rating = 0;
    //public List<Tuple> dialogue_options = new List<Tuple>();
    public Dictionary<int, List<Tuple>> dialogue = new Dictionary<int, List<Tuple>>()
    {
        { friend, new List<Tuple>() },
        { foe, new List<Tuple>() }
    };


    public Emotion(int emotion_id) {

        audiosource = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<AudioSource>();
        utility = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Utility>();
        data = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Dialogue>();
        for (int i = 0; i < data.database.Count; ++i)
        {
            if (data.database[i].Emotion == emotion_id && data.database[i].Action_tag == "Idle")
            {
                dialogue[data.database[i].FOF].Add(new Tuple(data.database[i].Sources, 0));
            }
        }
    }


    /// <summary>
    /// 
    /// We need more work here?
    /// 
    /// </summary>
    public void Talk(int fof, int index, ref float ls)
    {
        if (index < dialogue[fof].Count)
        {
            audiosource.clip = dialogue[fof][index].A;
            if (audiosource.clip != null)
            {
                dialogue[fof][index].count += 1;
                audiosource.Play();
                ls = Time.time;
            }
        } 
    }

}


