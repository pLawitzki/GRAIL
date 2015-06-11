using UnityEngine;
using System.Collections;

public class SkelettonBeacon : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Skeletton s = other.gameObject.GetComponent<Skeletton>();
        if (s != null)
        {
            s.TurnAround();
        }
    }
}
