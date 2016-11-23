using UnityEngine;
using System.Collections;

public class Pair : MonoBehaviour {

    public GameObject x;
    public GameObject y;

    public Pair(GameObject X, GameObject Y) {
        this.x = X;
        this.y = Y;
    }
}
