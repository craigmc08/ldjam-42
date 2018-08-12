using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    [Tooltip("Degrees per second")]
    public float speed;
    public Vector3 axis;
    public bool randomAxis;

    void Start()
    {
        if (randomAxis)
        {
            axis = Random.insideUnitSphere;
        }
    }

    void FixedUpdate()
    {
        if (!GameController.Playing) return;

        var rot = speed * Time.fixedDeltaTime;
        var eul = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(axis.x * rot + eul.x, axis.y * rot + eul.y, axis.z * rot + eul.z);
    }
}
