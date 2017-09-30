using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public Text collectedGemsText;
    public Text winText;
    public Text allGemsCollectedText;
    public float timeToDisplayAllGemsCollectedText;
    private int collectedGems = 0;
    private int maxGems = 0;
    private float startDisplayTime = 0;

	// Use this for initialization
	public void Start () {
        rb = GetComponent<Rigidbody>();
        maxGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        collectedGemsText.text = "Collected: " + collectedGems.ToString() + " of " + maxGems;
    }

    public void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = 0;
        float moveZ = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveY = (float) 5;
        }
        Vector3 move = new Vector3(moveX, moveY, moveZ);

        rb.AddForce(move * speed);

        if (allGemsCollectedText.gameObject.activeSelf && (Time.time-startDisplayTime)>timeToDisplayAllGemsCollectedText)
        {
            allGemsCollectedText.gameObject.SetActive(false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gem":
                collision.gameObject.SetActive(false);
                collectedGems++;
                collectedGemsText.text = "Collected: " + collectedGems.ToString() + " of "+maxGems;
                if (collectedGems == maxGems)
                {
                    allGemsCollectedText.gameObject.SetActive(true);
                    startDisplayTime = Time.time;
                }
                break;
            case "Target":
                if (collectedGems == maxGems)
                {
                    Application.Quit();
                }
                break;
        }
    }
}
