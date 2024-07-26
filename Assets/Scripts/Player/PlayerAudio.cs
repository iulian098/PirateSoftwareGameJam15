using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip footstepsClip;

    public void PlayFootSteps() {
        audioSource.pitch = Random.Range(0.9f, 1f);
        audioSource.volume = Random.Range(0.9f, 1f);
        audioSource.PlayOneShot(footstepsClip);
    }
}
