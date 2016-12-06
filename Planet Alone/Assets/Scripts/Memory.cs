using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviour {
    public Dictionary<string, Queue<float>> memory;
    private float time_to_forget = 0.2f;
    Utility ut;

    // Use this for initialization
    void Start () {
        ut = GetComponent<Utility>();
        // memory of past player action
        memory = new Dictionary<string, Queue<float>>()
        {
            { "Hostile_item", new Queue<float>()},
            { "Throw", new Queue<float>()},
            { "Shake", new Queue<float>()},
            { "Hitting", new Queue<float>()},
            { "Greeting", new Queue<float>()},
            { "Instruction", new Queue<float>() }
       };
       
    }
	
    public void RecordActionToMemory(string tag)
    {
        if(memory.ContainsKey(tag)) memory[tag].Enqueue(Time.time);
        Debug.Log(tag + "  "  +memory[tag].Count);
    }

    public float Rating(string tag, float weight)
    {
        float rating = 0;
        while (memory[tag].Count > 0 && ut.Memoryutility(memory[tag].Peek()) < time_to_forget) memory[tag].Dequeue();
        foreach (float i in memory[tag])
        {
            rating += ut.Memoryutility(i);
        }
        rating *= weight;
        return rating;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
