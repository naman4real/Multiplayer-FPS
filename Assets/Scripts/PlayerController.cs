
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float sensitivity = 5f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring Settings")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private ConfigurableJoint joint;



    private PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        motor.Move(velocity);

        // rotation around y axis(game object)
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * sensitivity;
        motor.Rotate(rotation);

        //rotation around x axis (camera)
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRot * sensitivity;
        motor.RotateCamera(cameraRotationX);


        Vector3 _thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);


        }
        else
        {
            SetJointSettings(jointSpring);
        }
        motor.applyThruster(_thrusterForce);


    }
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {

            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
