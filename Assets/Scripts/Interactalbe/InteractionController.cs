using System.Collections.Generic;
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
    public List<IInteractable> currentTargetedInteractable = new List<IInteractable> {};

    private int InteractableIndex = 0;

    

    private void OnScrollInteractables() 
    {

        
        var OldInteractableIndex = currentTargetedInteractable[0];
        
        currentTargetedInteractable.RemoveAt(InteractableIndex);
        currentTargetedInteractable.Add(OldInteractableIndex);

        UpdateInteractionText();

    }
   


    public void UpdateInteractionText()
    {
        Debug.Log(currentTargetedInteractable.Count);
        if (currentTargetedInteractable.Count <= 0)
        { 
            interactionText.text = string.Empty;
            return;
        }
        interactionText.text = currentTargetedInteractable[InteractableIndex].InteractMessage;
    }

    void OnInteractKey()
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