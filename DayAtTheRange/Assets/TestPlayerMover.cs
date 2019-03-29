using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMover : MonoBehaviour
{

    /*
     * This script's purpose is to simply move the Player Game Object on the X, Z plane 
     */

    // Declare Variables
    private Rigidbody rb; // References to Rigidbody Component
    public float movementSpeed; // Assigned in Unity Editor, Speed at which player moves
    public float lookSpeed; // Assigned in Unity Editor, Speed at which player moves

    private HeadReference playerHead; // Grabs reference to transform head of Player
    private GameObject playerHeadObject;

    //Camera fpsCam;

    float xAxisClamp;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Grabs reference to Player's Rigidbody component
        Cursor.lockState = CursorLockMode.Locked; // Locks mouse to center of screen

        /*
        GameObject playerHeadObject = GameObject.FindWithTag("PlayerHead");
        if (playerHeadObject != null)
        {
            playerHead = playerHeadObject.GetComponent<HeadReference>();
        }
        */
        playerHeadObject = GameObject.Find("Player Head");

        //fpsCam = GetComponent<Camera>();

        xAxisClamp = 0.0F;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(moveHorizontal, 0.0F, moveVertical);
        rb.velocity = movementInput * movementSpeed; // Moves independently of rotation of object
        //rb.AddRelativeForce((movementInput)*movementSpeed); // Moves dependent of rotation of object

        Vector3 forwardBackwardMovement = transform.forward * moveVertical * movementSpeed;
        Vector3 leftRightMovement = transform.right * moveHorizontal * movementSpeed;
        // Player Look

        float lookHorizontal = Input.GetAxis("Mouse X");
        float lookVertical = Input.GetAxis("Mouse Y");
        //characterController.SimpleMove(forwardBackwardMovement + leftRightMovement);

        Vector3 lookInput = new Vector3(-lookVertical, lookHorizontal, 0.0F);
        //transform.Rotate(-transform.right * lookVertical); // Rotates Torso up down
        transform.Rotate(Vector3.up * lookHorizontal); // rotates Torso left right
        //transform.Rotate(transform.forward * lookHorizontal); //  rotates Torso in lean left right

        playerHeadObject.transform.Rotate(Vector3.left * lookVertical); // Rotates Head up down
        //playerHeadObject.transform.Rotate(transform.up * lookHorizontal); // Rotates  Head left right
        //playerHeadObject.transform.rotation.y = transform.Rotate(transform.up * lookHorizontal);
        //transform.Rotate(transform.up * lookVertical);

        // Below Code keeps Player head tilt locked between 90 degree verticle and 
        xAxisClamp += lookVertical;

        if (xAxisClamp > 90.0F)
        {
            xAxisClamp = 90.0F;
            lookVertical = 0.0F;
            ClampXAxisRotationToValue(270.0F);

        }
        else if (xAxisClamp < -90.0F)
        {
            xAxisClamp = -90.0F;
            lookVertical = 0.0F;
            ClampXAxisRotationToValue(90.0F);
        }

        /*
       Vector3 targetDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Camera.main.transform.TransformDirection(targetDirection);
       targetDirection.y = 0.0f;
       rb.velocity = targetDirection * movementSpeed;
       */
    }
    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = playerHeadObject.transform.eulerAngles;
        eulerRotation.x = value;
        playerHeadObject.transform.eulerAngles = eulerRotation;
    }
    /*
    void Shoot()
    {
        Camera fpsCam;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100F)) { }
    }
    */
}
