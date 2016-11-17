using UnityEngine;
using System.Collections;

public class ControllerVelocity : MonoBehaviour {

   
    //SteamVR_Controller.Device device;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    public float getVelocity()
    {
        //device = SteamVR_Controller.Input((int)trackedObj.index);
        float v = controller.velocity.magnitude;
        return v;
    }
}
