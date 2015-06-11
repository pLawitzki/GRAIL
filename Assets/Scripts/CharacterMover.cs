using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour {

    // movement parameters
    public float gravity = 10f;
    public float speed = 1f;
    public float jumpImpulse = 1f;
    public float maxFallSpeed = 5f;

    // audio assets
    public AudioClip step;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip attack;
    public AudioClip death;

    public GameObject Godrays;

    private CharacterController controller;
    private Vector3 groundNormal;
    private float downVelocity;
    private float jumpVelocity;
    private MovingPlatform platform;
    
    public EnemyDeath enemyDeath;

    private float previousHAxis;
    private float previousJumpVelocity;

    private bool attacking;
    private bool victory;

	void Start ()
    {
        controller = GetComponent<CharacterController>();
        groundNormal = Vector3.up;
        previousJumpVelocity = 0f;
        previousHAxis = 0f;
        attacking = false;
        victory = false;
    }
	

	void Update ()
    {
        if (victory) return;
        /*
        // TEST
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        // TEST

        // TEST
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Test");
            Attack();
        }
        //TEST
        */
        
        if (attacking)
        {
            previousHAxis = 0f;
            if (!GetComponent<Animation>().isPlaying)
            {
                attacking = false;
                enemyDeath.gameObject.SetActive(false);
            }
        }

        float run = Right;

        if (groundNormal.y <= 0.8f)
        {
            downVelocity += gravity * Time.deltaTime;
            jumpVelocity -= downVelocity * Time.deltaTime;
        }

        jumpVelocity = (jumpVelocity < -maxFallSpeed) ? -maxFallSpeed : jumpVelocity;

        Vector3 direction = new Vector3( (attacking) ? 0f : run, jumpVelocity, 0f) * Time.deltaTime * speed;

        if (platform != null)
        {
            direction += platform.Direction * platform.speed * Time.deltaTime;
        }

        controller.Move(direction);


        UpdateRunAnimation();
        UpdateMidAirAnimation();

        previousHAxis = Right;
        previousJumpVelocity = jumpVelocity;

        StepAudio();
	}

    float Right {
        get
        {
            float right = Input.GetAxis("Horizontal");
            if (right <= 0f)
            {
                right = Input.GetMouseButton(0) ? 1f : 0f;
            }
            if (right <= 0f)
            {
                right = (float)Input.touchCount;
            }
            right = (right < 0f) ? 0f : right;
            return right;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        groundNormal = hit.normal;

        // hit ceiling
        if (groundNormal.y < 0f && jumpVelocity > 0f)
        {
            jumpVelocity = 0f;
        }
        // hit floor
        else if (groundNormal.y >= 0.7f && Mathf.Abs(groundNormal.x) <= 0.7f)
        {
            downVelocity = 0f;
            jumpVelocity = 0f;
            if (previousJumpVelocity < 0f)
            {
                previousHAxis = 0f;
                GetComponent<AudioSource>().PlayOneShot(land);
            }
        }

        platform = hit.gameObject.GetComponent<MovingPlatform>();
    }

    public void UpdateRunAnimation()
    {
        if (attacking) return;

        if (previousHAxis > 0f && Right > 0f)
            return; // input has not changed

        if (jumpVelocity == 0f)
        {
            if (Right > 0f)
            {
                GetComponent<Animation>().CrossFade("run", 0.1f);
            }
            else
            {
                GetComponent<Animation>().CrossFade("idle", 0.1f);
                if (GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().clip == step)
                {
                    GetComponent<AudioSource>().Stop();
                }
            }
        }
    }

    public void UpdateMidAirAnimation()
    {
        if (attacking) return;

        if (previousJumpVelocity > 0f && jumpVelocity < 0f)
        {
            GetComponent<Animation>().CrossFade("fall", 0.1f);
        }
    }

    public void Jump()
    {
        if (attacking) return;

        if (groundNormal.y != 0f)
        {
            // stop playing steps
            if (GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().clip == step)
            {
                GetComponent<AudioSource>().Stop();
            }

            groundNormal = Vector3.zero;
            downVelocity = 0f;
            jumpVelocity = jumpImpulse;

            GetComponent<Animation>().CrossFade("jump", 0.1f);
            GetComponent<AudioSource>().PlayOneShot(jump);
        }
    }

    void StepAudio()
    {
        if (GetComponent<Animation>().IsPlaying("run") && !GetComponent<AudioSource>().isPlaying && jumpVelocity == 0f && !attacking)
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = step;
            GetComponent<AudioSource>().Play();
        }
    }

    public void Kill()
    {
        Invoke("OnDeath", 2f);
        GameObject particleGO = Instantiate(Resources.Load("Particles"), transform.position, Quaternion.identity) as GameObject;
        ParticleSystem particleSys = particleGO.GetComponent<ParticleSystem>();
        if (particleSys != null)
        {
            particleSys.Play();
        }
        AudioSource particleAudio = particleGO.GetComponent<AudioSource>();
        if (particleAudio != null)
        {
            particleAudio.PlayOneShot(death);
        }

        // reset all skelettons
        GameObject[] skelettonArr = GameObject.FindGameObjectsWithTag("skeletton");
        foreach (GameObject go in skelettonArr)
        {
            Skeletton s = go.GetComponent<Skeletton>();
            if (s != null)
            {
                s.ResetSkeletton();
            }
        }

        gameObject.SetActive(false);
    }

    public void Attack()
    {
        GetComponent<AudioSource>().Stop();
        attacking = true;
        GetComponent<Animation>().CrossFade("attack", 0.1f);
        GetComponent<AudioSource>().PlayOneShot(attack);
        enemyDeath.gameObject.SetActive(true);
    }

    void OnDeath()
    {
        Persistency.OnDeath();
        Destroy(gameObject);
    }

    void Victory()
    {
        GetComponent<AudioSource>().Stop();
        victory = true;
        Godrays.SetActive(true);
        GetComponent<Animation>().CrossFade("ending", 0.1f);

        GameObject textContainer = GameObject.Find("EndingText");
        textContainer.SetActive(true);
        textContainer.GetComponent<Animation>().Play();

        GameObject deathFont = GameObject.Find("DeathText");
        GameObject deathFontShadow = GameObject.Find("DeathTextShadow");

        GUIText t = deathFont.GetComponent<GUIText>();
        GUIText s = deathFontShadow.GetComponent<GUIText>();

        t.text = "You died " + Persistency.deaths.ToString() + " times in the process.";
        s.text = t.text;

        if (Persistency.deaths < 1)
        {
            GameObject gj = GameObject.Find("GameJoltAPI");
            if (gj != null)
            {
                gj.SendMessage("UnlockAchivement", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
