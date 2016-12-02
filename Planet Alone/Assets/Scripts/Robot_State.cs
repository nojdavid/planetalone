using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Robot_State : MonoBehaviour
{
    /*
        hostile_item
        grab
        throw
        idle
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

        Talk(Idle_Diologue());
    }

    void Talk(Tuple audio)
    {
        if (Time.time - Last_time_speaking > 5 && !audiosource.isPlaying)
        {
            audiosource.clip = audio.A;
            //audiosource.clip = emotions[0].dialogue[1][0].A;
            //audiosource.clip = Action["Grab"].dialogue[0][0].A;
            if (audiosource.clip != null)
            {
                audio.count += 1;
                audiosource.Play();
                Debug.Log(audio.count);
            }
            Last_time_speaking = Time.time;
        }
    }

    Tuple Idle_Diologue()
    {
        // find the emotion 
        float maxRating = float.MinValue;
        Emotion maxEmotion = new Emotion(3);
        foreach (Emotion emotion in emotions)
        {
            if (emotion.rating > maxRating)
            {
                maxRating = emotion.rating;
                maxEmotion = emotion;
            }
        }

        // find the best quote
        Tuple best_quote = new Tuple();
        int max_count = int.MinValue;
        foreach (Tuple entry in maxEmotion.dialogue[(int)FriendOrFoe])
        {
            if (entry.count > max_count)
            {
                maxRating = entry.count;
                best_quote = entry;
            }
        }
        return best_quote;
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
