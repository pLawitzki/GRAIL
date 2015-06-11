using UnityEngine;
using System.Collections;

public class SkelettonSight : MonoBehaviour {

    private Skeletton bob;
    private Vector3 lastPlayerPos;

    void Start()
    {
        bob = transform.parent.gameObject.GetComponent<Skeletton>();
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterMover player = other.gameObject.GetComponent<CharacterMover>();
        if (player != null)
        {
            bob.BeginAttack();
            lastPlayerPos = player.transform.position;
        }
    }

    void OnTriggerStay(Collider other)
    {
        CharacterMover player = other.gameObject.GetComponent<CharacterMover>();
        if (player != null)
        {
            if (player.transform.position != lastPlayerPos)
            {
                lastPlayerPos = player.transform.position;
                bob.PlayerMovement();
            }
        }
    }
}
