using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovment : MonoBehaviour
{
    #region Vars

    [Header ("Movment Vars")]

    public float jumpPower = 1f;
    public float normalSpeed = 2f;
    private Rigidbody2D rb;
    private float speed;
    private Vector2 velocity;


    #endregion


    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        speed = normalSpeed;
    }



    void Update()
    {
        ProcMovment();

    }

    void ProcMovment()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)){
            velocity = new Vector2(0f,jumpPower);
            rb.AddForce(velocity, ForceMode2D.Impulse);

        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Obstacle"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        
    }


 
}
