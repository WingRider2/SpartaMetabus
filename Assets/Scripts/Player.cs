using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody2D = null;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCoolDoun = 0f;

    bool isFlap = false;

    public bool godMode = false;

    FlappyGameManager flappyGameManager;

    // Start is called before the first frame update
    void Start()
    {
        flappyGameManager = FlappyGameManager.Instance;
        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (animator == null) Debug.Log("Not found  Animator");
        if (_rigidbody2D == null) Debug.Log("Not found  Rigidbody2D");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCoolDoun <= 0f)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    flappyGameManager.RestartGame();
                }
            }
            else
            {
                deathCoolDoun -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody2D.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody2D.velocity = velocity;

        float angle = Mathf.Clamp(_rigidbody2D.velocity.y * 10f, -90, 90);
        //float lerpAngle = Mathf.Lerp(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;
        if (isDead) return;

        isDead = true;
        deathCoolDoun = 1f;

        animator.SetInteger("IsDie", 1);
        flappyGameManager.GameOver();
    }
}

