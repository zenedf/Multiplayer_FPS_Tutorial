using System;
using UnityEngine;

/// <summary>
/// TODO
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Vector3 velocity = Vector3.zero; // Vector3 defaults to zero anyway.
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField] private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    /// <summary>
    /// TODO
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Method for setting our velocity variable.
    /// Gets a movement vector.
    /// </summary>
    /// <param name="_velocity">TODO</param>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="_rotation">Gets a rotational vector</param>
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    
    /// <summary>
    /// Gets a rotational vector for the camera.
    /// </summary>
    /// <param name="_rotation">Gets a rotational vector</param>
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    // Get a force vector for our thrusters
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    /// <summary>
    /// Fixed update loop 
    /// Run every physics iteration
    /// </summary>
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    /// <summary>
    /// Perform movement based on velocity variable.
    /// Check if velocity is not equal to Vector3.zero
    /// </summary>
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); 
            // Stop the rigidbody from moving if it collides with something on the way.
            // Performs all the physics and collision checks, but it's much easier to control than the AddForce() method.
            // This will move our rigidbody, our player, to the position of our player plus the velocity vector. This performs the movement.
        }

        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Perform rotation
    /// </summary>
    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));  // rb.rotation is a Quaternion, and Quaternion.Euler will take in our Vector3 and make it into a Quaternion.

        // If we have put in a camera
        if (cam != null)
        {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX; // If something is strange, like a camera inversion or something, change '-=' to '+='
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            // Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }


}
