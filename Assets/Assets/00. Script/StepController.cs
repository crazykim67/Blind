using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
    private AudioSource ad;

    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();

    private void Awake()
    {
        ad = GetComponent<AudioSource>();
    }

    public void OnStep()
    {
        OnStepSound();
        StepPoolManager.Instance.GetStep();
    }

    private void OnStepSound()
    {
        int index = Random.Range(0, clips.Count);

        ad.clip = clips[index];
        ad.Play();
    }
}
