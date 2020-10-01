using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    private float moveInput;

    public Transform grounded;
    private bool isGrounded;
    public LayerMask whatIsGround;

    public float checkRadius;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;
    private int extraJumps;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = 2;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(grounded.position, checkRadius, whatIsGround);
        
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }   
        if(isGrounded)
        {
            extraJumps = 2;
        }         
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            anim.SetBool("Ground", false); // Start of jump, set ground to false.
            // Debug.Log("Jump press");
            isJumping = true;
            jumpTimeCounter = jumpTime;            
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            // Debug.Log("Jump hold?");
            if(jumpTimeCounter > 0)
            {                
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }            
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(grounded.position, checkRadius, whatIsGround);        
        anim.SetBool("Ground", isGrounded);
        moveInput = Input.GetAxis("Horizontal");        
        
        if(moveInput * speed != 0)
        {                        
            anim.SetFloat("Speed", Mathf.Abs(moveInput * speed));
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); 
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
        

    }
}
