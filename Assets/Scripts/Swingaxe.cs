using UnityEngine;
using System.Collections;

public class Swingaxe : MonoBehaviour {

    public float LeftPos;
    public float RightPos;

    public JointSpring forth;   // positive target pos
    public JointSpring back;    // negative target pos

    private HingeJoint joint;

	void Start ()
    {
        joint = GetComponent<HingeJoint>();

        // init springs
        forth = new JointSpring();
        forth.spring = joint.spring.spring;
        forth.targetPosition = RightPos;
        forth.damper = joint.spring.damper;
        back = new JointSpring();
        back.spring = joint.spring.spring;
        back.targetPosition = LeftPos;
        back.damper = joint.spring.damper;
        joint.spring = forth;
	}
	
	
	void Update ()
    {
        float currentAngle = joint.angle - 120f;

        if (joint.spring.targetPosition > 0f)
        {
            if (currentAngle > joint.spring.targetPosition)
            {
                joint.spring = back;
                gameObject.GetComponent<AudioSource>().PlayDelayed(1.5f);
            }
        }
        else
        {
            if (currentAngle < joint.spring.targetPosition)
            {
                joint.spring = forth;
                gameObject.GetComponent<AudioSource>().PlayDelayed(1.5f);
            }
        }
	}
}
