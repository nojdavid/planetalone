using UnityEngine;
using System.Collections;

public class Robot_State : MonoBehaviour {

    public Frustration frustration;
    public Comfort comfort;
    public Saddness saddness;
    public Anxiety anxiety;
    public Bored bored;
    public Playfullness playfullness;

    public World_State world_state;
    private GameObject my_head;
    public ControllerVelocity controller_velocity;
    public GameObject vr_player;
    private float velocity;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check_head();
        velocity = checkVelocity();
        Debug.Log(velocity);
    }

    void check_head()
    {
        my_head = world_state.getRobotObj();
    }

    float checkVelocity()
    { //UPDATE: anxiety, comfort, frustation
        float localVel = GetComponent<Rigidbody>().velocity.magnitude;


        if (world_state.rightHandItem != null && world_state.rightHandItem.CompareTag("Robot_Head"))
        {
            localVel = controller_velocity.getVelocity();
        }
        else if (world_state.leftHandItem != null && world_state.leftHandItem.CompareTag("Robot_Head"))
        {
            localVel = controller_velocity.getVelocity();
        }

        return localVel; //.magnitude;
    }

    bool is_vr_player_in_field_of_view_of_robot()
    {
        Vector3 screenPoint = GetComponent<Camera>().WorldToViewportPoint(vr_player.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
