using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionRange : MonoBehaviour
{
    [SerializeField]
    string tagFilter;

    private int onTriggerEnter = 1;

    private int onTriggerExit = -1;

    private int listOfEntetis = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.name.Contains(tagFilter)) return;

        listOfEntetis += onTriggerEnter;

    }
        
    private void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.name.Contains(tagFilter)) return;

        listOfEntetis += onTriggerExit;
    }
}
