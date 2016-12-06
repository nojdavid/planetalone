using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utility : MonoBehaviour {
    public int current_shift = 5;
    public float current_soft = 0.9f;
    public Robot_State rs;
    //List<float> utility_value;
    public float quiet_ut = 0;
    public const float default_comfort = 0.025f; //constant comfort increase
    public const float default_frustration = - 0.005f; //constant frustration decrease

    void Update()
    {
        quiet_ut = quiet_utility();
    }
   



    float quiet_utility()
    {
        float current_time = Time.time;
        return utility_calculation_opp(current_time - rs.getlastSpeakTime(), 1f, -7f, 0f);
    }
      ////
        /////
            //
              //
              //    
              //
              //
               //
                ////
                  ////////////////

    float utility_calculation_opp(float util, float q_soft, float q_shift, float y_shift)
    {
        return y_shift + 1 / (1 + 1f * (Mathf.Exp(util + q_shift) * Mathf.Pow(q_soft, (util + q_shift))));
    }

    public float Memoryutility(float j)
    {
        float i = Time.time - j;
        return 1 / (1 + 0.001f * (Mathf.Exp(i -10) * Mathf.Pow(0.5f, (i - 10))));
    }

    public float EmotionUtility(float rating)
    {
        return 10 / (1 + (Mathf.Exp(-rating + 6) * Mathf.Pow(100000f, -rating)));
    }


}



/*
   void Awake()
   {
       utility_value = new List<float>();
       utility_value.Add(0f);
       utility_value.Add(0f);
       utility_value.Add(0f);
   }

   // Update is called once per frame
   void Update()
   {


       float vel_util = velocity_utility();
       float FoV_util = FoV_utility();
       float vel_collide_hammer = hammer_utility();
       float hostileItem_inRange = hammer_time_utility();

       utility_value[0] = frustration_rating(vel_util, 1 - FoV_util, vel_collide_hammer);// * Time.deltaTime;
       utility_value[1] = comfort_rating(1 - vel_util, FoV_util, hostileItem_inRange);// * Time.deltaTime;
       utility_value[2] = quiet_utility();
   }

   //Combine Utility to enter in emotion
   float frustration_rating(float vel, float fov, float vel_col)
   {
       return (vel + fov + vel_col)/3;
   }

   float comfort_rating(float vel, float fov, float hos_inRange)
   {
       return hos_inRange >0? (vel - hos_inRange) / 2 : (vel + fov) / 2;
   }



   public List<float> GetScore()
   {
       //Debug.Log(value[0] + "," + value[1]);
       return utility_value;
   }

   float velocity_utility()
   {
       return utility_calculation(rs.checkVelocity(), current_shift, current_soft, -.2f);
   }

   float FoV_utility()
   {
       if (!rs.is_vr_player_in_field_of_view_of_robot())
       {
           //getTimeOutOfSight(); //range 0 - 10 sec
           float current_time = Time.time;
           return utility_calculation(current_time - rs.getTimeOutOfSight(), current_shift, current_soft, 0.0f);
       }
       return 0f;
   }

   float hammer_utility()
   {
       return utility_calculation(rs.hammer_vel_onCollision(), 4, current_soft, 0);
   }

   float hammer_time_utility()
   {
       return utility_calculation(rs.hostileItem_inRange_Time(), 4, 5, 0);
   }


                       //////////
                      //
                     //
                    //
                   //
                 //
               //
   /////////////

   float utility_calculation(float util, float shift, float soft, float y_shift)
   {
       return y_shift + 1 / (1 + (Mathf.Exp(-util + shift) * Mathf.Pow(soft, (-util + shift))));
   }
   */
