using UnityEngine;
using System.Collections;

public class GunParentChecker : MonoBehaviour {
    public bool weapon_grabbed = false;

	void FixedUpdate () {
        if (transform.parent != null) // && transform.parent.parent != null
        {

            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation =  Quaternion.Euler(-230, 0, 0);
            weapon_grabbed = true;
        }
        else
        {
            weapon_grabbed = false;
        }
    }

    public bool is_grabbed()
    {
        return weapon_grabbed;
    }
}
