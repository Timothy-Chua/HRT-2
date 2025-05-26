using UnityEngine;

public class BagActivation : MonoBehaviour
{
    private void Start()
    {
        BagManager.instance.isPlayerInRange = false;
    }

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.CompareTag("Player") && !BagManager.instance.isDone)
        {
            BagManager.instance.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.CompareTag("Player"))
        {
            BagManager.instance.isBagSelected = false;
            BagManager.instance.currentBag = null;

            BagManager.instance.isPlayerInRange = false;
        }
    }
}
