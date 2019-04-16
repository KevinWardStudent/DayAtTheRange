using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerMover : MonoBehaviour
{

    /*
     * 
     * This is a Singleton. This code will give you a reference to this script anywhere in your scene
     * as long as you don't have this script attached to more than one object.
     * 
     * Syntax for finding this class in other classes:
     * TestPlayerMover player
     * Start(){
     *      player = TestPlayerMover.instance;
     * }
     * 
     */
    public static TestPlayerMover instance;
    private void Awake()
    {
        instance = this;
    }

    /*
     * This script's purpose is to simply move the Player Game Object on the X, Z plane, it was also intended to move the player in rotation
     * on the Z axis. This however was changed to be handled by other scripts which play animations to accomplish the same task. Nonetheless,
     * the code is left in for testing purposes and to show that certain functions are achieved in this way. Meaning that its useless for reference
     * when trying to write other scripts. This also used in other cases.
     * This script also handles switching references to the player's weapons
     * 
     */

    // Declare Variables
    private Animator anim; // Reference to Animator component
    private Rigidbody rb; // References to Rigidbody Component
    public float movementSpeed; // Assigned in Unity Editor, Speed at which player moves
    public float lookSpeed; // Assigned in Unity Editor, Speed at which player moves

    //private HeadReference playerHead; // Grabs reference to transform head of Player--This is commented out in case needed to be called on later, but 
    private GameObject playerHeadObject; // Reference object which holds the transform of the head of the Player and which then is stored in playerHead

    //Camera fpsCam;

    float xAxisClamp; // Float value to determine the mximum distance the player can look up or down while rotating on the x axis
    float xAxisClampXbox; // Xbox version of above code
    float zAxisClamp; // Float value to determine the maximum distance the player "leans" aka rotate on the z axis


    // Variables for Weapon Switching
    private int indexSwitchWeapon; // Int used to set different states to check what weapon is selected
    private bool consoleTextDisplayed; // Control Flag to prevent overload in console of text
    public GameObject rifle; // Assigned in Unity Editor, grab reference to Rifle
    public GameObject pistol; // Assigned in Unity Editor, grab reference to Pistol
    public GameObject grenade; // Assigned in Unity Editor, grab reference to Grenade

    public Text ammoText;       //Text for ammo on UI.


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>(); // Grabs reference to Player's Animator component
        rb = GetComponent<Rigidbody>(); // Grabs reference to Player's Rigidbody component
        Cursor.lockState = CursorLockMode.Locked; // Locks mouse to center of screen

        /*
        GameObject playerHeadObject = GameObject.FindWithTag("PlayerHead");
        if (playerHeadObject != null)
        {
            playerHead = playerHeadObject.GetComponent<HeadReference>();
        }
        */
        playerHeadObject = GameObject.Find("Player Head"); // A more efficent way of storing a reference to the Player's head than the above method
        //fpsCam = playerHeadObject.GetComponent<Camera>();

        xAxisClamp = 0.0F;
        xAxisClampXbox = 0.0F;
        zAxisClamp = 0.0F;

        rb.isKinematic = false;
        indexSwitchWeapon = 0; // Rifle is first weapon selected, this will only be included in earlier build, later index value will be needed for
        consoleTextDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {

        /*
        
        if (Input.GetAxis("Lean") > 0 && !(Input.GetAxis("Lean") < 0))
        {
            Debug.Log("Do the lean! 'Right'");
            anim.SetBool("LeanRight", true);
        }
        else
        {
            anim.SetBool("LeanRight", false);
        }
        if (Input.GetAxis("Lean") < 0 && !(Input.GetAxis("Lean") > 0))
        {
            Debug.Log("Do the lean! 'Left'");
            anim.SetBool("LeanLeft", true);
        }
        else
        {
            anim.SetBool("LeanLeft", false);
        }
        */
        
    }
    void FixedUpdate()
    {
        // Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // Keyboard: AD Respectively
        float moveVertical = Input.GetAxis("Vertical"); // Keyboard: WS Respectively
        float moveHorizontalXbox = Input.GetAxis("LeftStickH"); // Xbox Controller: Left Stick Horizontal
        float moveVerticalXbox = Input.GetAxis("LeftStickV"); // Xbox Controller: Left Stick Vertical

        Vector3 movementInput = new Vector3(moveHorizontal, 0.0F, moveVertical);
        Vector3 movementInputXbox = new Vector3(moveHorizontalXbox, 0.0F, moveVerticalXbox);
        //rb.velocity = movementInput * movementSpeed; // Moves independently of rotation of object
        rb.AddRelativeForce((movementInput) * movementSpeed); // Moves dependent of rotation of object---This works
        rb.AddRelativeForce((movementInputXbox) * movementSpeed); // Xbox Version of above Code
        //rb.MovePosition(rb.position + movementInput); // Really quickly moves player in direction of inputs, basically like velocity 
        //rb.MovePosition(movementInput); // Moves player in direction of input, when input released, resets back to starting point

        //transform.Translate(Vector3.forward * moveHorizontal); // Quickly moves player along x axis, reset to 0, because of Vector3
        //transform.Translate(Vector3.right * moveVertical); // Quickly moves player along z axis, reset to 0, because of Vector3
        //transform.Translate(movementInput); // Quickly moves player in x and z axis directions
        //Vector3 forwardBackwardMovement = transform.forward * moveVertical * movementSpeed;
        //Vector3 leftRightMovement = transform.right * moveHorizontal * movementSpeed;


        // Below code in block comment is meant to utilize kinematic to better imitable movement in real life
        /*
        if ((Input.GetAxis("Horizontal") > 0.5 && Input.GetAxis("Vertical") > 0.5) || (Input.GetAxis("Horizontal") > -0.5 && Input.GetAxis("Vertical") > -0.5))
        //if ((Input.GetAxis("Horizontal") > 0.5 && Input.GetAxis("Vertical") > 0.5) || (Input.GetAxis("Horizontal") < -0.5 && Input.GetAxis("Vertical") < -0.5))
        {
            rb.isKinematic = false;
        }
        else if ((Input.GetAxis("Horizontal") < 0.5 && Input.GetAxis("Vertical") < 0.5) || (Input.GetAxis("Horizontal") < -0.5 && Input.GetAxis("Vertical") < -0.5))
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
        */

        // Player Look

        float lookHorizontal = Input.GetAxis("Mouse X"); // Mouse Left Right
        float lookVertical = Input.GetAxis("Mouse Y"); // Mouse Up Down
        float lookLean = Input.GetAxis("Lean"); // QE or RB LB
        float lookHorizontalXbox = Input.GetAxis("RightStickH"); // RS H
        float lookVerticalXbox = Input.GetAxis("RightStickV"); // RS V

        Vector3 lookInput = new Vector3(-lookVertical, lookHorizontal, 0.0F);
        Vector3 lookInputXbox = new Vector3(-lookVerticalXbox, lookHorizontalXbox, 0.0F);
        Vector3 leanRotationInput = new Vector3(0.0F, 0.0F, -lookLean);
        Vector3 leanPositionInput = new Vector3(lookLean, 0.0F, 0.0F);
        //transform.Rotate(-transform.right * lookVertical); // Rotates Torso up down
        transform.Rotate(Vector3.up * lookHorizontal); // rotates Torso left right--Used for Looking Left Right
        transform.Rotate(Vector3.up * lookHorizontalXbox); // Xbox Version of Above Code
        //transform.Rotate(Vector3.forward * -lookLean); //  rotates Torso in lean left right, on z axis -- lookLean must be negative
        //Quaternion leanRotation = Quaternion.Euler(leanInput);
        //rb.MoveRotation(leanRotation);// Lean(rotate) torso and then reset to original position
        //rb.MovePosition(leanPosInput); // Lean(move) the torso and then reset to original position 
        //transform.Translate(leanPosInput);

        // Below if statements are meant to rotate player, currently handled by animation so though evaluating to true its commented out code will not function
        if ((Input.GetAxis("Lean") > 0) && !(Input.GetAxis("Lean") < 0))
        {
            //transform.Rotate(Vector3.forward *Time.deltaTime);
        }

        if (Input.GetAxis("Lean") < 0 && !(Input.GetAxis("Lean") > 0))
        {
            //transform.Rotate(Vector3.forward * Time.deltaTime);
        }

        playerHeadObject.transform.Rotate(Vector3.left * lookVertical); // Rotates Head up down
        playerHeadObject.transform.Rotate(Vector3.left * lookVerticalXbox); // Xbox Version of Above Code
        //playerHeadObject.transform.Rotate(transform.up * lookHorizontal); // Rotates  Head left right
        //playerHeadObject.transform.rotation.y = transform.Rotate(transform.up * lookHorizontal);
        //transform.Rotate(transform.up * lookVertical);

        // Below Code keeps Player head tilt locked between 90 degree verticle
        xAxisClamp += lookVertical;
        //zAxisClamp += lookLean; // This will move body, even if no rotate functions are called-----Check if this is enabled if still leaning without animation, check off if you want old lean
    
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
        if (xAxisClampXbox > 90.0F)
        {
            xAxisClampXbox = 90.0F;
            lookVerticalXbox = 0.0F;
            ClampXAxisRotationToValue(270.0F);

        }
        else if (xAxisClampXbox < -90.0F)
        {
            xAxisClampXbox = -90.0F;
            lookVerticalXbox = 0.0F;
            ClampXAxisRotationToValue(90.0F);
        }
        if (zAxisClamp > 15.0F)
        {
            zAxisClamp = 15.0F;
            lookLean = 0.0F;
            ClampZAxisRotationToValue(-15.0F);
        }
        else if (zAxisClamp < -15.0F)
        {
            zAxisClamp = -15.0F;
            lookLean = 0.0F;
            ClampZAxisRotationToValue(15.0F);
        }
        //*

        // Weapon Switch
        if (indexSwitchWeapon == 1 && consoleTextDisplayed == false)
        {
            // Console Alert: Rifle Selected
            Debug.Log("Rifle Selected");
            // Set Pistol and Grenade to False, Rifle to True
            rifle.SetActive(true);
            pistol.SetActive(false);
            grenade.SetActive(false);
            consoleTextDisplayed = true; // Weapon selected has been changed so, Console has displayed text
        }

        if (indexSwitchWeapon == 2 && consoleTextDisplayed == false)
        {
            // Console Alert: Pistol Selected
            Debug.Log("Pistol Selected");
            // Set Rifle & Grenade to False, Pistol to True
            rifle.SetActive(false);
            pistol.SetActive(true);
            grenade.SetActive(false);
            consoleTextDisplayed = true; // Weapon selected has been changed so, Console has displayed text
        }
        if (indexSwitchWeapon == 3 && consoleTextDisplayed == false)
        {
            // Console Alert: Grenade Selected
            Debug.Log("Grenade Selected");
            // Set Rifle & Pistol to False, Grenade to True
            rifle.SetActive(false);
            pistol.SetActive(false);
            grenade.SetActive(true);
            consoleTextDisplayed = true; // Weapon selected has been changed so, Console has displayed text
        }
        if (indexSwitchWeapon > 3 && consoleTextDisplayed == false)
        {
            // Console Alert: Weapon Selected Reset
            Debug.Log("Weapon Selected Reset");
            indexSwitchWeapon = 1;
            consoleTextDisplayed = false; // Weapon selected has been changed so, Console has displayed text
        }
        if (Input.GetButtonDown("SwitchWeapon"))
        {
            // Console Alert: Weapon Switch Button Pressed
            Debug.Log("Weapon Switch Button Pressed");
            indexSwitchWeapon++;
            consoleTextDisplayed = false; // Weapon selected has been changed so, Console has displayed text
        }
        //*/


    }
    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = playerHeadObject.transform.eulerAngles;
        eulerRotation.x = value;
        playerHeadObject.transform.eulerAngles = eulerRotation;
    }
    private void ClampZAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.z = value;
        transform.eulerAngles = eulerRotation;
    }

    public void AmmoTextUpdate(string text)
    {
        ammoText.text = text;

    }

    /*
  Vector3 targetDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
   Camera.main.transform.TransformDirection(targetDirection);
  targetDirection.y = 0.0f;
  rb.velocity = targetDirection * movementSpeed;
  */
    /*
    void Shoot()
    {
        Camera fpsCam;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100F)) { }
    }
    */
}
