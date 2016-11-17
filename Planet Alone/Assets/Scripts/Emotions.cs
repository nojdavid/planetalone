using UnityEngine;
using System.Collections;

public class Emotions : MonoBehaviour {

    private float frustration = 0;
    private float comfort = 0;
    private float saddness = 0;
    private float anxiety = 0;
    private float bored = 0;
    private float playfulness = 0;

    public World_State world_state;
    private GameObject my_head;

    private float velocity;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        check_head();
        velocity = checkVelocity();
        Debug.Log(velocity);
	}

    void check_head()
    {
        my_head = world_state.getRobotObj();
    }

    float checkVelocity(){ //UPDATE: anxiety, comfort, frustation
        float localVel = GetComponent<Rigidbody>().velocity.magnitude;
        return localVel; //.magnitude;
    }
}

