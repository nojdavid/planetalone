using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Action_Dialogue
{

    AudioClip[] sources;
    AssetDatabase asd;
    Dialogue data;
    const int friend = 0;
    const int foe = 1;
    /*
    public Dictionary<int, List<Tuple>> dialogues = new Dictionary<int, List<Tuple>>()
    {
        { friend, new List<Tuple>() },
        { foe, new List<Tuple>() }
    };
    */
    public List<List<Tuple>> dialogue = new List<List<Tuple>>{new List<Tuple>(),new List<Tuple>()};

    public Action_Dialogue(string tag)
    {
        data = GameObject.FindGameObjectWithTag("Robot_Head").GetComponent<Dialogue>();
        for (int i = 0; i < data.database.Count; ++i)
        {
            if (data.database[i].Action_tag == tag)
            {
                dialogue[data.database[i].FOF].Add(new Tuple(data.database[i].Sources, 0));
            }
        }
    }
}
