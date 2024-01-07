using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Cooldown cooldown;
    public string dashCooldownKey = "dash";

    private PlayerStats stats;
    private Rigidbody2D rb;

    private Vector2 playerInput;

    private float speed = 3000f;
    public float maxVelocity = 40f;

    public float dashCooldown = 3f;
    private float baseSpeed;
    private float dashSpeed = 100000f;
    public float dashTime = 0.1f;
    void Start()
    {
        cooldown = Cooldown.instance;
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();

        baseSpeed = stats.Speed;
        dashSpeed = stats.baseDashSpeed;
        speed = baseSpeed;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (baseSpeed != stats.Speed)
        {
            baseSpeed = stats.Speed;
            speed = baseSpeed;
        }

        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && playerInput != Vector2.zero)
        {
            if (cooldown.IsInCooldown(dashCooldownKey))
            {
                GameObject.Find("DashUI").GetComponent<Animation>().Play("In-Cooldown");
            }
            else
            {
                StartCoroutine(Dash());
                GameObject.Find("DashUI").GetComponent<Animation>().Play("Activated");
                cooldown.StartCooldown(dashCooldownKey, dashCooldown);
            }
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
        // rb.velocity = new Vector2(horizontal, vertical).normalized * speed * Time.deltaTime;
        rb.AddForce(playerInput.normalized * speed * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        GetComponent<TrailRenderer>().enabled = true;

        yield return new WaitForSeconds(dashTime);

        speed = baseSpeed;

        yield return new WaitForSeconds(2 * dashTime);
        GetComponent<TrailRenderer>().enabled = false;
    }
}
