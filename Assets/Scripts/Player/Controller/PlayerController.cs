using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float velocidade = 1f;
    public float JumpForce = 1f;
    float movement = 0f;
    public Rigidbody2D rb2d;
    Animator anim;
    public bool floor = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "HP")
        {
            if (PlayerStats.getIstance().getHealthPoints() < 3)
            {
                //Debug.Log("CHAMOU!");
                PlayerStats.getIstance().healthGain();
                Destroy(collision.gameObject);
            }
                //Debug.Log("Vida esta cheia!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            transform.parent = collision.collider.transform;
            floor = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            
            if (PlayerStats.getIstance().getHealthPoints() >= 1)
            {
                PlayerStats.getIstance().healthLoss();
                Destroy(collision.gameObject);
            }
            //else
            //    Debug.Log("Voce morreu!");
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

        //Detecta cabeça do inimigo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, enemyLayer);

        if (hit && hit.collider.CompareTag("Enemy"))
        {
                hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                hit.collider.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                hit.collider.gameObject.transform.Rotate(0, 0, 180);
           
            hit.collider.gameObject.transform.position += new Vector3(15, 80, 0f) * Time.deltaTime;
        }

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
