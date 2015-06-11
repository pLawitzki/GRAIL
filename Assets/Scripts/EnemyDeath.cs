using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Skeletton s = other.gameObject.GetComponent<Skeletton>();
        if (s != null)
        {
            s.Kill();
        }
    }
}
