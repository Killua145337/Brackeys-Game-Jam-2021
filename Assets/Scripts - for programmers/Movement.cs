using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]  float moveSpeed;
    [SerializeField]  float jumpForce;

    Vector2 input;
    Vector3 moveVelocity;
    Vector3 moveInput;

    bool jump;
    bool canJump;

    Rigidbody rigidBody;
    Camera mainCamera;
    Ray lookRay;
    Animator animater;
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

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

    }


    private void FixedUpdate()
    {
        if (jump)
        {
            rigidBody.AddForce(Vector3.up * jumpForce);
            jump = false;
        }

        moveVelocity.y = rigidBody.velocity.y;
        rigidBody.velocity = moveVelocity;

    }
}
