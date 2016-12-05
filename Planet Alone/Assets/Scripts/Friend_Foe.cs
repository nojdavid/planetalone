using UnityEngine;
using System.Collections;

public class Friend_Foe : MonoBehaviour {
    public float minimum = -1.0F;
    public float maximum = 1.0F;
    float Friend_or_foe_meter = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Mathf.Clamp(Friend_or_foe_meter, minimum, maximum);
    }

    public float GetFOF()
    {
        return Mathf.Round(Friend_or_foe_meter);
    }
}
