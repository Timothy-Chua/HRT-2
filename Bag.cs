using UnityEngine;

[System.Serializable]
public class Bag : MonoBehaviour
{
    public int order;
    public Content content;
    public GameObject itemInside;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemInside.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (MasterManager.instance.gameState == GameState.Play && !BagManager.instance.isDone && BagManager.instance.isPlayerInRange)
        {
            itemInside.SetActive(true);
            itemInside.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (MasterManager.instance.gameState == GameState.Play && !BagManager.instance.isDone && BagManager.instance.isPlayerInRange)
        {
            itemInside.GetComponent<Outline>().enabled = false;
            itemInside.SetActive(false);
        }
    }

    private void OnMouseUp()
    {
        if (MasterManager.instance.gameState == GameState.Play && !BagManager.instance.isDone && BagManager.instance.isPlayerInRange)
        {
            if (BagManager.instance.isBagSelected)
            {
                BagManager.instance.MoveBag(order);
            }
            else
                BagManager.instance.SelectBag(order);
        }
    }
}

public enum Content
{
    Apples, Oranges, Both
}
