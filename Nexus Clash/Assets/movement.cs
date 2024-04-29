using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Die Geschwindigkeit, mit der der Spieler sich bewegt
    public float jumpForce = 10f; // Die Kraft, mit der der Spieler springt
    public KeyCode jumpKey = KeyCode.Space; // Die Taste zum Springen
    public KeyCode[] moveKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }; // Die Tasten für die Bewegung

    public float rotationSpeed = 100f; // Die Geschwindigkeit der Kameradrehung
    public KeyCode rotateKey = KeyCode.Mouse1; // Die Taste zum Drehen der Kamera (Rechtsklick)

    private Rigidbody rb;
    private bool isGrounded;

    private float verticalLookRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Damit der Mauszeiger in der Szene zentriert und versteckt ist
    }

    void Update()
    {
        // Prüfen, ob der Spieler auf dem Boden ist
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Die Eingabe für die horizontale und vertikale Bewegung
        float horizontalInput = 0f;
        float verticalInput = 0f;

        foreach (KeyCode key in moveKeys)
        {
            if (Input.GetKey(key))
            {
                if (key == KeyCode.W)
                    verticalInput = 1f;
                else if (key == KeyCode.S)
                    verticalInput = -1f;
                else if (key == KeyCode.A)
                    horizontalInput = -1f;
                else if (key == KeyCode.D)
                    horizontalInput = 1f;
            }
        }

        // Die Richtung, in die der Spieler sich bewegt
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Die Bewegung basierend auf der Richtung und der Geschwindigkeit
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Springen, wenn die Sprungtaste gedrückt wird und der Spieler auf dem Boden ist
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Kameradrehung mit Rechtsklick
        if (Input.GetKey(rotateKey))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            verticalLookRotation -= mouseY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f); // Begrenzt die vertikale Rotation auf -90 bis 90 Grad

            transform.Rotate(Vector3.up * mouseX);
            Camera.main.transform.localEulerAngles = new Vector3(verticalLookRotation, 0f, 0f);
        }
    }
}
