using UnityEngine;
using System.Collections;

public class JumpDetector : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        CharacterMover mover = other.gameObject.GetComponent<CharacterMover>();
        if (mover != null)
        {
            mover.Jump();
        }
    }
}
