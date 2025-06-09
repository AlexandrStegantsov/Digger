using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float shiftMultiplier = 2f;
    public float mouseSensitivity = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Вращение мышью
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);

        // Передвижение
        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * shiftMultiplier : moveSpeed;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = transform.TransformDirection(direction) * speed * Time.deltaTime;
        transform.position += velocity;
    }
}
