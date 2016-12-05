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
    int FriendOrFoe = 0;// x < -0.2 = Foe , -0.2<x<0.2 = Neurtual, 0.2<x = Friend
    List<Emotion> emotions;
    public Dictionary<string, Action_Dialogue> Action;

    Friend_Foe fof_class;
    bool firstTimeSeen = false;
    float lastPickedUp;
    List<System.Func<string>> Rule_Set;
    string action_tag;
    string previous_action_tag;
    public bool idle = false;
    private bool collide_with_hammer = false;
    private float hostileItem_InRange;

    const float no_threat = 0.0f;
    const float low_threat = 0.001f;
    const float mediium_threat = 0.005f;
    const float high_threat = 0.01f;


    // Use this for initialization
    void Start()
    {

        audiosource = GetComponent<AudioSource>();
        utility = GetComponent<Utility>();
        fof_class = GetComponent<Friend_Foe>();


        Rule_Set = new List<System.Func<string>>
        {
            Greeting_Rule,
            Shake_Rule,
            Grab_Rule,
            Throw_Rule,
            Recovery_Rule,
            HostileItem_Rule,
            Instruction_Rule
        };
        


        Action = new Dictionary<string, Action_Dialogue>()
        {
            { "Hostile_item", new Action_Dialogue("Hostile_item", mediium_threat)},
            { "Grab", new Action_Dialogue("Grab", no_threat)},
            { "Throw", new Action_Dialogue("Throw", mediium_threat)},
            { "Recovery", new Action_Dialogue("Recovery", no_threat)},
            { "Shake", new Action_Dialogue("Shake", mediium_threat)},
            { "Greeting", new Action_Dialogue("Greeting", -mediium_threat)},
            { "Hitting", new Action_Dialogue("Hitting", high_threat)},
            { "Instruction", new Action_Dialogue("Instruction", -high_threat) }
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

        FriendOrFoe = fof_class.GetFOF();
       // check_head();
        velocity = checkVelocity();

        //Updates last time player is seen
        //is_vr_player_in_field_of_view_of_robot(); 
        List<float> utility_value = utility.GetScore();
        
       emotions[0].rating = utility_value[0];
       emotions[1].rating = utility_value[1];
       emotions[2].rating = utility_value[2]; // quiet


        Action_tag_determine();
        Action_Dialogue();

       
        if (!audiosource.isPlaying)
        {
            //Memory Dialogue
            Idle_Dialogue();
            idle = true;
        }
        
    }

    // determine the action_tag
    void Action_tag_determine()
    {
        foreach (System.Func<string> rule in Rule_Set)
        {
            //is null if no action is satisfied
            action_tag = rule();

            if (action_tag != null)
            {
                // affect friend or foe status
                fof_class.AddFOF(Action[action_tag].get_weight());
                break;
            }
        }
    }

    public float hostileItem_inRange_Time()
    {
        if (is_vr_player_in_field_of_view_of_robot() && (world_state.rightHandItem != null && world_state.rightHandItem.CompareTag("hostile_item")) || (world_state.leftHandItem != null && world_state.leftHandItem.CompareTag("hostile_item")))
        {
            hostileItem_InRange = Time.time;
            return hostileItem_InRange;
        }
        return 0f;
        
    }

    public float hammer_vel_onCollision()
    {
        if ((world_state.rightHandItem != null && world_state.rightHandItem.CompareTag("hostile_item")) || (world_state.leftHandItem != null && world_state.leftHandItem.CompareTag("hostile_item")) && collide_with_hammer )
        {
            return controller_velocity.getVelocity();
        }else
        {
            return 0;
        }
       
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.CompareTag("hostile_item"))
        {
            collide_with_hammer = true;
        }
        else
        {
            collide_with_hammer = false;
        }
    
    }

    void Action_Dialogue()
    {
        
        //ADD EXCEPTION TO THE REPEAT RULE FOR SHAKE

        if (Repeat_Rule()) return;
        if(action_tag == previous_action_tag && action_tag == "Shake" ) Debug.Log("Shacking");
        //Debug.Log("actiobn tag" + action_tag  + ", prev   " + previous_action_tag);
        //if (action_tag == null) return;
        // find the best quote; 
        int minIndex = 0;
        int min_count = int.MaxValue;

        for (int i = 0; i < Action[action_tag].dialogue[(int)FriendOrFoe].Count; ++i)
        {
            if (Action[action_tag].dialogue[(int)FriendOrFoe][i].count < min_count)
            {
                min_count = Action[action_tag].dialogue[(int)FriendOrFoe][i].count;
                minIndex = i;
            }
        }
       // Debug.Log("Before" + minIndex + "," + Action[action_tag].dialogue[(int)FriendOrFoe][minIndex].count);
        Action[action_tag].Talk((int)FriendOrFoe, minIndex, ref Last_time_speaking);
        previous_action_tag = action_tag == null ? previous_action_tag : action_tag;
        //action_tag = null;
    }

    void Idle_Dialogue()
    {
        
        if (Time.time - Last_time_speaking > 2)
        {
                // find the emotion 
            float maxRating = float.MinValue;
            int maxindex = 0;
            for (int i = 0; i < emotions.Count; ++i)
            {

                //Debug.Log(i + " " + emotions[i].rating);
                if (emotions[i].rating > maxRating)
                {
                    maxRating = emotions[i].rating;
                    maxindex = i;
                }
            }

            // find the best quote; 
            int minIndex = 0;
            int min_count = int.MaxValue;
            for (int i = 0; i < emotions[maxindex].dialogue[FriendOrFoe].Count; ++i)
            {
                if (emotions[maxindex].dialogue[FriendOrFoe][i].count < min_count)
                {
                    min_count = emotions[maxindex].dialogue[FriendOrFoe][i].count;
                    minIndex = i;
                }
            }
            //Debug.Log(maxindex);
            //Debug.Log("Before" + minIndex + "," + emotions[maxindex].dialogue[(int)FriendOrFoe][minIndex].count);
            emotions[maxindex].Talk(FriendOrFoe,minIndex, ref Last_time_speaking);
        }
    }

    bool Repeat_Rule()
    {
        if (action_tag == null || (action_tag == previous_action_tag && (action_tag != "Shake")))
            return true;
        return false;
    }

    
    string Greeting_Rule()
    {
        //Greetings: not seen 7 - 10 seconds and distance is <= 20
        if ((Time.time - time_player_not_insight >= Random.Range(0, 3) + 7 && Vector3.Distance(this.transform.position, vr_player.transform.position) <= 20) || firstTimeSeen == false)
        {
            firstTimeSeen = true;
            return "Greeting";
        }
        return null;
    }
    string Grab_Rule()
    {
       //Grab: picked up and time last 3 - 8
        if (check_hand("Robot_Head") && previous_action_tag != "Shake") //&& Time.time - lastPickedUp == Random.Range(1, 5) + 2 
        {
            return "Grab";
        }
        return null;
    }

    string Throw_Rule()
    {
        //Throw: velcoity and not picked up
        if (checkVelocity() >= 3.5f && !check_hand("Robot_Head"))
        {
            return "Throw";
        }
        return null;
    }

    string Shake_Rule()
    {
        //if(check_hand("Robot_Head"))Debug.Log(checkVelocity());
        //Shake:     
        if (check_hand("Robot_Head") && checkVelocity() >= 4f )// && previous_action_tag == "Grab")
        {
            return "Shake";
        }
        return null;
    }

    string Recovery_Rule()
    {
        
        if (previous_action_tag == "Throw" && this.transform.position.y  < 0.5f && !check_hand("Robot_Head"))
        {
            return "Recovery";
        }
        return null;
    }

    string HostileItem_Rule()
    {
        if (check_hand("hostile_item"))
        {
            return "Hostile_item";
        }
        return null;
    }

    string Instruction_Rule()
    {
        if (check_hand("gun"))
        {
            return "Instruction";
        }
        return null;
    }

    bool check_hand(string tag)
    {
        Pair Items = world_state.get_Items_In_Hands();
        if ((Items.x != null && Items.x.CompareTag(tag)) || ((Items.y != null && Items.y.CompareTag(tag))))
        {
            return true;
        }
        return false;
    }

    public List<Emotion> getEmotions()
    {
        return emotions;
    }
    void check_head()
    {
        //Null if robot head is not in vr player's hands
        if(my_head != world_state.getRobotObj())
        {
            lastPickedUp = Time.time;
        }
    }

    public float checkVelocity()
    { //UPDATE: anxiety, comfort, frustation
        float localVel = GetComponent<Rigidbody>().velocity.magnitude;

        if (world_state.rightHandItem != null && world_state.rightHandItem.CompareTag("Robot_Head"))
        {
            localVel = controller_velocity.getVelocity() * 3;
        }
        else if (world_state.leftHandItem != null && world_state.leftHandItem.CompareTag("Robot_Head"))
        {
            localVel = controller_velocity.getVelocity() * 3;
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
