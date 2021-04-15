using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidade = 1f;
    public float JumpForce = 1f;
    float movement = 0f;
    public Rigidbody2D rb2d;
    Animator anim;
    public bool floor = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            transform.parent = collision.collider.transform;
            floor = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
        floor = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        movement = Input.GetAxisRaw("horizontal") * velocidade * Time.deltaTime;
        anim.SetFloat("Speed", Mathf.Abs(movement));
        transform.position += new Vector3(movement, 0, 0);

        if(!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if(Input.GetButtonDown("Jump") && Mathf.Abs(rb2d.velocity.y) < 0.001f)
        {
            rb2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }

}
