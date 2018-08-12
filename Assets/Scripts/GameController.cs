using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static BoundController boundController;
    public static CollectableGenerator collectableGenerator;
    public static PlayerController playerController;
    public static AudioController audioController;
    public static DamageIndicator damageIndicator;
    public static UIStateManager uiStateManager;

    public static float scoreMultiplier = 1f;
    public static float pointsPerCrystal;
    public static float boundBonusPercentage;

    [Header("Settings")]
    public float baseScorePerSecond = 1f;
    public float maxScoreMultiplier = 3f;
    //[Range(0, 1)]
    //public float maxScoreMultBoundDistance = 0.25f;
    [Tooltip("Higher = score multiplier increases faster the closer the boundaries")]
    public float scorePower = 2f;

    public float _pointsPerCrystal = 5f;
    [Range(0, 1)]
    public float _boundBonusPercentage = 0.7f;

    [Header("Controllers")]
    public BoundController _boundController;
    public CollectableGenerator _collectableGenerator;
    public PlayerController _playerController;
    public AudioController _audioController;
    public DamageIndicator _damageIndicator;
    public UIStateManager _uiStateManager;

    static float score = 0;
    static int highscore;

    static bool playing = true;
    static bool controlling = true;

    public static void StartGame()
    {
        controlling = true;
        playing = true;
        Time.timeScale = 1f;

        score = 0;
        playerController.gameObject.SetActive(true);
        boundController.Progress = 1f;
        playerController.Reset();
        collectableGenerator.Reset();
        boundController.UpdateNoSmoothing();
        audioController.PlayAmbient();

        uiStateManager.GameStart();
    }
    public static void EndGame()
    {
        controlling = false;
        playing = false;

        audioController.PlayExplosion();
        audioController.StopAmbient();
        uiStateManager.GameOver();
        playerController.gameObject.SetActive(false);
    }

    public void InstanceStartGame() { StartGame(); }

	void Start()
    {
        Settings.Load();

        boundController = _boundController;
        collectableGenerator = _collectableGenerator;
        playerController = _playerController;
        audioController = _audioController;
        damageIndicator = _damageIndicator;
        uiStateManager = _uiStateManager;

        pointsPerCrystal = _pointsPerCrystal;
        boundBonusPercentage = _boundBonusPercentage;

        highscore = PlayerPrefs.GetInt("highscore", 0);

        StartGame();
	}
	
	void Update()
    {
        if (playing && controlling)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Score > highscore)
        {
            highscore = Score;
            PlayerPrefs.SetInt("highscore", highscore);
        }

        if (!Playing) return;

        var progress = 1 - boundController.Progress;
        scoreMultiplier = Mathf.Pow(progress, scorePower) * (maxScoreMultiplier - 1) + 1;
        score += scoreMultiplier * baseScorePerSecond * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape)) controlling = !controlling;
    }

    public static void PickupPointCrystal()
    {
        score += pointsPerCrystal;
        audioController.PlayPickup();
    }
    public static void PickupBoundBonus()
    {
        boundController.Progress += boundBonusPercentage;
        audioController.PlayBoundaryBonus();
    }

    public static int Score
    {
        get
        {
            return (int) score;
        }
    }
    public static float Multiplier
    {
        get
        {
            return scoreMultiplier;
        }
    }

    public static bool Controlling
    {
        get
        {
            return controlling && playing;
        }
    }
    public static bool Playing
    {
        get
        {
            return playing;
        }
    }

    public static int Highscore
    {
        get
        {
            return highscore;
        }
    }
}
