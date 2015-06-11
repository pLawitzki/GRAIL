using UnityEngine;
using System.Collections;

public class Grail : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        CharacterMover player = other.gameObject.GetComponent<CharacterMover>();
        if (player != null)
        {
            player.SendMessage("Victory");
        }
    }
}
