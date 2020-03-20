﻿
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]


public class PlayerMotor : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotation = 0f;
    private Vector3 thrusterForce = Vector3.zero;
    private ConfigurableJoint joint;

    private float cameraRotationLimit = 85f;


    [SerializeField]
    private Camera cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = this.GetComponent<ConfigurableJoint>();

        
    }
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void applyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        
    }
    void PerformMovement()
    {
        if (velocity!=Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (thrusterForce!=Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
       
        }

    }
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam!=null)
        {
            currentCameraRotation -= cameraRotationX;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
        }
    }
  
}
