using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Serialized Variables
    [SerializeField]  float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpWaitTime;

    //Vector3 Variables
    Vector2 input;
    Vector3 moveVelocity;
    Vector3 moveInput;

    public bool isGrounded = true;

    //Cached References
    Rigidbody rigidBody;
    Animator animater;
    Camera mainCamera;
    Ray lookRay;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        animater = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(input.x, 0, input.y).normalized;
        moveVelocity = transform.forward * moveSpeed * moveInput.sqrMagnitude;

        Vector3 CameraForward = mainCamera.transform.forward;
        CameraForward.y = 0;

        Quaternion CameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, CameraForward);

        Vector3 looktoward = CameraRelativeRotation * moveInput;

        if (moveInput.sqrMagnitude > 0)
        {
            lookRay = new Ray(transform.position, looktoward);
            Vector3 dir = lookRay.GetPoint(1);
            dir.y = transform.position.y;
            transform.LookAt(dir);
        }

        animater.SetFloat("Speed", rigidBody.velocity.magnitude);

        Jump();

    }

    private void FixedUpdate()
    {
        moveVelocity.y = rigidBody.velocity.y;
        rigidBody.velocity = moveVelocity;

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isGrounded = false;
        }

        if(isGrounded == false)
        {
            animater.SetFloat("Speed", 0f);
        }

    }

    private void OnCollisionEnter(Collision other) {
        isGrounded = true;
    }

   
}
