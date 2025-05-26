using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    [SerializeField] private int poleNum;

    private void OnMouseDown()
    {
        if (MasterManager.instance.gameState == GameState.Play && !HanoiManager.instance.isDone && HanoiManager.instance.isPlayerInRange)
        {
            if (HanoiManager.instance.isDiscSelected)
            {
                HanoiManager.instance.MoveDisc(poleNum);
            }
        }
    }

    private void OnMouseOver()
    {
        if (MasterManager.instance.gameState == GameState.Play && !HanoiManager.instance.isDone && HanoiManager.instance.isPlayerInRange)
            gameObject.GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        if (MasterManager.instance.gameState == GameState.Play && !HanoiManager.instance.isDone && HanoiManager.instance.isPlayerInRange)
            gameObject.GetComponent<Outline>().enabled = false;
    }
}
