using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Cooldown cooldown;
    public string dashCooldownKey = "dash";

    private Rigidbody2D rb;
    public float speed = 3000f;
    public float dashMultiplyer = 500000f;
    public float dashCooldown = 3f;
    private Vector2 playerInput;


    private float baseSpeed;
    public float dashSpeed = 100000f;
    public float dashTime = 0.1f;
    void Start()
    {
        cooldown = Cooldown.instance;
        rb = GetComponent<Rigidbody2D>();

        baseSpeed = speed;
    }

    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && playerInput != Vector2.zero)
        {
            if (cooldown.IsInCooldown(dashCooldownKey))
            {
                GameObject.Find("DashUI").GetComponent<Animation>().Play("Dash-in-cooldown");
            }
            else
            {
                rb.AddForce(playerInput.normalized * dashMultiplyer * Time.deltaTime);
                GameObject.Find("DashUI").GetComponent<Animation>().Play("Dash");
                cooldown.StartCooldown(dashCooldownKey, dashCooldown);
            }
        }

        if (Input.GetKeyDown(KeyCode.V) && playerInput != Vector2.zero)
        {
            if (cooldown.IsInCooldown(dashCooldownKey))
            {
                GameObject.Find("DashUI").GetComponent<Animation>().Play("Dash-in-cooldown");
            }
            else
            {
                StartCoroutine(Dash());
                GameObject.Find("DashUI").GetComponent<Animation>().Play("Dash");
                cooldown.StartCooldown(dashCooldownKey, dashCooldown);
            }
        }
    }

    private void FixedUpdate()
    {

        // rb.velocity = new Vector2(horizontal, vertical).normalized * speed * Time.deltaTime;
        rb.AddForce(playerInput.normalized * speed * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        speed = baseSpeed;
    }
}
