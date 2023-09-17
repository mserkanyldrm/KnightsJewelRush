using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;
    static GameManager s_Instance;

    public delegate void GameManagerEvent();
    public GameManagerEvent OnPlayerDeath;

    private bool slowmo = false;
    private float slowDownFactor = 0.1f;
    private float slowmoTime = 3f;

    private float score;

    [SerializeField] GameObject deathScreen;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreTextDeathScreen;
    [SerializeField] ProgressBar slowmoBar;

    public enum PlayerState { PLAYING,DEAD};
    public PlayerState playerState;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.PLAYING;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.PLAYING)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                ToggleSlowMotion();
            }


            if (slowmo)
            {
                slowmoTime -= Time.deltaTime / slowDownFactor;

                if (slowmoTime <= 0)
                {
                    ToggleSlowMotion();
                }
            }
            else
            {
                slowmoTime += Time.deltaTime;
            }

            slowmoTime = Mathf.Clamp(slowmoTime, 0, 3);
            if (slowmoBar != null)
            {
                slowmoBar.current = (int)(Mathf.InverseLerp(0, 3, slowmoTime) * 100);
            }
            
        }
        
    }

    public void AddScore(float points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        }
        
    }

    public void EndGame()
    {
        playerState = PlayerState.DEAD;
        ActivateDeathScene();
        OnPlayerDeath?.Invoke();
    }

    private void ActivateDeathScene()
    {
        deathScreen.SetActive(true);
        scoreTextDeathScreen.text = "Score: " + Mathf.RoundToInt(score).ToString();
    }
    public void ToggleSlowMotion()
    {
        if (!slowmo)
        {
            Time.timeScale = slowDownFactor;
            PlayerController.Instance.speed = PlayerController.Instance.speed / slowDownFactor;
            slowmo = true;
        }
        else
        {
            Time.timeScale = 1f;
            PlayerController.Instance.speed = PlayerController.Instance.speed * slowDownFactor;
            slowmo = false;
        }

        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
