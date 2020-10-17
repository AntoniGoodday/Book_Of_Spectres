using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    float speed;
    Vector2 input;

    Vector2 inputDir;

    [SerializeField]
    float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    [SerializeField]
    Vector3 moveVector;

    [SerializeField]
    Transform facingTest;

    private void Start()
    {
    }

    private void Update()
    {
        //moveX = Input.GetAxisRaw("Horizontal");
        //moveZ = Input.GetAxisRaw("Vertical");

        //moveVector = new Vector3((transform.position.x - cam.transform.position.x) * moveX, 0f, (transform.position.z - cam.transform.position.z) * moveZ);

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            //facingTest.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            facingTest.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(facingTest.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
        {
            transform.Translate(facingTest.forward * speed * Time.deltaTime, Space.World);
        }

        /*Vector3 camF = cam.transform.forward;
        camF.y = 0;
        camF = camF.normalized;
        Vector3 camR = cam.transform.right;
        camR.y = 0;
        camR = camR.normalized;


       
        //rb.AddForce(new Vector3(modifiedInput.x, rb.velocity.y, modifiedInput.y));
        rb.velocity = new Vector3(modifiedInput.x, rb.velocity.y, modifiedInput.y);*/
    }

}
