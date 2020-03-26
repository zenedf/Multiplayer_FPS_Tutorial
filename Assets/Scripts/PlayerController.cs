using UnityEngine;

/// <summary>
/// TODO
/// </summary>
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;

    private PlayerMotor motor;

    /// <summary>
    /// TODO
    /// </summary>
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    /// <summary>
    /// TODO
    /// </summary>
    void Update()
    {
        // Calculate movement velocity as a 3D vector
        float _xMovement = Input.GetAxisRaw("Horizontal"); // Horizontal goes between -1 and 1 for either a mouse or a keyboard.
        float _zMovement = Input.GetAxisRaw("Vertical");   // The same goes for Vertical.

        // We use 'transform.right' instead of 'Vector3.right' because we want to take our rotation into consideration.
        Vector3 _moveHorizontal = transform.right * _xMovement; // (1, 0, 0) = moving forward, (0, 0, 0) = not moving, (-1, 0, 0) = moving backward
        Vector3 _moveVertical = transform.forward * _zMovement; // (0, 0, 1) = moving forward, (0, 0, 0) = not moving, (0, 0, -1) = moving backward

        // The total length of this combined vector should be 1, so we shouldn't get a varying speed. We will always get a vector with a length of 1.
        // So, we are only using this as a direction that we then multiply with the speed.
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed; // Final movement vector

        // Apply movement
        motor.Move(_velocity);

        // Calculate rotation as a 3D vector (turning around).
        float _yRotation = Input.GetAxisRaw("Mouse X"); // When you move your mouse around the x axis, you want to rotate around the y axis.
                                                        // We only want the 'x axis / y rotation' to affect the player.
                                                        // We only want the 'y axis / x rotation' to affect our camera.

        Vector3 _rotation = new Vector3 (0f, _yRotation, 0f) * lookSensitivity;

        // Apply rotation
        motor.Rotate(_rotation);

        // Calculate camera rotation as a 3D vector
        float _xRotation = Input.GetAxisRaw("Mouse Y"); 
                                                        
        Vector3 _cameraRotation = new Vector3(_xRotation, 0f, 0f) * lookSensitivity;

        // Apply rotation
        motor.RotateCamera(_cameraRotation);

    }
}
