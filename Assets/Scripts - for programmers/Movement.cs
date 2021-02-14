using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _movespeed;
    [SerializeField] private float _JumpForce;

    Vector2 _Input;
    Vector3 _moveVelocity;
    Vector3 _moveInput;

    bool _jump;
    bool canJump;

    Rigidbody _Rigidbody;
    Camera mainCamera;
    Ray lookRay;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _Input.x = Input.GetAxis("Horizontal");
        _Input.y = Input.GetAxis("Vertical");
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _jump = true;
        }
        else
        {
            _jump = false;
        }

        _moveInput = new Vector3(_Input.x, 0, _Input.y);
        //_moveInput.Normalize();
        _moveVelocity = transform.forward * _movespeed * _moveInput.sqrMagnitude;

        Vector3 CameraForward = mainCamera.transform.forward;
        CameraForward.y = 0;

        Quaternion CameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, CameraForward);

        Vector3 looktoward = CameraRelativeRotation * _moveInput;

        if (_moveInput.sqrMagnitude > 0)
        {
            lookRay = new Ray(transform.position, looktoward);
            Vector3 dir = lookRay.GetPoint(1);
            dir.y = transform.position.y;
            transform.LookAt(dir);
        }
    }


    private void FixedUpdate()
    {
        if (_jump)
        {
            _Rigidbody.AddForce(Vector3.up * _JumpForce);
            _jump = false;
        }

        _moveVelocity.y = _Rigidbody.velocity.y;
        _Rigidbody.velocity = _moveVelocity;
    }
}
