using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private AudioSource footSource;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    float horizontalInput, verticalInput;
    Vector3 moveDir;
    Rigidbody rb;
    bool isAudioPlaying;

    [SerializeField] private float groundDrag = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isAudioPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterManager.instance.gameState == GameState.Play)
        {
            GetInput();
            SpeedControl();
            rb.linearDamping = groundDrag;
        }

        if (moveDir != Vector3.zero && !isAudioPlaying)
        {
            footSource.Play();
            isAudioPlaying = true;
        }
        else if (moveDir == Vector3.zero && isAudioPlaying)
        {
            footSource.Stop();
            isAudioPlaying = false;
        }
    }

    private void FixedUpdate()
    {
        if (MasterManager.instance.gameState == GameState.Play)
        {
            MovePlayer();
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        bool isMoving = (moveDir != Vector3.zero) ? true : false;

        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
