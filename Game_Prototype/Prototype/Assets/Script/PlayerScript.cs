using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    private PlayerController input = null;
    public TMP_Text interactText; //player health text 
    private Vector2 moveVector; //vector 2 of player movement
    public Transform cameraTransform; //the maincamera
    public float moveSpeed = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerController(); //new instance of the new input system 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCancelled;

        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
    }

    private void Move()
    {
        //Debug.Log(moveVector);
        Vector3 moveDirection = cameraTransform.forward * moveVector.y + cameraTransform.right * moveVector.x; // Calculate the movement direction based on camera orientation
        moveDirection.y = 0; // freezes the y coordinate

        moveDirection.Normalize(); // Normalize the direction to prevent faster movement diagonally
        // rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime); //causes conflict when using with rb.addforce for jumping
        //rb.position += moveDirection * moveSpeed * Time.deltaTime; //its too laggy because of not using interpolation
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //Vector3 force = moveDirection * moveSpeed * rb.mass * Time.deltaTime;

        //rb.AddForce(force); //using rb.addforce here for movement because am also using it at the same time for jump
    }

    private void OnMovePerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        Debug.Log(moveVector);
        anim.SetFloat("hInput", moveVector.x);
        anim.SetFloat("yInput", moveVector.y);

    }

    private void OnMoveCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
        anim.SetFloat("hInput", moveVector.x);
        anim.SetFloat("yInput", moveVector.y);
    }

    void OnInteractPerformed(InputAction.CallbackContext value)
    {
        Debug.Log("successfully picked up object");
        interactText.enabled = false;
    }

    void OnInteractCancelled(InputAction.CallbackContext value)
    {
        //Debug.Log("successfully dropped object");
        //interactText.enabled = true;
    }
}
