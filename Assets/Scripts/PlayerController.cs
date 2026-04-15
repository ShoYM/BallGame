using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI stageNumberText;
    [SerializeField] private GameObject winTextObject;
    [SerializeField] private GameObject loseTextObject;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject nextStageButton;
    
    private Rigidbody rb;
    private int count;

    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        SetStageText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        retryButton.SetActive(false);
        nextStageButton.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 4)
        {
            winTextObject.SetActive(true);
            retryButton.SetActive(true);
            nextStageButton.SetActive(true);
            Destroy(gameObject);
        }
    }
    void SetStageText()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        stageNumberText.text = "Stage: " + currentSceneIndex.ToString();
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            loseTextObject.SetActive(true);
            retryButton.SetActive(true);
        }
    }
}
