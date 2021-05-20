using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed = 5.0f;
    public float RotationSpeed = 2.0f;
    public float JumpHeight = 1.0f;

    private Transform _groundChecker;
    public float GroundDistance = 0.15f;
    public LayerMask Ground;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _groundChecker = transform.Find("GroundChecker");

        _velocity = new Vector3();
        _isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get input
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        //Check if character is grounded
        //_isGrounded = _controller.isGrounded;
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        //Rotate
        transform.Rotate(0, horizontalMove * RotationSpeed * Time.deltaTime, 0);

        //Forward movement
        Vector3 _movement = transform.forward * verticalMove;

        //Gravity
        _velocity.y += Physics.gravity.y * Time.deltaTime;
        if (_isGrounded){
            _velocity.y = 0;
        }

        //Jump
        if (Input.GetButtonDown("Jump") && _isGrounded){
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
        }

        //Move charactercontroller
        _controller.Move(WalkSpeed * Time.deltaTime * _movement + _velocity * Time.deltaTime);
    }
}
