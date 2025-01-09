using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleraion = 20f;
    public Transform cameraTransform;
    public float visibility = 1f;

    public event Action OnBeforeMove;

    public float Height
    {
        get => controller.height;
        set => controller.height = value;
    }

    Vector2 look;
    internal Vector3 velocity;
    CharacterController controller;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateGravity();
        UpdateLook();
        UpdateMovement();
    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }

    Vector3 GetMovementInput()
    {
        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= movementSpeed;
        return input;
    }
    void UpdateMovement()
    {
        OnBeforeMove?.Invoke();

        var input = GetMovementInput();

        var factor = acceleraion * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }
}
