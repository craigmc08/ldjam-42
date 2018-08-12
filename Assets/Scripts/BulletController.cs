using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float damage = 0f;
    [Tooltip("Lifetime of bullet, in seconds")]
    public float lifetime = 1f;

    float creation;

    void Start()
    {
        creation = Time.time;
    }
    void Update()
    {
        if (Time.time - creation > lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CollectableID collectableID = other.GetComponent<CollectableID>();

        if (collectableID != null && collectableID.type == CollectableType.Obstacle)
        {
            ObstacleHealth oh = other.GetComponent<ObstacleHealth>();
            if (oh != null)
            {
                oh.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
