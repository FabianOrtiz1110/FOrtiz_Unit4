using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject focalPoint;
    Renderer rendererPlayer;
    public float speed = 10.0f;
    public float PowerUpSpeed = 10.0f;
    bool hasPowerUp = false;
    public GameObject powerUpInd;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rendererPlayer = GetComponent<Renderer>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        powerUpInd.transform.position = transform.position;
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Force);

        Debug.Log("Mag:" + magnitude);
        Debug.Log("FI:" + forwardInput);

        if(forwardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f-forwardInput);
        }
            else
            {
                rendererPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
            }

    }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("PowerUp"))
            {
                hasPowerUp = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerUpCountdown());
                powerUpInd.SetActive(true);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(hasPowerUp && collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player has collided with" + collision.gameObject +" with powerup set to: " +hasPowerUp);
                Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayDir = collision.gameObject.transform.position = transform.position;

                rbEnemy.AddForce(awayDir * PowerUpSpeed, ForceMode.Impulse);
            }
        }
        IEnumerator PowerUpCountdown()
        {
            yield return new WaitForSeconds(8);
            hasPowerUp = false; 
            powerUpInd.SetActive(false);
        }
    }
