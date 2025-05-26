using UnityEngine;

[System.Serializable]
public class HanoiDisc : MonoBehaviour
{
    public int discNum;
    public int currentPole;
    public GameObject discObj;

    private void Start()
    {
        discObj = gameObject;
    }

    private void OnMouseUp()
    {
        if (MasterManager.instance.gameState == GameState.Play && !HanoiManager.instance.isDone && HanoiManager.instance.isPlayerInRange)
        {
            if (HanoiManager.instance.isDiscSelected)
                HanoiManager.instance.DeselectDisc();
            else
                HanoiManager.instance.SelectDisc(currentPole);
        }
    }

    private void OnMouseOver()
    {
        if (MasterManager.instance.gameState == GameState.Play && !HanoiManager.instance.isDiscSelected && !HanoiManager.instance.isDone && HanoiManager.instance.isPlayerInRange)
            gameObject.GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        if (MasterManager.instance.gameState == GameState.Play && !HanoiManager.instance.isDiscSelected && !HanoiManager.instance.isDone && HanoiManager.instance.isPlayerInRange)
            gameObject.GetComponent<Outline>().enabled = false;
    }
}
