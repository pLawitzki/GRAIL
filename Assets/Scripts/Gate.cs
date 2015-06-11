using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

    public AudioClip open;
    public AudioClip close;

    public bool startOpen;

    void Awake()
    {
        if (startOpen)
        {
            GetComponent<Animation>().Play("gateopen");
            GetComponent<Animation>()["gateopen"].normalizedTime = 1f;
        }
    }

    void Open()
    {
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = open;
        GetComponent<AudioSource>().Play();
        GetComponent<Animation>().Play("gateopen");
    }

    void Close()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = false;
        if (GetComponent<Animation>().isPlaying)
            GetComponent<Animation>()["gateclose"].normalizedTime = 1f - GetComponent<Animation>()["gateopen"].normalizedTime;
        GetComponent<Animation>().Play("gateclose");
    }

    void PlayCloseAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(close);
    }

    void OnOpened()
    {
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<Animation>()["gateclose"].normalizedTime = 0f;
    }
}
