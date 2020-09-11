using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    float speed = 4f;

    [SerializeField]
    GameObject cameraMover;

    Vector3 forward;
    Vector3 right;
    Vector3 heading;

    Vector3 tempForward;
    Vector3 tempRight;
    Vector3 tempVector;
    bool rotationIsDone = true;

    [Range(1, 10)]
    public float jumpVelocity;

    //rigidbody
    public Rigidbody rb;

    public float maxSpeed;

    //for checking if grounded
    float distToGround;

    //for making diagonal movement same speed as other directions
    float xAxis;
    float yAxis;
    int diagonalModifier = 1;

    Collision collisionMain;

    void Move()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        xAxis = Mathf.Abs(Input.GetAxis("Horizontal"));

        yAxis = Mathf.Abs(Input.GetAxis("Vertical"));

        if (xAxis + yAxis <= 1)
        {
            diagonalModifier = 1;
        }
        else
        {
            diagonalModifier = 2;
        }
        Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal") / diagonalModifier;
        Vector3 upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical") / diagonalModifier;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);



        if (xAxis + yAxis != 0)
        {
            transform.forward = heading;
        }
        rb.AddForce(rightMovement * speed, ForceMode.VelocityChange);
        rb.AddForce(upMovement * speed, ForceMode.VelocityChange);
        /*transform.position += rightMovement;
        
        transform.position += upMovement;*/

    }
    IEnumerator RotationCoroutine(Vector3 axis, float angle, float duration = 1.0f)
    {
        if (angle == 90)
        {
            rotationIsDone = false;

            tempVector = tempForward;
            tempForward = tempRight;
            tempRight = -tempVector;
        }
        else if (angle == -90)
        {
            rotationIsDone = false;

            tempVector = tempForward;
            tempForward = -tempRight;
            tempRight = tempVector;
        }
        Vector3 axisCorrection = new Vector3(0, angle, 0);
        Quaternion from = cameraMover.transform.rotation;
        Quaternion to = cameraMover.transform.rotation;
        to *= Quaternion.Euler(axisCorrection);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            cameraMover.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraMover.transform.rotation = to;
        rotationIsDone = true;
        yield return null;
    }

    /*private void OnCollisionStay(Collision collision)
    {
        ContactPoint contact = collision.contacts[0]; 
        if(contact.normal.y < 0.1f)
        {
            //Debug.DrawRay(contact.point, contact.normal, Color.blue, 1.25f);
            if (Input.GetButtonDown("Jump") && !Physics.Raycast(transform.position, -Vector3.up, distToGround + 0, 5))
            {
                rb.AddForce((Vector3.up + contact.normal)* 800);
                Debug.DrawRay(contact.point, contact.normal, Color.red, 5f);
            }
        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        collisionMain = collision;
    }
    private void OnCollisionExit(Collision collision)
    {
        collisionMain = null;
    }

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        forward = cam.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        tempForward = forward;
        tempRight = right;

        distToGround = GetComponent<Collider>().bounds.extents.y;
    }



    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(rb.velocity);
        if (rb.velocity.magnitude > maxSpeed)
        {

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        if (Input.anyKey)
        {
            if (Input.GetButtonDown("Shoot") && Physics.Raycast(transform.position, -Vector3.up, distToGround + 0, 5))
            {
                forward = tempForward;
                right = tempRight;
                //GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
                rb.AddForce(Vector3.up * 800);
            }
            if (collisionMain != null)
            {
                ContactPoint contact = collisionMain.contacts[0];
                if (contact.normal.y < 0.1f)
                {
                    //Debug.DrawRay(contact.point, contact.normal, Color.blue, 1.25f);
                    if (Input.GetButtonDown("Shoot") && !Physics.Raycast(transform.position, -Vector3.up, distToGround + 0, 5))
                    {
                        rb.AddForce((Vector3.up + contact.normal) * 800);
                        Debug.DrawRay(contact.point, contact.normal, Color.red, 5f);
                    }
                }
            }
            Move();


        }
        else
        {
            forward = tempForward;
            right = tempRight;
        }


        if (rotationIsDone == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(RotationCoroutine(Vector3.up, -90, 1.0f));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(RotationCoroutine(Vector3.up, 90, 1.0f));
            }
        }

    }
}
