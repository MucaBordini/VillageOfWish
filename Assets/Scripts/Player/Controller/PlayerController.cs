using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private AudioSource sounds;
    public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip hurtSound;
    public AudioClip enemySound;
    public AudioClip fallDeath;
    public LayerMask enemyLayer;
    public float velocidade = 1f;
    public float JumpForce = 1f;
    public float damageForce = 1f;
    float movement = 0f;
    private Rigidbody2D rb2d;
    Animator anim;
    public bool floor = false;
    private bool playedFall;
    private int hitBoss = 0;
    public int playerScene;
    public int nextScene;
    private bool menuOn = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "HP")
        {
            sounds.PlayOneShot(healthSound);
            Destroy(collision.gameObject);
            if (PlayerStats.getIstance().getHealthPoints() < 3)
            {
                PlayerStats.getIstance().healthGain();
            } else
            {
                PopUpText.fillPopUp("Vida cheia!");
                StartCoroutine(WaitPopUp());
            }
        } else if (collision.gameObject.tag == "Points")
        {
            sounds.PlayOneShot(coinSound);
            PlayerStats.getIstance().addPoints();
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "EndOfLevel")
        {
            SceneManager.LoadScene(nextScene);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            transform.parent = collision.collider.transform;
            floor = true;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            
            if (PlayerStats.getIstance().getHealthPoints() >= 1)
            {
                sounds.PlayOneShot(hurtSound);
                PlayerStats.getIstance().healthLoss();
                if (collision.gameObject.tag == "Enemy")
                    Destroy(collision.gameObject);
                if(transform.eulerAngles.y == 180)
                {
                    rb2d.AddForce(new Vector2(-damageForce, 0), ForceMode2D.Impulse);
                } else
                {
                    rb2d.AddForce(new Vector2(damageForce, 0), ForceMode2D.Impulse);
                }
                
                
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
        sounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuOn)
            {
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync(19, LoadSceneMode.Additive);
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync(20);
            }
            menuOn = !menuOn;


        }

        if ((PlayerStats.getIstance().getHealthPoints() == 0) || transform.position.y < -8)
        {
            Time.timeScale = 1;
            PlayerStats.getIstance().healthReset();
            if(PlayerStats.getIstance().getPoints() < 100)
            {
                PlayerStats.getIstance().bonusPoints();
            }
            if (transform.position.y < -6.20f)
            {
                if (playedFall == false)
                {
                    sounds.PlayOneShot(fallDeath);
                    playedFall = true;
                }
                StartCoroutine(WaitFall());
            } else
            {
                SceneManager.LoadScene(playerScene);
            }
           
            
        }

        //Detecta cabeça do inimigo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, enemyLayer);

        if (hit && hit.collider.CompareTag("Enemy"))
        {
            sounds.PlayOneShot(enemySound);
            hit.collider.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            hit.collider.gameObject.transform.Rotate(0, 0, 180);
            hit.collider.gameObject.transform.position += new Vector3(15, 80, 0f) * Time.deltaTime;
            rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
        if (hit && hit.collider.CompareTag("Boss"))
        {
            
                sounds.PlayOneShot(enemySound);
                rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                hitBoss++;
                Debug.Log(hitBoss);
                if (hitBoss == 3)
                {
                    hit.collider.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    hit.collider.gameObject.transform.Rotate(0, 0, 180);
                    hit.collider.gameObject.transform.position += new Vector3(15, 80, 0f) * Time.deltaTime;
                    hitBoss = 0;
                    StartCoroutine(WaitEndOfLevel());
                }
             
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

    IEnumerator WaitPopUp()
    {
        yield return new WaitForSeconds(3f);
        PopUpText.fillPopUp("");

    }

    IEnumerator WaitFall()
    {
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(playerScene);
    }
    IEnumerator WaitEndOfLevel()
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }


}
