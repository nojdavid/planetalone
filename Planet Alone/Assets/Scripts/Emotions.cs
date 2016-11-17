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
    public ControllerVelocity controller_velocity;

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
        

        if (world_state.rightHandItem != null && world_state.rightHandItem.CompareTag("Robot_Head"))
        {
            localVel = controller_velocity.getVelocity();
        }else if (world_state.leftHandItem != null && world_state.leftHandItem.CompareTag("Robot_Head"))
        {
            localVel = controller_velocity.getVelocity();
        }
        
        return localVel; //.magnitude;
    }
}

