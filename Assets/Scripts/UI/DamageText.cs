using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Text))]
public class DamageText : MonoBehaviour {
    public float driftAmount = 5f;
    public float driftProgressPower = 2f;
    public float lifetime = 0.5f;

    float aliveFor = 0;
    RectTransform rectTransform;
    public int Damage;
    Text text;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }
    void Start()
    {
        aliveFor = 0;
    }

    float Progress
    {
        get
        {
            return aliveFor / lifetime;
        }
    }
    float Drift
    {
        get
        {
            return Mathf.Pow(Progress, driftProgressPower) * driftAmount;
        }
    }

    void FixedUpdate()
    {
        rectTransform.position -= Vector3.up * Drift;
        aliveFor += Time.fixedDeltaTime;
        rectTransform.position += Vector3.up * Drift;
    }
    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - Progress);
        text.text = "-" + Damage;

        if (Progress >= 1)
        {
            Destroy(gameObject);
        }
    }
}
