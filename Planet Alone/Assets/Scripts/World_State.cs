using UnityEngine;
using System.Collections;
using VRTK;

public class World_State : MonoBehaviour {

    private bool robot_interact;
    public VRTK_InteractGrab rightController;
    public VRTK_InteractGrab leftController;
    public GameObject rightHandItem;
    public GameObject leftHandItem;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        updateInteract();
        updatePlayerItem();

	}

    public Pair get_Items_In_Hands()
    {
        return new Pair(rightHandItem, leftHandItem);
    }

    void updatePlayerItem()
    {
        rightHandItem = rightController.GetGrabbedObject();
        leftHandItem = leftController.GetGrabbedObject();
    }

    
    void updateInteract()
    {
        if (( rightHandItem != null && rightHandItem.CompareTag("Robot_Head")) || 
            (leftHandItem != null && leftHandItem.CompareTag("Robot_Head")))
        {
            robot_interact = true;
        }else
        {
            robot_interact = false;
        }
    }
    

    
    public GameObject getRobotObj()
    {
        if (rightHandItem != null)
        {
            if (rightHandItem.CompareTag("Robot_Head"))
            {
                return rightHandItem;
            }


        } else if (leftHandItem != null) {
            if (leftHandItem.CompareTag("Robot_Head"))
            {
                return leftHandItem;
            }
        }
        
        return null;
        
    }
    
}
