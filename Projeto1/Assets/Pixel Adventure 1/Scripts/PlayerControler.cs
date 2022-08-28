using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float speed, jumpForce, x, y;
    [SerializeField] bool isJump, isDJump;

    [SerializeField] Text text;
    Animator animator;
    Rigidbody2D rig;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Animations();
        Move();
        Jump();
        
    }

    public void Click()
    {
        Time.timeScale = 0;
    }
    void Move()
    {
        x = Input.GetAxisRaw("Horizontal") * speed;
        y = rig.velocity.y;

        rig.velocity = new Vector2 (x, y);

        if (rig.velocity.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            
        }
        else if (rig.velocity.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isDJump = true;
            }
            else if (isDJump)
            {
                rig.AddForce(new Vector2(0, jumpForce * 1.5f), ForceMode2D.Impulse);
                isDJump = false;
            }
        }
    }
    void Animations()
    {
        if (rig.velocity.x != 0 && !isJump) // run
        {
            animator.SetFloat("Anim", 0.25f);
        }
        else if (isDJump && rig.velocity.y > 0) //jump
        {
            animator.SetFloat("Anim", 0.5f);
        }
        else if (!isDJump && rig.velocity.y > 0)
        {
            animator.SetFloat("Anim", 0.75f);
        }
        else if (rig.velocity.y < 0)
        {
            animator.SetFloat("Anim", 1f);
        }
        else if (rig.velocity.x == 0 && rig.velocity.y == 0)
        {
            animator.SetFloat("Anim", 0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
          
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = true;
        }
    }
}
