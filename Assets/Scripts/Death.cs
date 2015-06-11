using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        CharacterMover player = other.gameObject.GetComponent<CharacterMover>();
        if (player != null)
        {
            player.Kill();
        }
    }
}
