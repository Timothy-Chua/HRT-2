using UnityEngine;

public class HanoiActivationArea : MonoBehaviour
{
    private void Start()
    {
        HanoiManager.instance.isPlayerInRange = false;
    }

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.CompareTag("Player") && !HanoiManager.instance.isDone)
        {
            HanoiManager.instance.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.CompareTag("Player"))
        {
            HanoiManager.instance.isDiscSelected = false;
            HanoiManager.instance.currentList = null;

            HanoiManager.instance.isPlayerInRange = false;
        }
    }
}
