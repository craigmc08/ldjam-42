using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ShieldShimmer : MonoBehaviour
{

    public float offsetSpeed = 0.1f;

    Material material;
    float offset = 0;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += offsetSpeed * Time.deltaTime;
        material.SetFloat("_Offset", offset);
    }
}
