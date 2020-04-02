using UnityEngine;


/// <summary>
/// We always want to have a Rigidbody with our PlayerMotor
/// </summary>
[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// Has a set of functions that will move the player, make him jump, make him fly.
/// It doesn't handle any of the input at all.
/// A utility script that will take directions and velocities to apply them to the RigidBody.
/// </summary>
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Camera cam; // Can't name this 'camera' because that is a keyword included in MonoBehaviour

    private Vector3 velocity = Vector3.zero; // Vector3 defaults to zero anyway.
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    //private float cameraRotationX = 0f; // used later???
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField] private float cameraRotationLimit = 85f;

    private Rigidbody rigidBody;

    /// <summary>
    /// TODO
    /// </summary>
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    //We want to have this PlayerController script control the PlayerMotor.velocity variable, and then we want a fixed update loop here that uses this PlayerMotor.velocity variable to move the player.

    /// <summary>
    /// Method for setting our velocity variable.
    /// </summary>
    /// <param name="_velocity">Movement Vector</param>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="_rotation">Rotational vector</param>
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    /// <summary>
    /// Gets a rotational vector for the camera.
    /// </summary>
    /// <param name="_rotation">Gets a rotational vector</param>
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    /// <summary>
    /// Gets a rotational vector for the camera.
    /// This is used later???
    /// </summary>
    /// <param name="_rotation">Gets a rotational vector</param>
    //public void RotateCamera(float _cameraRotationX)
    //{
    //    cameraRotationX = _cameraRotationX;
    //}

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
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime); 
            // Stop the rigidbody from moving if it collides with something on the way.
            // Performs all the physics and collision checks, but it's much easier to control than the AddForce() method.
            // This will move our rigidbody, our player, to the position of our player plus the velocity vector. This performs the movement.
        }

        if (thrusterForce != Vector3.zero)
        {
            rigidBody.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Perform rotation
    /// </summary>
    void PerformRotation()
    {
        // Quaternions are just like Vector3 but with an imaginary component. (Difficult to understand)
        // Euler angles are the angles with (x,y,z) rotation that we know. (Vector3)
        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotation));  // rb.rotation is a Quaternion, and Quaternion.Euler will take in our Vector3 and make it into a Quaternion.

        // If we have put in a camera
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);

        }


        // If we have put in a camera
        // This is used later???
        //if (cam != null)
        //{
        //    // Set our rotation and clamp it
        //    currentCameraRotationX -= cameraRotationX; // If something is strange, like a camera inversion or something, change '-=' to '+='
        //    currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        //    // Apply our rotation to the transform of our camera
        //    cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        //}
    }


}
