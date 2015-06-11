using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

    public GameObject Death;
    public float period;

    void Trigger()
    {
        Death.SetActive(true);
        GetComponent<Animation>().Play();
        GetComponent<AudioSource>().Play();
    }

    void Finish()
    {
        Death.SetActive(false);
        Invoke("Trigger", period);
    }
}
