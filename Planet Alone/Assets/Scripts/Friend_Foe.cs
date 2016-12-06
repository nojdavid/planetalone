using UnityEngine;
using System.Collections;

public class Friend_Foe : MonoBehaviour {
    public int minimum = 0;
    public int maximum = 1;
    float Friend_or_foe_meter = 0.0f;
	
	// Update is called once per frame
	void Update () {
        Friend_or_foe_meter = Mathf.Clamp(Friend_or_foe_meter, minimum, maximum);
        //Debug.Log("FOFFUCK " + Friend_or_foe_meter);
    }


    public void AddFOF(float score)
    {
        Friend_or_foe_meter += score * Time.deltaTime;
    }

    public int GetFOF()
    {
        return (int)Mathf.Round(Friend_or_foe_meter);
    }
}
