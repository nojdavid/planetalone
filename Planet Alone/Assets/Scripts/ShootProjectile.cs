using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShootProjectile : MonoBehaviour {
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    private Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public Rigidbody projectile;
    public float speed = 10;
    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
   /* void Update () {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        if (controller.GetPressDown(trigger_button))
        {
            Rigidbody clone = (Rigidbody) Instantiate(projectile, transform.position, transform.rotation);
            clone.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            //Destroy(clone.gameObject, 5);
        }

        if (controller.GetPressUp(trigger_button))
        {
            
        }
    } */

    public void shootseeds()
    {

        Rigidbody clone = (Rigidbody)Instantiate(projectile, transform.position, transform.rotation);
        clone.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        //Destroy(clone.gameObject, 5);
        


    }
}
