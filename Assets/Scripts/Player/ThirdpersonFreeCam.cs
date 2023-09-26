using UnityEngine;

public class ThirdPersonFreeCam : MonoBehaviour
{
    public Transform target; // The target object to orbit around
    public float rotationSpeed = 2.0f; // Rotation speed of the camera
    public float distance = 5.0f; // Distance from the target
    public float minYAngle = -30.0f; // Minimum vertical angle
    public float maxYAngle = 80.0f; // Maximum vertical angle

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate new rotation angles
        xRotation += mouseY * rotationSpeed;
        yRotation += mouseX * rotationSpeed;

        // Clamp vertical rotation to the specified range
        xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);

        // Calculate rotation quaternion based on angles
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Calculate camera position based on rotation and distance
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // Apply rotation and position to the camera
        transform.rotation = rotation;
        transform.position = position;
    }
}