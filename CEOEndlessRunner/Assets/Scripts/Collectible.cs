using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Collectible : MonoBehaviour
{
    [SerializeField] ParticleSystem consumptionVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(10);
            ParticleSystem vfx = Instantiate(consumptionVFX, transform.position, Quaternion.identity, transform);
            vfx.Play();
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,3);
        }
    }
}
