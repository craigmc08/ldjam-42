using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundFlash : MonoBehaviour {
    public Color flashColor = Color.white;
    public float hold = 0f;
    public float fade = 1f;

    Color restingColor;
    Image image;
    float timeInPhase;
    bool holding;
    bool fading;

    void OnEnable()
    {
        image = GetComponent<Image>();
        restingColor = image.color;

        timeInPhase = 0;
        holding = true;
        fading = false;
    }
    void Update()
    {
        if (holding)
        {
            image.color = flashColor;
            if (timeInPhase > hold)
            {
                holding = false;
                fading = true;
                timeInPhase = 0;
            }
        } else if (fading)
        {
            if (timeInPhase > fade)
            {
                fading = false;
                timeInPhase = 0;
            } else
            {
                image.color = Color.Lerp(flashColor, restingColor, timeInPhase / fade);
            }
        }

        timeInPhase += Time.deltaTime;
    }

    void OnDisable()
    {
        fading = false;
        holding = false;
        timeInPhase = 0;
        image.color = restingColor;
    }
}
