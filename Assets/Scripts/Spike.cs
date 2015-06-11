using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

    public AudioClip snap;
    public AudioClip recover;
    public GameObject death;

    private bool isDown;

    void Recover()
    {
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = recover;
        GetComponent<AudioSource>().Play();
        GetComponent<Animation>().Play("SpikeRecover");
    }

    void Snap()
    {
        if (isDown) return;

        death.SetActive(true);
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = false;
        if (GetComponent<Animation>().isPlaying)
            GetComponent<Animation>()["SpikeSnap"].normalizedTime = 1f - GetComponent<Animation>()["SpikeRecover"].normalizedTime;
        GetComponent<Animation>().Play("SpikeSnap");
    }

    void PlaySnapAudio()
    {
        isDown = true;
        GetComponent<AudioSource>().PlayOneShot(snap);
        death.SetActive(false);
    }

    void OnRecovered()
    {
        isDown = false;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<Animation>()["SpikeSnap"].normalizedTime = 0f;
    }
}
