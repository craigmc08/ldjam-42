using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Camera orthoCamera;
    public float movementSpeed = 12f;
    public float movementSmoothing = 25f;
    [Range(0, 1)]
    [Tooltip("0 for no rotation, 1 for lots of it")]
    public float movementRotation = 0.2f;

    [Header("Shooting")]
    public GameObject bulletObject;
    public float bulletSpeed = 30f;
    public float bulletDamage = 15f;
    public float bulletCooldown = 0.2f;

    [Header("misc.")]
    public string boundTag = "bound";
    public ParticleSystem system1;
    public ParticleSystem system2;

    float velocity;
    bool mouseHeld;
    float lastTimeBulletShot = 0;

    Quaternion baseRot;

    float horizontalInput = 0;
    float mouseDeltaX = 0;

    float xPos = 0;

	void Start()
    {
        baseRot = transform.localRotation;
	}
    
    void Update()
    {
        if (!GameController.Playing) return;

        mouseHeld = Input.GetAxisRaw("Fire1") > 0;
        //if (Input.GetMouseButtonDown(0)) lastTimeBulletShot = Time.time - bulletCooldown;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        mouseDeltaX = Input.GetAxisRaw("Mouse X");
    }
	
	void FixedUpdate()
    {
        if (!GameController.Playing) return;

        if (GameController.Controlling)
        {
            UpdateMovement();
            UpdateShooting();
        }
    }

    Vector3 GetPosition(float dt)
    {
        if (GameController.Controlling)
        {
            var input = horizontalInput + mouseDeltaX * Settings.MouseSensitivity;
            xPos += movementSpeed * dt * input;
        }
        return new Vector3(xPos, transform.position.y, transform.position.z);
    }

    void UpdateMovement()
    {
        var targetPlayerPos = GetPosition(Time.fixedDeltaTime);
        var nextPlayerPos = Vector3.Lerp(transform.position, targetPlayerPos, movementSmoothing * Time.fixedDeltaTime);

        velocity = nextPlayerPos.x - transform.position.x;

        transform.position = nextPlayerPos;

        // Update rotation
        var rotMultiplier = Mathf.Abs(velocity) > 1 ? Mathf.Sign(velocity) : velocity;
        var rotation = Quaternion.AngleAxis(rotMultiplier * movementRotation * 90f, baseRot * Vector3.forward);
        transform.localRotation = baseRot * rotation;
    }

    void UpdateShooting()
    {
        if (mouseHeld)
        {
            if (Time.time - lastTimeBulletShot >= bulletCooldown)
            {
                CreateBullet();
                lastTimeBulletShot = Time.time;
            }
        }
    }
    void CreateBullet()
    {
        GameObject bullet = GameObject.Instantiate(bulletObject);
        bulletObject.transform.position = transform.position;

        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.position = transform.position;
        bulletRB.velocity = Vector3.forward * bulletSpeed;

        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.damage = bulletDamage;

        GameController.audioController.PlayShoot();
    }

    void OnTriggerEnter(Collider other)
    {
        CollectableID collectableID = other.gameObject.GetComponent<CollectableID>();
        
        if (collectableID != null)
        {
            switch (collectableID.type)
            {
                case CollectableType.Point:
                    GameController.PickupPointCrystal();
                    break;
                case CollectableType.BoundBonus:
                    GameController.PickupBoundBonus();
                    break;
                case CollectableType.Obstacle:
                    GameOver();
                    break;
            }
            Destroy(other.gameObject);
        } else if (other.tag == boundTag)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        system1.Stop();
        system2.Stop();
        GameController.EndGame();
    }

    public void Reset()
    {
        system1.Play();
        system2.Play();
        xPos = 0;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
