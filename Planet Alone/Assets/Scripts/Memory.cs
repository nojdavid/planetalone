using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviour {
    public Dictionary<string, Action_Dialogue> memory;


    // Use this for initialization
    void Start () {
        /*
        memory = new Dictionary<string, Action_Dialogue>()
        {
            { "Hostile_item", new Action_Dialogue("Hostile_item")},
            { "Grab", new Action_Dialogue("Grab")},
            { "Throw", new Action_Dialogue("Throw")},
            { "Recovery", new Action_Dialogue("Recovery", no_threat)},
            { "Shake", new Action_Dialogue("Shake", mediium_threat)},
            { "Greeting", new Action_Dialogue("Greeting", -mediium_threat)},
            { "Hitting", new Action_Dialogue("Hitting", high_threat)},
            { "Instruction", new Action_Dialogue("Instruction", -high_threat) }
       };
       */
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
