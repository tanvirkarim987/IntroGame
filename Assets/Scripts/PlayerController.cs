using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI velocityText;

    private int count;
    private int numPickups = 5; // must match the number of PickUps in the scene
    private Vector3 oldPosition;

    void Start()
    {
        count = 0;
        winText.text = "";
        SetCountText();
        oldPosition = transform.position;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        // Velocity from position change over time, as the lab asks (no Rigidbody.velocity)
        Vector3 velocity = (transform.position - oldPosition) / Time.deltaTime;

        Vector3 pos = transform.position;
        positionText.text = "Position: (" + pos.x.ToString("0.00") + ", "
            + pos.y.ToString("0.00") + ", " + pos.z.ToString("0.00") + ")";

        velocityText.text = "Velocity: (" + velocity.x.ToString("0.00") + ", "
            + velocity.y.ToString("0.00") + ", " + velocity.z.ToString("0.00") + ")"
            + "  Speed: " + velocity.magnitude.ToString("0.00") + " units/s";

        // Must be last so next frame compares against this frame's position
        oldPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if (count >= numPickups)
        {
            winText.text = "You win!";
        }
    }
}