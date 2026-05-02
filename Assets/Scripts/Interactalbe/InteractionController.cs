using UnityEngine;
using UnityEngine.InputSystem;

public class InteracionController : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    TMPro.TextMeshProUGUI interactionText;

    [SerializeField]
    float interactionDistance = 5f;

    [SerializeField]
    LayerMask interactableLayer;

    public IInteractable[] currentTargetedInteractable;

    private int InteractableIndex = 0;

    public void Update()
    {



        CheckForInteractionInput();
    }


    public void UpdateInteractionText()
    {
        if (currentTargetedInteractable[InteractableIndex] == null)
        { 
            interactionText.text = string.Empty;
            return;
        }
        interactionText.text = currentTargetedInteractable[InteractableIndex].InteractMessage;
    }

    void CheckForInteractionInput()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && currentTargetedInteractable[InteractableIndex] != null)
        {
            currentTargetedInteractable[InteractableIndex].Interact();
        }
    }

}



/*
    void UpdateCurrentInteractable()
    {
        var ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out var hit , interactionDistance, interactableLayer);

        currentTargetedInteractable = hit.collider?.GetComponent<IInteractable>();
    }
*/