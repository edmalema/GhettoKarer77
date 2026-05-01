using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    TMPro.TextMeshProUGUI interactionText;

    [SerializeField]
    float interactionDistance = 5f;

    [SerializeField]
    LayerMask interactableLayer;

    private IInteractable currentTargetedInteractable;

    public void Update()
    {
        UpdateCurrentInteractable();

        UpdateInteractionText();

        CheckForInteractionInput();
    }

    void UpdateCurrentInteractable()
    {
        var ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out var hit , interactionDistance, interactableLayer);

        currentTargetedInteractable = hit.collider?.GetComponent<IInteractable>();
    }

    void UpdateInteractionText()
    {
        if (currentTargetedInteractable == null)

        { 
            interactionText.text = string.Empty;
            return;
        }
        interactionText.text = currentTargetedInteractable.InteractMessage;
    }

    void CheckForInteractionInput()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && currentTargetedInteractable != null)
        {
            currentTargetedInteractable.Interact();
        }
    }

}
