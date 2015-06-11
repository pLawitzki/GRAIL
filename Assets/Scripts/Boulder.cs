using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

    public float initialVelocity;

    void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity = initialVelocity;
    }

}
