using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public AudioClip _audioClip;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
         {
            
            Destroy(gameObject); 
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
         }

    }
}
