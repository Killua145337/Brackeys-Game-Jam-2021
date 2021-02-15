using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Serialized Variables
    [SerializeField]  float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpWaitTime;
    [SerializeField] float dashOnCooldownTime;

    [SerializeField] bool dashAbility;
    [SerializeField] GameObject energyParticles;

    //Sounds
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip landSFX;

    //Vector3 Variables
    Vector2 input;
    Vector3 moveVelocity;
    Vector3 moveInput;

    public bool isGrounded = true;
    private bool dashOnCooldown;

    //Cached References
    Rigidbody rigidBody;
    Animator animator;
    Camera mainCamera;
    Ray lookRay;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
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

        animator.SetFloat("Speed", rigidBody.velocity.magnitude);

        Jump();

        //Check if dash ability is enabled, 'T' key is pressed, and dash is not on cooldown
        if (dashAbility && Input.GetKeyDown(KeyCode.T) && !dashOnCooldown)
        {
            StartCoroutine(Dash());
        }
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
            AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
            rigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            animator.SetBool("jump", true);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        isGrounded = true;
        AudioSource.PlayClipAtPoint(landSFX, transform.position);
        animator.SetBool("jump", false);
    }

   IEnumerator Dash()
    {
        dashOnCooldown = true;

        float cameraTurnSpeedTemp = transform.parent.GetComponentInChildren<UnityStandardAssets.Cameras.FreeLookCam>().m_TurnSpeed;

        //Start dash
        moveSpeed *= 3;
        transform.parent.GetComponentInChildren<UnityStandardAssets.Cameras.FreeLookCam>().m_TurnSpeed = 0.5f;
        GameObject dashParticles = Instantiate(energyParticles, transform.position, Quaternion.identity);


        //Continue after 1 second
        yield return new WaitForSeconds(1);

        //Stop dash
        moveSpeed /= 3;
        transform.parent.GetComponentInChildren<UnityStandardAssets.Cameras.FreeLookCam>().m_TurnSpeed = cameraTurnSpeedTemp;
        Destroy(dashParticles);

        //Continue after a further 3 seconds
        yield return new WaitForSeconds(dashOnCooldownTime);

        dashOnCooldown = false;
    }
}
