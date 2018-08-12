using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundController : MonoBehaviour {

    public Transform boundLeft;
    public Transform boundRight;

    public float maxDist = 5f;
    public float minDist = 0.5f;
    public float boundSpeed = 0.2f;
    public float transformSmoothing = 12f;

    [Header("don't touch")]
    float progress = 1f;

    Vector3 leftOrigin;
    Vector3 rightOrigin;

    void Start()
    {
        leftOrigin = boundLeft.localPosition;
        leftOrigin.x = 0;
        rightOrigin = boundRight.localPosition;
        rightOrigin.x = 0;
    }

	void FixedUpdate()
    {
        if (!GameController.Playing) return;

        Progress -= boundSpeed * Time.fixedDeltaTime;

        var targetLeft = leftOrigin + Vector3.left * Distance;
        var targetRight = rightOrigin + Vector3.right * Distance;

        var smoothing = transformSmoothing * Time.fixedDeltaTime;
        boundLeft.localPosition = Vector3.Lerp(boundLeft.localPosition, targetLeft, smoothing);
        boundRight.localPosition = Vector3.Lerp(boundRight.localPosition, targetRight, smoothing);
    }
    public void UpdateNoSmoothing()
    {
        boundLeft.localPosition = leftOrigin + Vector3.left * Distance;
        boundRight.localPosition = rightOrigin + Vector3.right * Distance;
    }

    public float Progress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = Mathf.Clamp01(value);
        }
    }
    public float Distance
    {
        get
        {
            return Progress * (maxDist - minDist) + minDist;
        }
    }
}
