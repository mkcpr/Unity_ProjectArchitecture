using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRB : MonoBehaviour
{
    public float WalkSpeed = 5.0f;
    public float RotationSpeed = 200.0f;
    public float JumpHeight = 1.0f;

    private Transform _groundChecker;
    public float GroundDistance = 0.15f;
    public LayerMask Ground;

    private Rigidbody _rb;
    private Vector3 _movement;
    private Vector3 _rotation;

    private Vector3 _velocity;
    public bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _groundChecker = transform.Find("GroundChecker");
    }

    // Update is called once per frame
    void Update()
    {
        //Get input
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        //Check if character is grounded
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        //Forward movement
        _movement = transform.forward * verticalMove;

        //Rotate 
        _rotation = Vector3.up * horizontalMove * RotationSpeed;

        //Jump
        if (Input.GetButtonDown("Jump") && _isGrounded){
            _rb.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (WalkSpeed * Time.fixedDeltaTime * _movement));

        Quaternion _deltaRotation = Quaternion.Euler(_rotation * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * _deltaRotation);
    }
}
