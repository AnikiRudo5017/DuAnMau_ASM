using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float jumpspeed = 5f;
    [SerializeField] private float leospeed = 5f;
    public string again;
    private bool leo = false;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    public ScoreData diemLuu;
    public TMP_Text Score;
    [SerializeField] Transform firePoint;
    [SerializeField] private GameObject bullet;
    public float atkSpeed = 1, countDown = 0;
    public AudioClip coinPick;
    private AudioSource audioSource;
    // Start is called before the first frame update
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Score.text = "Score: " + diemLuu.score;
    }
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    private void Update()
    {
        Attack();
        Jump();
        Run();
        Flip();
        if (leo && Input.GetKey(KeyCode.W))
        {
            float leoInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, leoInput * leospeed * Time.deltaTime);

            bool checkLeo = Mathf.Abs(leoInput) > Mathf.Epsilon;
            anim.SetBool("IsLeo", checkLeo);
        }
    }

    void Run()
    {
        Vector2 player = new Vector2(moveInput.x * movespeed, rb.velocity.y);
        rb.velocity = player;

        bool checkRun = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; // True/False
        anim.SetBool("Run", checkRun);
    }

    void Flip()
    {
        bool flip = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (flip)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            
        }
    }
    public void Attack()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            anim.SetBool("Attack", true);
            Instantiate(bullet, firePoint.position, transform.rotation);
            countDown = atkSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Ladder"))
        //{
        //    if (leo && Input.GetKey(KeyCode.W))
        //    {
        //        float leoInput = Input.GetAxisRaw("Vertical");
        //        rb.velocity = new Vector2(rb.velocity.x, leoInput * leospeed * Time.deltaTime);
        //    }
        //}
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("da cham");
            Destroy(this.gameObject);
            SceneManager.LoadScene(again);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            diemLuu.score++;
            Score.text = "Score: " + diemLuu.score;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinPick);
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Ladder"))
    //    {
    //        leo = false;
    //        rb.gravityScale = 1f;
    //    }
    //}

    
}