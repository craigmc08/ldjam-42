using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BoundsShimmer : MonoBehaviour {

    public float offsetSpeed = 0.1f;

    Material material;
    float offset = 0;

    float mainOffset = 0;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (!GameController.Playing) return;

        offset += offsetSpeed * Time.deltaTime;
        material.SetFloat("_Offset", offset);

        float distortion = (1f - GameController.boundController.Progress) * 0.2f + 0.2f;
        material.SetFloat("_Amount", distortion);

        mainOffset += GameController.collectableGenerator.moveSpeed * Time.deltaTime;
        material.SetFloat("_MainOffset", mainOffset);
    }
}
