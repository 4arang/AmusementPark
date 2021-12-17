using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHitEffect : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;

    private void Start()
    {

            if (hitSound != null && GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().PlayOneShot(hitSound);
                Debug.Log("hitSoundPlay");
            }

        Destroy(gameObject, 1.5f);
    }
}
