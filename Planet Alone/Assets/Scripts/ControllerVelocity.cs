using UnityEngine;
using System.Collections;

public class ControllerVelocity : MonoBehaviour {

   
    //SteamVR_Controller.Device device;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    private Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public GunParentChecker gpc; // A class that will let us know if the seed gun is currently being held by player.
    public ShootProjectile shooter;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (gpc.is_grabbed() && controller.GetHairTriggerDown())
        {
            shooter.shootseeds();
        }

        if (controller.GetPressUp(trigger_button))
        {

        }
    }
    public float getVelocity()
    {
        //device = SteamVR_Controller.Input((int)trackedObj.index);
        float v = controller.velocity.magnitude;
        return v;
    }
}
