using UnityEngine;
using System.Collections;

public class Floorplate : MonoBehaviour {

    public GameObject DownTarget;
    public GameObject UpTarget;
    public string DownMethod;
    public string UpMethod;
    
    public float downDelay;
    public float upDelay;

    private bool pressed;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            pressed = true;
            Invoke("TriggerDown", downDelay);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            Invoke("TriggerUp", upDelay);
        }
    }

    void TriggerDown()
    {
        if (!pressed) return;

        if (DownTarget != null)
            DownTarget.SendMessage(DownMethod);
        GetComponent<AudioSource>().Play();
        transform.Translate(0f, 0f, -0.15f);
    }

    void TriggerUp()
    {
        if (pressed)
        {
            transform.Translate(0f, 0f, 0.15f);
            if (UpTarget != null)
                UpTarget.SendMessage(UpMethod);
            GetComponent<AudioSource>().Play();
        }
        pressed = false;
    }
}
