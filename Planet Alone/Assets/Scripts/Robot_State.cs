using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Robot_State : MonoBehaviour
{
    /*
     *         idle
     *         
        hostile_item
        grab
        throw
        recovery
        shake
        greeting
        hitting
        instruction
    */

    public World_State world_state;
    private GameObject my_head;
    public ControllerVelocity controller_velocity;
    public GameObject vr_player;
    AudioSource audiosource;
    Utility utility;
    private float velocity;
    float time_player_not_insight;
    float Last_time_speaking = 0;
    float FriendOrFoe = 0;// x < -0.2 = Foe , -0.2<x<0.2 = Neurtual, 0.2<x = Friend
    List<Emotion> emotions;
    public Dictionary<string, Action_Dialogue> Action;

    // Use this for initialization
    void Start()
    {

        audiosource = GetComponent<AudioSource>();
        utility = GetComponent<Utility>();


        Action = new Dictionary<string, Action_Dialogue>()
        {
            { "Hostile_item", new Action_Dialogue("Hostile_item")},
            { "Grab", new Action_Dialogue("Grab")},
            { "Throw", new Action_Dialogue("Throw")},
            { "Recovery", new Action_Dialogue("Recovery")},
            { "Shake", new Action_Dialogue("Shake")},
            { "Greeting", new Action_Dialogue("Greeting")},
            { "Hitting", new Action_Dialogue("Hitting")},
            { "Instruction", new Action_Dialogue("Instruction") }
        };
        emotions = new List<Emotion> // Idle
        {
            new Emotion(0),//frustration
            new Emotion(1),//comfort
            new Emotion(2)// quiet
        };
    }

    // Update is called once per frame
    void Update()
    {
        check_head();
        velocity = checkVelocity();
        is_vr_player_in_field_of_view_of_robot();
        List<float> temp = utility.GetScore();
        
       emotions[0].rating += temp[0];
       emotions[1].rating += temp[1];

        Idle_Diologue();
    }

    void Idle_Diologue()
    {
        if (Time.time - Last_time_speaking > 2 && !audiosource.isPlaying)
        {
                // find the emotion 
            float maxRating = float.MinValue;
            int maxindex = 0;
            for (int i = 0; i < emotions.Count; ++i)
            {
                if (emotions[i].rating > maxRating)
                {
                    maxRating = emotions[i].rating;
                    maxindex = i;
                }
            }

            // find the best quote; 
            int minIndex = 0;
            int min_count = int.MaxValue;
            for (int i = 0; i < emotions[maxindex].dialogue[(int)FriendOrFoe].Count; ++i)
            {
                if (emotions[maxindex].dialogue[(int)FriendOrFoe][i].count < min_count)
                {
                    min_count = emotions[maxindex].dialogue[(int)FriendOrFoe][i].count;
                    minIndex = i;
                }
            }
            Debug.Log("Before" + minIndex + "," + emotions[maxindex].dialogue[(int)FriendOrFoe][minIndex].count);
            emotions[maxindex].Talk((int)FriendOrFoe,minIndex, ref Last_time_speaking);
            Debug.Log("After" + emotions[maxindex].dialogue[(int)FriendOrFoe][minIndex].count);
        }
    }

    public List<Emotion> getEmotions()
    {
        return emotions;
    }
    void check_head()
    {
        my_head = world_state.getRobotObj();  //Null if robot head is not in vr player's hands
    }

    public float checkVelocity()
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

    public bool is_vr_player_in_field_of_view_of_robot()
    {
        RaycastHit hit;
        Vector3 rayDirection = vr_player.transform.position - transform.position;
        float fieldOfViewRange = 50;
        float rayRange = 10;
        float minPlaterDetectDistance = 3;

        var distanceToPlayer = Vector3.Distance(transform.position, vr_player.transform.position);
       
        if (Physics.Raycast(transform.position, rayDirection, out hit))
        { // If the player is very close behind the player and in view the enemy will detect the player
            if ((hit.transform.tag == "MainCamera") && (distanceToPlayer <= minPlaterDetectDistance))
            {
               // Debug.Log("Can see player");
                time_player_not_insight = Time.time;
                return true;
            }
        }

        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewRange)
        { // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, rayRange))
            {
                if (hit.transform.tag == "MainCamera")
                {
                    //Debug.Log("Can see player");
                   time_player_not_insight = Time.time;
                    return true;
                }
                else
                {
                   // Debug.Log("Can not see player");
                    return false;
                }
            }
        }
       // Debug.Log("END OF FUNC");
        return false;
    }

    public float getlastSpeakTime() {
        return Last_time_speaking;
    }

    public float getTimeOutOfSight()
    {
        return time_player_not_insight;
    }


    public string FriendNFoe() {
        if (FriendOrFoe > 0.2) {
            return "Friend";
        } else if (FriendOrFoe < -0.2) {
            return "Foe";
        } else {
            return "Neutral";
        }
    }
}
