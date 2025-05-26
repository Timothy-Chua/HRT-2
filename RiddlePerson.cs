using UnityEngine;

[RequireComponent(typeof(Outline))]
public class RiddlePerson : MonoBehaviour
{
    private Outline outline;
    [HideInInspector] public bool isDone, isPlayerInRange;
    [SerializeField] private string txtQuestion;
    [SerializeField] private string[] txtAnswer;

    private void Awake()
    {
        isDone = false;
        isPlayerInRange = false;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*isDone = false;
        isPlayerInRange = false;
        outline = GetComponent<Outline>();
        outline.enabled = false;*/
    }

    private void OnMouseOver()
    {
        if (this.enabled)
        {
            if (!isDone && this.isPlayerInRange && !outline.enabled && MasterManager.instance.gameState == GameState.Play && !PigpenManager.instance.isDone)
            {
                outline.enabled = true;
            }
        }
    }

    private void OnMouseExit()
    {
        if (this.enabled)
        {
            if (!isDone && this.isPlayerInRange && MasterManager.instance.gameState == GameState.Play && !PigpenManager.instance.isDone)
            {
                outline.enabled = false;
            }
        }
    }

    private void OnMouseUp()
    {
        if (this.enabled)
        {
            if (!isDone && this.isPlayerInRange && MasterManager.instance.gameState == GameState.Play && !PigpenManager.instance.isDone)
            {
                this.AskQuestion();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && isPlayerInRange && !isDone && !PigpenManager.instance.isDone)
            this.SubmitAnswer();
    }

    private void AskQuestion()
    {
        MasterManager.instance.EnterDialogue(txtQuestion, true);
        MasterManager.instance.answerField.gameObject.SetActive(true);
    }

    public bool CheckAnswer()
    {
        string trueAnswer = MasterManager.instance.answerField.text.ToLower();
        Debug.Log("Answer: " + trueAnswer);

        foreach (string answer in txtAnswer) 
        {
            Debug.Log(answer);
            if (trueAnswer == answer)
                return true;
        }
        return false;
    }

    public void SubmitAnswer()
    {
        MasterManager.instance.AnswerSFX(this.CheckAnswer());

        if (this.CheckAnswer())
        {
            isDone = true;
            //GetComponentInParent<RiddleActivation>().enabled = false;
            PigpenManager.instance.RiddleCompleted();
            MasterManager.instance.CompletedGame();
            outline.enabled = false;
            isPlayerInRange = false;        }
        else
        {
            MasterManager.instance.ExitDialogue();
        }
    }
}
