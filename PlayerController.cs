using System.Collections; //Contains classes that describe different types of objects
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] // Import 'Player Motor' script 
public class PlayerController : MonoBehaviour { // Make Filename a class using the MonoBehaviour default class

    // Public means it can be used in a variety of ways, not limited

    public Interactable focus; // Create a variable called focus that will be an Interactable (user can interact with) 

    public LayerMask movementMask; // Create a menu in Inspector called movement. 

    Camera cam; // Use Camera class and make it a variable called 'cam'
                // This will be the camera following the character
    PlayerMotor motor; // Using the PlayerMotor class (from when we imported),
                       // Declare it as the variable 'motor'
    // When user hits play
    void Start()
    {
        cam = Camera.main; // Declare that cam sets Camera class as the main camera throughout the program.
        motor = GetComponent<PlayerMotor>(); // Set motor variable equal to the class 'PlayerMotor'
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If the user presses left mouse button (0) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Create a ray set to the location the user clicked, store location in 'ray' var 
            RaycastHit hit; // Store data about the ray in the hit variable

            if (Physics.Raycast(ray, out hit, 100, movementMask)) // Using all variables that has to do with direction on click 
            {
                motor.MoveToPoint(hit.point); // Move our player to what we hit

                RemoveFocus(); // If player is already focused on an object
            }
        }

        if (Input.GetMouseButtonDown(1)) // If the user presses right mouse button (1) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Create a ray set to the location the user clicked, store location in 'ray' var
            RaycastHit hit; // Store data about the ray in the hit variable

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>(); // Change Interactable variable from focus to interactable
                                                                                       // if the ray hits and collides with a game object, get the Interactable components
                                                                                       // from interactable script
                if (interactable != null) // If object is interactable 
                {
                    SetFocus(interactable); // Focus on interactable
                }

            }
        }
    }

    void SetFocus(Interactable newFocus) // Change 'focus' from Interactable to 'newFocus'
    {
        if (newFocus != focus) // If player focuses on an object they are not focusing on
        {
            if (focus != null) // And if player is already focusing on an object already
                focus.OnDefocused(); // Defocuse from that object

            motor.FollowTarget(newFocus); // Go to the 'newFocus' point
            focus = newFocus; // replace old 'focus' point to 'newFocus' point
        }

        newFocus.OnFocused(transform); // Focus on new focus 

    }

    void RemoveFocus () // To remove focus point
    {
        if (focus != null) // If player is currently focused
            focus.OnDefocused(); // Dont focus
        focus = null; // There is nothing to focus on
        motor.StopFollowingTarget(); // access StopFollowingTarget() code from PlayerMotor.cs (Remove Target)
    }
}
