using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour {

    [Header("General")]
    public float spawnDepth = 15f;
    public float moveSpeed = 15f;

    [Header("Point Collectable")]
    public GameObject pointCollectable;
    [Tooltip("Amount of seconds to wait between spawning")]
    public float pointSpawnCooldown = 0.5f;

    [Header("Bound Bonus Collectable")]
    public GameObject boundBonusCollectable;
    public float boundBonusSpawnTimer = 1f;
    [Range(0, 1)]
    public float boundBonusSpawnChance = 0.2f;
    public int maxBoundBonusTriesWithoutSpawning = 5;

    [Header("Spike Obstacle")]
    public GameObject spikeObstacle;
    public float spikeSpawnCooldown = 1.5f;
    [Range(0, 1)]
    public float spikeSpawnMultipleChance = 0.2f;
    [Range(0, 10)][Tooltip("Maximum number of spikes to spawn in 1 attempt")]
    public int maxSpikes = 3;

    float timeLastPointSpawned = 0;
    Queue<GameObject> pointCollectables;

    float timeLastBoundBonusTry = 0;
    Queue<GameObject> boundBonusCollectables;
    int boundBonusTriesSinceSpawn = 0;

    float timeLastSpikeSpawned = 0;
    Queue<GameObject> spikeObstacles;

    void Start()
    {
        pointCollectables = new Queue<GameObject>();
        boundBonusCollectables = new Queue<GameObject>();
        spikeObstacles = new Queue<GameObject>();
    }

    void FixedUpdate()
    {
        if (!GameController.Playing) return;

        transform.position += Vector3.back * moveSpeed * Time.fixedDeltaTime;

        SpawnPoints();
        SpawnBoundBonuses();
        SpawnSpikes();
    }

    void SpawnPoints()
    {
        if (Time.time - timeLastPointSpawned > pointSpawnCooldown)
        {
            var obj = GameObject.Instantiate(pointCollectable);
            var boundDistance = GameController.boundController.Distance - 0.5f;
            obj.transform.position = Vector3.right * Random.Range(-boundDistance, boundDistance) + Vector3.forward * spawnDepth;
            obj.transform.parent = transform;
            timeLastPointSpawned = Time.time;

            pointCollectables.Enqueue(obj);
        }

        if (pointCollectables.Count > 5)
        {
            Destroy(pointCollectables.Dequeue());
        }
    }
    void SpawnBoundBonuses()
    {
        if (Time.time - timeLastBoundBonusTry > boundBonusSpawnTimer)
        {
            boundBonusTriesSinceSpawn++;
            timeLastBoundBonusTry = Time.time;
            if (Random.value < boundBonusSpawnChance || boundBonusTriesSinceSpawn >= maxBoundBonusTriesWithoutSpawning)
            {
                var obj = GameObject.Instantiate(boundBonusCollectable);
                var boundDistance = GameController.boundController.Distance - 0.4f;
                obj.transform.position = Vector3.right * Random.Range(-boundDistance, boundDistance) + Vector3.forward * spawnDepth;

                obj.transform.parent = transform;

                boundBonusCollectables.Enqueue(obj);

                boundBonusTriesSinceSpawn = 0;
            }
        }

        if (boundBonusCollectables.Count > 5)
        {
            Destroy(boundBonusCollectables.Dequeue());
        }
    }
    void SpawnSpikes()
    {
        if (Time.time - timeLastSpikeSpawned > spikeSpawnCooldown)
        {
            timeLastSpikeSpawned = Time.time;

            int number = 1;
            for (int i = 1; i < maxSpikes; i++)
            {
                if (Random.value < spikeSpawnMultipleChance)
                {
                    number++;
                } else
                {
                    break;
                }
            }

            GameObject spikeHolder = new GameObject();
            spikeHolder.name = "Spike Holder";
            spikeHolder.transform.parent = transform;
            spikeHolder.transform.position = Vector3.forward * spawnDepth;
            for (int i = 0; i < number; i++)
            {
                GameObject spike = GameObject.Instantiate(spikeObstacle);
                spike.transform.parent = spikeHolder.transform;
                var boundWidth = GameController.boundController.Distance - 0.6f;
                spike.transform.localPosition = Vector3.right * Random.Range(-boundWidth, boundWidth);
            }
            spikeObstacles.Enqueue(spikeHolder);

            if (spikeObstacles.Count > 5)
            {
                Destroy(spikeObstacles.Dequeue());
            }
        }
    }

    public void Reset()
    {
        EmptyQueue(pointCollectables);
        EmptyQueue(boundBonusCollectables);
        EmptyQueue(spikeObstacles);
    }

    void EmptyQueue(Queue<GameObject> queue)
    {
        while (queue.Count > 0)
        {
            Destroy(queue.Dequeue());
        }
    }
}
