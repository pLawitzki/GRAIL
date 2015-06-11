using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public enum PlatformMode
    {
        pingpong,
        reset
    };

    public float speed = 1f;
    public PlatformMode mode;
    public Vector3 endPosition;

    private Vector3 startPosition;
    private Vector3 direction;

	void Start ()
    {
        startPosition = transform.position;
        direction = (endPosition - startPosition).normalized;
    }
	

	void Update () 
    {
        if (Vector3.Distance(transform.position, endPosition) < direction.magnitude * Time.deltaTime * speed)
        {
            switch (mode)
            {
                case PlatformMode.pingpong:
                    Vector3 temp = startPosition;
                    startPosition = endPosition;
                    endPosition = temp;
                    direction = -direction;
                    break;

                case PlatformMode.reset:
                    transform.position = startPosition;
                    break;
            }
        }
        else
            transform.position += direction * Time.deltaTime * speed;
	}

    public Vector3 Direction
    {
        get { return direction; }
    }
}
