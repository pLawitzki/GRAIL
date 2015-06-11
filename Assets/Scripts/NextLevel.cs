using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

    public string nextLevelName;
	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterMover>() != null)
            Application.LoadLevel(nextLevelName);	
	}
}
