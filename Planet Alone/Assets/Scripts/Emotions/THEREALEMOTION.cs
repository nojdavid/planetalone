using UnityEngine;
using System.Collections;

public class THEREALEMOTION : MonoBehaviour {
    public int shift = 5;
    public float soft = 0.9f;
    public Robot_State rs;
    
    // Use this for initialization
    //start at 3
    float velocity_utility()
    {
        return utility_calculation(rs.checkVelocity());
    }

    float FoV_utility()
    {
        if (!rs.is_vr_player_in_field_of_view_of_robot())
        {
            float current_time = Time.time;
            //getTimeOutOfSight(); //range 0 - 10 sec
            return utility_calculation(current_time - rs.getTimeOutOfSight());
        }
        return 0f;
    }

    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        float vel_util = velocity_utility();
        float FoV_util = FoV_utility();
        //Debug.Log("vel util " + vel_util);
        //Debug.Log("FoV util " + FoV_util);
	}


    float utility_calculation(float util)
    {
        return 1 / (1 + (Mathf.Exp(-util + shift) * Mathf.Pow( soft , (-util + shift))));
    }

}
