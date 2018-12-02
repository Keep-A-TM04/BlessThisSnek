using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaYoPlayerController : MonoBehaviour {

    //movement variables
    public float speed;
    private Rigidbody2D rb2d;
    public float spin;

    //win condition test
    private bool win;

    //text declarations
    //public Text instructionText;
    public Text winText;
    public Text countText;

    //count down variable
    float timeLeft = 10.0f;

    //audio
    private AudioSource source;
    public AudioClip boop;
    public AudioClip heck;
    public AudioClip lose;
    private int sounds;
    private bool alreadyPlayed = false;

    //particles
    public Transform flowers;
    

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        flowers.GetComponent<ParticleSystem>().enableEmission = false;
        //InstructionText();
        win = false;
        winText.text = "";
        sounds = 0;
	}


	void FixedUpdate () {
        //Basic movement controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        //rotation movement controls
        //first line too snappy
        if (Input.GetKeyDown("u"))
        {
            //GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            transform.Rotate(0, 0, -spin);
        }
        if (Input.GetKeyDown("i"))
        {
            //GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            transform.Rotate(0, 0, spin);
        }

        //countdown
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            win = false;
        }

        //call count down function
        CountDown();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            win = true;
            source.PlayOneShot(boop);
            flowers.GetComponent<ParticleSystem>().enableEmission = true;
        }
    }

    //seperate script now
    /*
    void InstructionText()
    {
        while (timeLeft >= 8)
        {
            instructionText.text = "Boop the Snek";
        }
        instructionText.text = "";
    }
    */

    IEnumerator ByeAfterDelay(float timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        //GameLoader.gameOn = false;
    }

    void CountDown()
    {
        //comment next line, moved to GameLoader
        //countText.text = timeLeft.ToString();
        if (win == true)
        {
            winText.text = "O H   H E C K !";
            //GameLoader.AddScore(10);
            StartCoroutine(ByeAfterDelay(2));
            sounds = 1;
            Sounds();
        }
        else if (win == false && timeLeft <= -1)
        {
            winText.text = "Y O U   L O S E !";
            sounds = 2;
            Sounds();
        }
    }

    void Sounds()
    {
        if (sounds == 1 && alreadyPlayed == false)
        {
            source.PlayOneShot(heck);
            alreadyPlayed = true;
        }
        else if (sounds == 2 && alreadyPlayed == false)
        {
            source.PlayOneShot(lose);
            alreadyPlayed = true;
        }
    }
}
