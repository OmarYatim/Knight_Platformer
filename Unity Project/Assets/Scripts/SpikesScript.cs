using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField]
    Transform Heart1,Heart2,Heart3;
    [SerializeField]
    GameObject Player,UI;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().lives=0;
            Heart3.localScale = Vector3.zero;
            Heart2.localScale = Vector3.zero;
            Heart1.localScale = Vector3.zero;
            UI.SetActive(true);
            Player.GetComponent<Animator>().SetInteger("animate",6);
        }
    }

}
