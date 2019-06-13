using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // nahi el animation mte3 dharb w tan9iz
    //nahi el jary?
    // baddl el sprites mte3 el enemies
    // zid ui mel lowel w mte3 rebh w 5sara
    //
    #region Public Params
    public int score;
    public int lives = 3;
    public LayerMask groundLayer;
    #endregion
    #region Private Params
    int speed = 4;
    bool att,die,Up,UpAtt;
    bool IsFacingRight = true;
    GameObject Enemy;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        att = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");
        die = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death");
        Up = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump");
        UpAtt = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("JumpAttack");
        if(!die)
        {
            if(!att)
            {
                Movement();
                Jumping();
            }
            Attack();
        }
        if(!IsGrounded() && !Input.GetButtonDown("Fire1"))
            GetComponent<Animator>().SetInteger("animate",4);
    }

    
    bool IsGrounded() 
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundLayer.value)) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Tiles")
        {
            if(!Input.anyKey)
            {
                GetComponent<Animator>().SetInteger("animate",0);
            }
        }
    }

    void Movement()
    {
        float Move = Input.GetAxis("Horizontal");
        if(Move < 0)
        {
            GetComponent<Animator>().SetInteger("animate",1);
            transform.Translate(-speed*Time.deltaTime,0,0);   
        }
        else if(Move > 0)
        {
            GetComponent<Animator>().SetInteger("animate",1);
            transform.Translate(speed*Time.deltaTime,0,0);
        }
        else
        {
            GetComponent<Animator>().SetInteger("animate",0);
        }
        /* if(Input.GetButton("Fire3"))
        {
            speed = 4;
            if(Move != 0)
                GetComponent<Animator>().SetInteger("animate",2);
            else
            {
                GetComponent<Animator>().SetInteger("animate",0);
            }
        }
        if(Input.GetButtonUp("Fire3"))
        {
            speed = 2;
        }*/
        if(Move > 0 && !IsFacingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            IsFacingRight = true;
        }
        else if(Move<0 && IsFacingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            IsFacingRight = false;
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(IsGrounded())
                GetComponent<Animator>().SetInteger("animate",3);
            if(!IsGrounded())
            {
                GetComponent<Animator>().SetInteger("animate",5);
            }
        }
    }

    void Jumping()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded() && !Up && !UpAtt)
        {
            GetComponent<Rigidbody2D>().velocity  =  Vector2.up*7;
        }
    }

    bool isAttacking;
    void Attacking()
    {
        isAttacking =true;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(isAttacking)
        {
            if(other.gameObject.layer == 9)
            {
                other.GetComponent<EnemyController>().EnemyDeath();
            }
        }
        isAttacking = false;
    }
}
