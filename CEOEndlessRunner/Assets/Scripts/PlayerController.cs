using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance => s_Instance;
    static PlayerController s_Instance;

    float direction;

    public float speed;

    public Animator playerAnimator;

    AudioSource audioSource;
    [SerializeField] AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        audioSource = GetComponent<AudioSource>(); 
        playerAnimator = GetComponentInChildren<Animator>();

        GameManager.Instance.OnPlayerDeath += PlayDeathAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerState == GameManager.PlayerState.PLAYING)
        {
            direction = Input.GetAxis("Horizontal");

            if (transform.position.x >= 3)
            {
                direction = Mathf.Clamp(direction, -1, 0);
            }
            else if (transform.position.x <= -3)
            {
                direction = Mathf.Clamp(direction, 0, 1);
            }

            transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        }

    }

    void PlayDeathAnimation()
    {
        audioSource.PlayOneShot(deathSound);
        playerAnimator.SetBool("Dead", true);
    }

    private void OnDisable()
    {
        
        GameManager.Instance.OnPlayerDeath -= PlayDeathAnimation;
    }
}
