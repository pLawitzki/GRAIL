using UnityEngine;
using System.Collections;

public class KnightSight : MonoBehaviour {

    private CharacterMover player;

    void Start()
    {
        player = transform.parent.gameObject.GetComponent<CharacterMover>();
    }

    void OnTriggerEnter(Collider other)
    {
        Skeletton s = other.gameObject.GetComponent<Skeletton>();
        if (s != null)
        {
            player.Attack();
        }
    }
}
