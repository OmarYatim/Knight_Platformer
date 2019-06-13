using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    Transform Heart1,Heart2,Heart3;
    GameObject Player;
    public GameObject UI;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            gameObject.GetComponent<Animator>().SetBool("Explode",true);
    }
    //Hit distance needs to be changed, y coordinates need to be taken into acccount
    void Death()
    {
        distance = Mathf.Abs(transform.position.x-Player.transform.position.x);
        if(distance<2.4f){Hit();}
    }
    void Hit()
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
            UI.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
        }

    }
    void Destroy(){
        Destroy(gameObject);
    }
}
