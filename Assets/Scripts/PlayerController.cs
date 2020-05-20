using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 1000f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text WinLoseText;
    public GameObject WinLoseObject;
    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Update()
    {
        if(health == 0)
        {
            StartCoroutine("LoadScene", 3f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }

    }
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    // fixed beacuse includes physiscs
    //move awsd
    void FixedUpdate()
    {
        if( Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
        if( Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if( Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
        if( Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
    }
    //Ontrigger destroy and count healt or score or win
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            score++;
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            if (health == 0)
            {
                //change color and text to text
                WinLoseText.text = "Game Over!";
                WinLoseText.color = Color.white;
                //change color to image
                WinLoseObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                //Activate the image to show if Lose
                WinLoseObject.SetActive(true);
                
            }
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            //change color and text to text
            WinLoseText.text = "You Win!";
            WinLoseText.color = Color.black;
            //change color to image
            WinLoseObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            //Activate the image to show if win
            WinLoseObject.SetActive(true);
            StartCoroutine("LoadScene", 3f);
        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

}
