using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private ParticleSystem particle;
    private GameObject shine;
    private AudioSource audio;

	void Start () {
        shine = transform.FindChild("shine").gameObject;
        particle = transform.FindChild("particle").gameObject.GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (!shine.activeSelf)
        {
            shine.SetActive(true);
            particle.Play();
            audio.Play();

            Persistency.SetCheckpoint(gameObject);
        }
    }

    void SetChecked()
    {
        shine = transform.FindChild("shine").gameObject;
        if (shine != null)
            shine.SetActive(true);
    }
}
