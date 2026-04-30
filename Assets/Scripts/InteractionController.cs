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

    private IInteractable currentTargetedInteractable;

    public void Update()
    {
        UpdateCurrentInteractable();

        UpdateInteractionText();

        CheckForInteractionInput();
    }

    void UpdateCurrentInteractable()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));

        Physics.Raycast(ray, out var hit , interactionDistance);

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
