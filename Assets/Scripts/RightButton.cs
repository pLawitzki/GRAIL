using UnityEngine;
using System.Collections;

public class RightButton : MonoBehaviour {

    public ParticleSystem particle;
    public string firstLevel;
    private bool pressed = false;

	void Update () {
        if (Input.GetAxis("Horizontal") > 0f && !pressed || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            pressed = true;
            particle.Play();
            GetComponent<AudioSource>().Play();
            Invoke("Begin", 1.6f);
        }
	}

    void Begin()
    {
        Application.LoadLevel(firstLevel);
    }
}
