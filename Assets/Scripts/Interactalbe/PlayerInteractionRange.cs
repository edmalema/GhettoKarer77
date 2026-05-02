using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionRange : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("contactr");

        if (other.GetComponentInParent<InteracionController>() == null) return;

        InteracionController InteractionObj = other.GetComponentInParent<InteracionController>();

        InteractionObj.currentTargetedInteractable.Append(GetComponent<IInteractable>());
        InteractionObj.UpdateInteractionText();
    }
        
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<InteracionController>() == null) return;


    }
}


