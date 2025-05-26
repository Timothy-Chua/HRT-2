using UnityEngine;
using UnityEngine.Animations;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] private float sensX, sensY;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    private float xRotation, yRotation;
    [SerializeField] private Vector2 yAxisClamp = new Vector2(-90, 90);
    [SerializeField] private float rotationSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterManager.instance.gameState == GameState.Play)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, yAxisClamp.x, yAxisClamp.y);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xRotation, yRotation, 0), Time.deltaTime * rotationSpeed);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            playerObj.rotation = Quaternion.Slerp(playerObj.rotation, orientation.rotation, Time.deltaTime * rotationSpeed);
        }
    }
}
