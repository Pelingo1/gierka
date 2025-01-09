using UnityEngine;
using UnityEngine.InputSystem;
using System;


[RequireComponent(typeof(Player))]
public class PlayerCrouch : MonoBehaviour
{
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] float crouchTransitionSpeed = 10f;

    public Player player;
    PlayerInput playerInput;
    InputAction crouchAction;

    Vector3 initialCameraPosition;
    float currentHeight;
    float standingHeight;

    bool isCrouching => standingHeight - currentHeight > .1f;
    bool wasCrouching = false;

    void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        crouchAction = playerInput.actions["crouch"];
    }

    void Start()
    {
        initialCameraPosition = player.cameraTransform.localPosition;
        standingHeight = currentHeight = player.Height;
    }

    void OnEnable() => player.OnBeforeMove += OnBeforeMove;
    void OnDisable() => player.OnBeforeMove -= OnBeforeMove;

    void OnBeforeMove()
    {
        var isTryingToCrouch = crouchAction.ReadValue<float>() > 0;
        var heightTarget = isTryingToCrouch ? crouchHeight : standingHeight;

        if(isCrouching && !isTryingToCrouch)
        {
            var castOrigin = transform.position + new Vector3(0, currentHeight / 2, 0);
            if(Physics.Raycast(castOrigin,Vector3.up,out RaycastHit hit, 0.2f))
            {
                var distanceToCeiling = hit.point.y - castOrigin.y;
                heightTarget = Mathf.Max
                (
                    currentHeight + distanceToCeiling - 0.1f,
                    crouchHeight
                ); 
            }
        }

        if(!Mathf.Approximately(heightTarget,currentHeight))
        {
            var crouchDelta = Time.deltaTime * crouchTransitionSpeed;
            currentHeight = Mathf.Lerp(currentHeight, heightTarget, crouchDelta);

            var halfHeightDifference = new Vector3(0, (standingHeight - currentHeight) / 2, 0);
            var newCameraPosition = initialCameraPosition - halfHeightDifference;

            player.cameraTransform.localPosition = newCameraPosition;
            player.Height = heightTarget;

            if(isTryingToCrouch && !wasCrouching)
            {
                player.visibility -= 0.2f;
                wasCrouching = true;
            }
            else if(!isTryingToCrouch && wasCrouching)
            {
                player.visibility += 0.2f;
                wasCrouching = false;
            }
        }
    }
}
