using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utility : MonoBehaviour {
    public int shift = 5;
    public float soft = 0.9f;
    public Robot_State rs;
    List<float> utility_value;
    public const float default_comfort = 0.25f; //constant comfort increase
    public const float default_frustration = - 0.15f; //constant frustration decrease

    void Awake()
    {
        utility_value = new List<float>();
        utility_value.Add(0f);
        utility_value.Add(0f);
    }

    // Update is called once per frame
    void Update()
    {
        float vel_util = velocity_utility();
        float FoV_util = FoV_utility();

        utility_value[0] = frustration_rating(vel_util, FoV_util) + default_frustration;
        utility_value[0] = utility_value[0] < 0 ? 0 : utility_value[0];
        /*
        if (utility_value[0]<0)
        {
            utility_value[0] = 0;
        }
        */
        utility_value[1] = comfort_rating(vel_util, FoV_util) + default_comfort;


        //Debug.Log("vel util " + vel_util);
        //Debug.Log("FoV util " + FoV_util);
    }

    //
    float frustration_rating(float vel, float fov)
    {
        float temp = 0;

        temp = vel + fov;
        if(temp > 1)
        {
            temp = 1;
        }

        return temp;
    }

    float comfort_rating(float vel, float fov)
    {
        float temp = 0;

        temp = 1 - vel;

        return temp;
    }


    public List<float> GetScore()
    {
        //Debug.Log(value[0] + "," + value[1]);
        return utility_value;
    }

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

    float quiet_utility()
    {
        float current_time = Time.time;
        return utility_calculation_opp(current_time - rs.getlastSpeakTime());
    }

    float utility_calculation(float util)
    {
        return 1 / (1 + (Mathf.Exp(-util + shift) * Mathf.Pow(soft, (-util + shift))));
    }

    float utility_calculation_opp(float util)
    {
        return 1 / (1 + (Mathf.Exp(util + shift) * Mathf.Pow(soft, (util + shift))));
    }
}
