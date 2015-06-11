using UnityEngine;
using System.Collections;

public class Skeletton : MonoBehaviour {

    public GameObject death;
    public bool lookingLeft = true;
    public float speed = 1f;

    private Animation anim;
    private CharacterController controller;
    private bool playerMoved;
    private bool resetFlag;

    private enum EnemyState
    {
        roaming, preAttack, attack, postAttack, wait
    };

    private EnemyState state;

	void Start ()
    {
        anim = GetComponent<Animation>();
        state = EnemyState.roaming;
        controller = GetComponent<CharacterController>();
        playerMoved = false;
        resetFlag = false;

        if (!lookingLeft)
            transform.Rotate(0f, 180f, 0f);
	}

	
    void Update ()
    {
        switch(state)
        {
            case EnemyState.roaming:
                anim.CrossFade("walk", 0.1f);
                if (lookingLeft)
                    controller.Move(Vector3.left * speed * Time.deltaTime);
                else
                    controller.Move(Vector3.right * speed * Time.deltaTime);
                break;

            case EnemyState.preAttack:
                if (!anim.isPlaying)
                    Strike();
                break;

            case EnemyState.attack:
                if (!anim.isPlaying)
                {
                    state = EnemyState.postAttack;
                    anim.CrossFade("post_attack", 0.1f);
                    death.SetActive(false);
                }
                break;

            case EnemyState.postAttack:
                if (!anim.isPlaying)
                {
                    if (resetFlag)
                    {
                        resetFlag = false;
                        state = EnemyState.roaming;
                    }
                    else
                        state = EnemyState.wait;
                }
                break;

            case EnemyState.wait:
                anim.CrossFade("idle", 0.1f);   // wait until player moves
                if (playerMoved)
                {
                    BeginAttack();
                    playerMoved = false;
                }
                if (resetFlag)
                {
                    resetFlag = false;
                    state = EnemyState.roaming;
                }
                break;
        }
	}

    public void BeginAttack()
    {
        // don't attack while recovering
        if (state == EnemyState.postAttack) return;

        state = EnemyState.preAttack;
        anim.CrossFade("pre_attack", 0.1f);
    }

    void Strike()
    {
        state = EnemyState.attack;
        anim.CrossFade("attack", 0.1f);
        death.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void TurnAround()
    {
        lookingLeft = !lookingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    public void PlayerMovement()
    {
        playerMoved = true;
    }

    public void ResetSkeletton()
    {
        resetFlag = true;
    }

    public void Kill()
    {
        GameObject particleGO = Instantiate(Resources.Load("BoneParticles"), transform.position, Quaternion.identity) as GameObject;
        ParticleSystem particleSys = particleGO.GetComponent<ParticleSystem>();
        if (particleSys != null)
        {
            particleSys.Play();
        }
        particleGO.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
