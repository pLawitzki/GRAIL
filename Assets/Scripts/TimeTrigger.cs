using UnityEngine;
using System.Collections;

public class TimeTrigger : MonoBehaviour {

    public MonoBehaviour[] targets;
    public string[] methods;
    public float[] offsets;
    public float[] periods;

    void Awake()
    {
        for (int i = 0; i < targets.Length; ++i)
        {
            targets[i].InvokeRepeating(methods[i], offsets[i], periods[i]);
        }
    }
}
