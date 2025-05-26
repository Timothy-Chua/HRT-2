using UnityEngine;

public class StationActivation : MonoBehaviour
{
    private void OnTriggerEnter(Collider actor)
    {
        if (actor.CompareTag("Player") && !StationManager.instance.isDone)
        {
            StationManager.instance.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.CompareTag("Player") && !StationManager.instance.isDone)
        {
            StationManager.instance.isPlayerInRange = false;
            GetComponentInChildren<Outline>().enabled = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
    }
}
