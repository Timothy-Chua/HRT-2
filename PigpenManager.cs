using UnityEngine;

public class PigpenManager : MonoBehaviour
{
    public static PigpenManager instance;
    [HideInInspector] public int gamesCompleted;
    [HideInInspector] public bool isDone;
    [SerializeField] private int gamesToComplete = 2;

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
        gamesCompleted = 0;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamesCompleted == gamesToComplete && !isDone)
        {
            MasterManager.instance.CompletedGame();
            isDone = true;
        }
    }

    public void RiddleCompleted()
    {
        gamesCompleted++;

        MasterManager.instance.ExitDialogue();
        if (gamesCompleted == gamesToComplete)
        {
            isDone = true;
        }
    }
}
