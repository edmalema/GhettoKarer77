using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionRange : MonoBehaviour
{
    private void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<InteracionController>() == null) return;

        InteracionController InteractionObj = other.GetComponentInParent<InteracionController>();


        InteractionObj.currentTargetedInteractable.Add(GetComponent<IInteractable>());
        InteractionObj.UpdateInteractionText();
    }
        
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<InteracionController>() == null) return;

        InteracionController InteractionObj = other.GetComponentInParent<InteracionController>();


        InteractionObj.currentTargetedInteractable.Remove(GetComponent<IInteractable>());
        InteractionObj.UpdateInteractionText();
    }
}


