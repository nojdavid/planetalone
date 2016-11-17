using UnityEngine;
using System.Collections;
public class Robot_Talk : MonoBehaviour
{
    public Transform farEnd;
    private Vector3 from;
    private Vector3 to;
    public float secondsForOneLength = 1f;
    public Vector3 length = new Vector3(0, (float)-.5, 0);

    void Start()
    {
        from = transform.localPosition;
        to = from + length;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(from, to,
         Mathf.SmoothStep(0f, 1f,
          Mathf.PingPong(Time.time * secondsForOneLength, 1f)
        ));
    }
}