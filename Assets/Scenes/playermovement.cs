using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class playermovement : MonoBehaviour
{
    public int speed;
    public int score;
    public int health = 100;
    public int jumpforce;
    public bool isJumping;
    private Rigidbody2D rigidbody;
    public Text scoreText;
    public Text healthTxt;
    public AudioSource coinSound;
    public AudioSource jumpSound;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        rigidbody = GetComponent<Rigidbody2D>();
        scoreText.text = "Score  " + score.ToString();
        healthTxt.text = "Health " + health.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            jumpSound.Play();  
            isJumping = true;
            jump();

        }

    }

    void OnCollisionEnter2D(Collision2D colloider)
    {
        if (colloider.gameObject.tag == "coin")
        {
            score++;
            coinSound.Play();  
            scoreText.text = "Score " + score.ToString();
            Destroy(colloider.gameObject);
        }
        else if (colloider.gameObject.tag == "spike")
        {
            health -= 25;
            healthTxt.text = "Health " + health.ToString();
            if(health <=0)
            {
                Destroy(gameObject);

            }
        }

        if (colloider.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    void jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpforce);
    }
}
