using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPointController : MonoBehaviour
{
    private float move = 0;
    private float speed = 5;
    private string wizard1 = "start";
    private string wizard2 = "start";
    public GameObject deathvfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(move, 0, 0) * Time.deltaTime;
        
        moveController();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject Temporary_DeathVFX = null;
        Debug.Log("Ded");
        ContactPoint contact = collision.GetContact(0);
        Temporary_DeathVFX = Instantiate(deathvfx, contact.point, Quaternion.identity);
        if (collision.collider.tag == "Wizard 1")
        {
            Destroy(collision.gameObject);
            transform.position = new Vector3(-10, 0, 0);
        }
        if (collision.collider.tag == "Wizard 2")
        {
            Destroy(collision.gameObject);
            transform.position = new Vector3(10, 0, 0);
        }
    }
    void moveController()
    {
        //Wizard 1
        if (Input.GetKeyDown("z"))
        {
            print("Rock");
            if (wizard2 == "Rock" || wizard2 == "start")
            {
                move = 0;
            }
            if (wizard2 == "Paper" || wizard2 == "start")
            {

            }
            if (wizard2 == "Scissors" || wizard2 == "start")
            {
                move = speed;
            }
            wizard1 = "Rock";

        }
        if (Input.GetKeyDown("x"))
        {
            print("Paper");
            if (wizard2 == "Rock" || wizard2 == "start")
            {
                move = speed;
            }
            if (wizard2 == "Paper" || wizard2 == "start")
            {
                move = 0;
            }
            if (wizard2 == "Scissors" || wizard2 == "start")
            {

            }
            wizard1 = "Paper";

        }
        if (Input.GetKeyDown("c"))
        {
            print("Scissors");
            if (wizard2 == "Rock" || wizard2 == "start")
            {

            }
            if (wizard2 == "Paper" || wizard2 == "start")
            {
                move = speed;
            }
            if (wizard2 == "Scissors" || wizard2 == "start")
            {
                move = 0;
            }
            wizard1 = "Scissors";

        }
        //Wizard 2
        if (Input.GetKeyDown(","))
        {
            print("Rock");
            if (wizard2 == "Rock" || wizard1 == "start")
            {
                move = 0;
            }
            if (wizard2 == "Paper" || wizard1 == "start")
            {

            }
            if (wizard2 == "Scissors" || wizard1 == "start")
            {
                move = -speed;
            }
            wizard2 = "Rock";

        }
        if (Input.GetKeyDown("."))
        {
            print("Paper");
            if (wizard2 == "Rock" || wizard1 == "start")
            {
                move = -speed;
            }
            if (wizard2 == "Paper" || wizard1 == "start")
            {
                move = 0;
            }
            if (wizard2 == "Scissors" || wizard1 == "start")
            {

            }
            wizard2 = "Paper";
            
        }
        if (Input.GetKeyDown("/"))
        {
            print("Scissors");
            if (wizard2 == "Rock" || wizard1 == "start")
            {

            }
            if (wizard2 == "Paper" || wizard1 == "start")
            {
                move = -speed;
            }
            if (wizard2 == "Scissors" || wizard1 == "start")
            {
                move = 0;
            }
            wizard2 = "Scissors";

        }
    }
}
