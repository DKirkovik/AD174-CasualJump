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
    public float minY;
    public float maxY;
    private Rigidbody2D rb;
    private float speed;
    private Vector2 velocity;

    [Header ("SoundFX")]

    public GameObject destroySound;
    public AudioSource destroyAudio;
    public float waitTime;
    public GameObject playerSprite;
    public ParticleSystem playerPart;


    #endregion


    void Awake()
    {
        //Set vars
        rb = this.GetComponent<Rigidbody2D>();
        speed = normalSpeed;
        destroyAudio = destroySound.GetComponent<AudioSource>();
        playerPart = gameObject.GetComponent<ParticleSystem>();
    }



    void Update()
    {
        //Process movment
        ProcMovment();

    }

    void ProcMovment()
    {
        //Clamp player movment on Y
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, Mathf.Clamp(this.gameObject.transform.position.y,minY,maxY), this.gameObject.transform.position.z);
        
        //Input jump
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)){
            velocity = new Vector2(0f,jumpPower);
            rb.AddForce(velocity, ForceMode2D.Impulse);

        }
        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        //Collision
        if(other.gameObject.tag == "Obstacle"){
            CallDeath();

        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Border"){
            CallDeath();
        }
        
    }


    void CallDeath()
    {
        playerSprite.SetActive(false);
        playerPart.Stop();
        StartCoroutine(DeathCooldown());

    }

    IEnumerator DeathCooldown()
    {
        //Play audio then wait and change scene
        destroyAudio.PlayOneShot(destroyAudio.clip);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


 
}
