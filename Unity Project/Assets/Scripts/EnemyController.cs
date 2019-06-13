using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject UI,Lose;
    float xMin;
    float xMax;
    bool isFacingRight;
    bool isRunning = false;
    bool Moving = true;
    float xDistance,yDistance;
    GameObject Player;

    [SerializeField]
    GameObject HealthBar;
    [SerializeField]
    Transform Heart1,Heart2,Heart3;
    float HealthBarScale;

    
    void Start()
    {
        HealthBarScale = HealthBar.transform.localScale.x;
        Player = GameObject.FindWithTag("Player");
        xMax = transform.position.x + 3;
        xMin = transform.position.x - 3;
    }

    bool die;
    void Update()
    {
        die = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("die");
        if(!die && Moving && !isRunning && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("appear"))
            Mouvement();
    }
    public IEnumerator Attack()
	{
        isRunning = true;
        GetComponent<Animator>().SetInteger("Anim", 1);
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetInteger("Anim", 2);
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        GetComponent<Animator>().SetInteger("Anim", 1);
        yield return new WaitForSeconds(0.5f);
        isRunning = false;
	}

    void Death()
    {
        xDistance = gameObject.transform.position.x-Player.transform.position.x;
        yDistance = gameObject.transform.position.y-Player.transform.position.y;
        if(Mathf.Abs(xDistance) < 2.5f && yDistance < 2)
        {
            Player.GetComponent<PlayerScript>().lives--;
            if(Player.GetComponent<PlayerScript>().lives == 2)
            {
                Heart3.localScale = Vector3.zero;
            }
            if(Player.GetComponent<PlayerScript>().lives == 1)
            {
                Heart2.localScale = Vector3.zero;
            }
            if(Player.GetComponent<PlayerScript>().lives == 0)
            {
                Heart1.localScale = Vector3.zero;
                Player.GetComponent<Animator>().SetInteger("animate",6);
                Lose.SetActive(true);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void Disappear()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Moving = false;
        if(other.gameObject.tag == "Player" && gameObject.tag == "Boss")
        {
            HealthBar.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Moving = true;
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            /* if(gameObject.transform.position.x < other.gameObject.transform.position.x)
            {
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;
                isFacingRight = true;
            }
            if(gameObject.transform.position.x > other.gameObject.transform.position.x)
            {
                //gameObject.GetComponent<SpriteRenderer>().flipX = false;
                isFacingRight = false;
            }*/
            if(!isRunning){StartCoroutine(Attack());}
        }
    }
    void Mouvement()
	{
        if (transform.position.x == xMax)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
			isFacingRight = false;
		}
		if (transform.position.x == xMin)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
			isFacingRight = true;
		}
		if (!isFacingRight)
		{
            GetComponent<Animator>().SetInteger("Anim", 0);
			gameObject.transform.Translate(-Time.deltaTime, 0, 0);
			Vector3 clampedPosition = transform.position;
			clampedPosition.x = Mathf.Clamp(transform.position.x, xMin, xMax);
			transform.position = clampedPosition;
		}
		if (isFacingRight)
		{
			GetComponent<Animator>().SetInteger("Anim", 0);
            gameObject.transform.Translate(Time.deltaTime, 0, 0);
			Vector3 clampedPosition = transform.position;
			clampedPosition.x = Mathf.Clamp(transform.position.x, xMin, xMax);
			transform.position = clampedPosition;
		}
	}

    public void EnemyDeath()
    {
        if(gameObject.CompareTag("NormalEnemy"))
        {
            GetComponent<Animator>().SetInteger("Anim",3);
            Player.GetComponent<PlayerScript>().score += 100;
        }
        if(gameObject.CompareTag("MediumEnemy"))
        {
            GetComponent<Animator>().SetInteger("Anim",3);
            Player.GetComponent<PlayerScript>().score += 250;
        }
        if(gameObject.CompareTag("Boss"))
        {
            if(HealthBar.transform.localScale.x > 0)
            {
                HealthBar.transform.localScale-=new Vector3(HealthBarScale/3,1,1);
            }
            if(HealthBar.transform.localScale.x == 0)
            {
                GetComponent<Animator>().SetInteger("Anim",3);
                Player.GetComponent<PlayerScript>().score+=500;
                UI.SetActive(true);
            }
        }
    }
}
