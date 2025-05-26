using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;

    [SerializeField] private Bag order1, order2, order3;
    [HideInInspector] public bool isDone, isPlayerInRange, isBagSelected;
    [SerializeField] public Bag currentBag;
    [SerializeField] private int locCurrent, locSelected;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDone = false;
        isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckOrder() && !isDone)
        {
            DeactivateOutlines();

            order1.itemInside.SetActive(false);
            order2.itemInside.SetActive(false);
            order3.itemInside.SetActive(false);

            MasterManager.instance.AnswerSFX(true);
            MasterManager.instance.CompletedGame();
            isDone = true;
        }

        if (!isPlayerInRange)
        {
            DeactivateOutlines();
        }
    }

    public void SelectBag(int position)
    {
        locCurrent = position;

        switch (position)
        {
            case 0:
                currentBag = order1; break;
            case 1:
                currentBag = order2; break;
            case 2:
                currentBag = order3; break;
            default:
                currentBag = order1; break;
        }

        currentBag.gameObject.GetComponent<Outline>().enabled = true;
        isBagSelected = true;
    }

    public void DeselectBag()
    {
        currentBag.gameObject.GetComponent<Outline>().enabled = false;
        currentBag = null;
        isBagSelected = false;
    }

    public void MoveBag(int position)
    {
        locSelected = position;

        Bag bagSelected = currentBag;
        Bag bagToSwitch;
        Vector3 tempPos;
        Quaternion tempRotation;
        int tempOrder;

        switch (position)
        {
            case 0:
                bagToSwitch = order1; break;
            case 1:
                bagToSwitch = order2; break;
            case 2:
                bagToSwitch = order3; break;
            default:
                bagToSwitch = order1; break;
        }

        tempPos = bagToSwitch.transform.position;
        tempOrder = bagToSwitch.order;
        tempRotation = bagToSwitch.transform.rotation;

        bagToSwitch.transform.position = bagSelected.transform.position;
        bagSelected.transform.position = tempPos;

        bagToSwitch.transform.rotation = bagSelected.transform.rotation;
        bagSelected.transform.rotation = tempRotation;

        bagToSwitch.order = bagSelected.order;
        bagSelected.order = tempOrder;

        switch (locCurrent)
        {
            case 0:
                order1 = bagToSwitch; break;
            case 1:
                order2 = bagToSwitch; break;
            case 2:
                order3 = bagToSwitch; break;
            default:
                order1 = bagToSwitch; break;
        }

        switch (locSelected)
        {
            case 0:
                order1 = bagSelected; break;
            case 1:
                order2 = bagSelected; break;
            case 2:
                order3 = bagSelected; break;
            default:
                order1 = bagSelected; break;
        }

        locCurrent = 0;
        locSelected = 0;

        DeselectBag();
        CheckOrder();
    }

    private bool CheckOrder()
    {
        if (order1.content == Content.Apples && order2.content == Content.Oranges && order3.content == Content.Both)
            return true;
        return false;
    }

    private void DeactivateOutlines()
    {
        Outline[] outlines = GetComponentsInChildren<Outline>();
        foreach (Outline outline in outlines) { outline.enabled = false; }
    }
}
