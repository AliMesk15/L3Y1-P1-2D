using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerTxt;
    public float timer;

    [Header("Health")]
    public Slider healthSlider;
    public int MaxHealth;
    public int CurrentHealth;

    [Header("Shooting")]
    public Transform shootingPoint;
    public GameObject Bullet;
    bool IsFacingRight;

    [Header("Main")]
    public float moveSpeed;
    public float jumpForce;
    float inputs;
    public Rigidbody2D rb;
    public float groundDistance;
    public LayerMask layerMask;

    RaycastHit2D hit;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        startPos = transform.position;
        IsFacingRight = true;
        healthSlider.maxValue = MaxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerTxt.text = timer.ToString("F2");
        
        Movement();
        Health();
        Shoot();
        MovementDirection();
    }

    void Movement()
    {
        inputs = Input.GetAxisRaw("Horizontal");
        rb.velocity = new UnityEngine.Vector2(inputs * moveSpeed, rb.velocity.y);

        hit = Physics2D.Raycast(transform.position, -transform.up, groundDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.yellow);

        if (hit.collider)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void Health()
    {
        healthSlider.value = CurrentHealth;
      
        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Shoot()
{
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
        Instantiate(Bullet, shootingPoint.position, shootingPoint.rotation);
    }
}
void MovementDirection()
{
    if (IsFacingRight && inputs <-.1f)
    {
        Flip();
    }
    else if (!IsFacingRight && inputs > .1f)
    {
        Flip();
    }
}  

    void Flip ()
    {
    IsFacingRight = !IsFacingRight;
    transform.Rotate(0f, 180f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = startPos;
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            CurrentHealth--;
            Destroy(other.gameObject);
        }
    }
}

