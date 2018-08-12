using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour {

    [System.Serializable]
    public class BobAnim
    {
        public Vector3 direction;
        public float period;
        public float phaseOffset;
        public bool randomPhase = false;
        public float amplitude;

        float phase;

        float b
        {
            get
            {
                return Mathf.PI / period / 2;
            }
        }
        float c
        {
            get
            {
                return phaseOffset / period * b;
            }
        }

        public BobAnim(Vector3 direction, float period, float phaseOffset, bool randomPhase, float amplitude)
        {
            this.direction = direction;
            this.period = period;
            this.phaseOffset = phaseOffset;
            if (randomPhase)
            {
                this.phaseOffset = Random.Range(0, period);
            }
            this.amplitude = amplitude;

            phase = 0;
        }

        public Vector3 Next(float dt)
        {
            phase += dt;
            float mag = amplitude * Mathf.Sin(b * phase + c);
            return direction * mag;
        }
    }

    public BobAnim[] bobAnims;

    Vector3 lastMove;

    void Start()
    {
        lastMove = Vector3.zero;
    }

	void Update()
    {
        if (!GameController.Playing) return;

        transform.localPosition -= lastMove;
        Vector3 nextMove = Vector3.zero;
        foreach (BobAnim bobAnim in bobAnims)
        {
            nextMove += bobAnim.Next(Time.fixedDeltaTime);
        }
        transform.localPosition += nextMove;
        lastMove = nextMove;
	}
}
