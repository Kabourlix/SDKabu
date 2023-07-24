using System;
using SDKabu.KCharacter.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable

namespace SDKabu.KCharacter
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera = null!;
        [SerializeField] private LayerMask movableLayers = 0;
        
        
        private PlayerInput? playerInput;
        private InputAction moveAction = null!;
        private InputAction movePositionAction = null!;

        
        private KTopDownNavComponent navComponent = null!;

        private void Awake()
        {
            navComponent = GetComponent<KTopDownNavComponent>();
        }

        private void OnEnable()
        {
            if(playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
                moveAction = playerInput.actions["Move"];
                movePositionAction = playerInput.actions["MovePos"];
                
                moveAction.performed += OnMove;
            }
            
            playerInput.actions.Enable();
        }

        private void OnDisable()
        {
            if(playerInput != null)
            {
                playerInput.actions.Disable();
            }
        }

        
    

        private void OnMove(InputAction.CallbackContext _obj)
        {
            var mousePos = movePositionAction.ReadValue<Vector2>();
            //Create a ray from the Mouse click position
            var ray = mainCamera.ScreenPointToRay(mousePos);
            //Raycast into the scene starting at the mouse click position
            if (Physics.Raycast(ray, out var hit, 100, movableLayers))
            {
                Debug.Log($"Hit {hit.collider.gameObject.name} at pos {hit.point}");
                navComponent.MoveTo(hit.point, true);
            }
        }
    }
}