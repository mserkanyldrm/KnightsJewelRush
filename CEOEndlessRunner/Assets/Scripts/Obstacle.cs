using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip hitSFX;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.playerState == GameManager.PlayerState.PLAYING)
            {
                audioSource.PlayOneShot(hitSFX);
                GameManager.Instance.EndGame();
            }
            
        }
    }
}
