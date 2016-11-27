using UnityEngine;
using System.Collections;

public class GunParentChecker : MonoBehaviour {
    public bool weapon_grabbed = true;

	void FixedUpdate () {
        if (transform.parent != null) // && transform.parent.parent != null
        {

            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation =  Quaternion.Euler(-230, 0, 0);
        }
    }

    public bool is_grabbed()
    {
        return weapon_grabbed;
    }
}
