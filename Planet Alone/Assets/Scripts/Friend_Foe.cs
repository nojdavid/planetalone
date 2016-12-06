using UnityEngine;
using System.Collections;

public class Friend_Foe : MonoBehaviour {
    public float minimum = 0F;
    public float maximum = 1.0F;
    float Friend_or_foe_meter = 0.5f;
	
	// Update is called once per frame
	void Update () {
        Mathf.Clamp(Friend_or_foe_meter, minimum, maximum);
    }


    public void AddFOF(float score)
    {
        Friend_or_foe_meter = score * Time.deltaTime;
    }

    public int GetFOF()
    {
        return (int)Mathf.Round(Friend_or_foe_meter);
    }
}
