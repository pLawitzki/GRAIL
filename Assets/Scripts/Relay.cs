using UnityEngine;
using System.Collections;

public class Relay : MonoBehaviour {

    public GameObject[] targets;
    public string[] methods;

    void Trigger()
    {
        for (int i = 0; i < targets.Length; ++i )
        {
            targets[i].SendMessage(methods[i]);
        }
    }
}
