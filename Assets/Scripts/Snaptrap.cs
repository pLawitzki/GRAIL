using UnityEngine;
using System.Collections;

public class Snaptrap : MonoBehaviour {

    public GameObject death;

    public void Snap()
    {
        death.SetActive(true);
        GetComponent<Animation>().Play();
    }

    void Recover()
    {
        death.SetActive(false);
    }
}
