using NUnit.Framework;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class StationManager : MonoBehaviour
{
    public static StationManager instance;
    [HideInInspector] public bool isDone, isPlayerInRange;
    private Outline outline;
    [SerializeField] private string[] stations;
    private int selectedStation;

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
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if (!isDone && isPlayerInRange && outline.enabled == false && MasterManager.instance.gameState == GameState.Play)
        {
            outline.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (!isDone && isPlayerInRange && MasterManager.instance.gameState == GameState.Play)
        {
            outline.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (!isDone && isPlayerInRange && MasterManager.instance.gameState == GameState.Play)
        {
            AskQuestion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && isPlayerInRange && !isDone)
            SubmitAnswer();
    }

    private void AskQuestion()
    {
        selectedStation = Random.Range(0, stations.Length);
        MasterManager.instance.EnterDialogue("In which position is " + stations[selectedStation] + " stationed?", true);
        MasterManager.instance.answerField.gameObject.SetActive(true);
        Debug.Log(selectedStation.ToString());
    }

    public bool CheckAnswer()
    {
        switch (selectedStation)
        {
            case 0:
                if (MasterManager.instance.answerField.text.ToLower() == "first" || MasterManager.instance.answerField.text == "1st" || MasterManager.instance.answerField.text == "1")
                    return true;
                return false;
            case 1:
                if (MasterManager.instance.answerField.text.ToLower() == "second" || MasterManager.instance.answerField.text == "2nd" || MasterManager.instance.answerField.text == "2")
                    return true;
                return false;
            case 2:
                if (MasterManager.instance.answerField.text.ToLower() == "third" || MasterManager.instance.answerField.text == "3rd" || MasterManager.instance.answerField.text == "3")
                    return true;
                return false;
            case 3:
                if (MasterManager.instance.answerField.text.ToLower() == "fourth" || MasterManager.instance.answerField.text == "4th" || MasterManager.instance.answerField.text == "4")
                    return true;
                return false;
            case 4:
                if (MasterManager.instance.answerField.text.ToLower() == "fifth" || MasterManager.instance.answerField.text == "5th" || MasterManager.instance.answerField.text == "5")
                    return true;
                return false;
            case 5:
                if (MasterManager.instance.answerField.text.ToLower() == "sixth" || MasterManager.instance.answerField.text == "6th" || MasterManager.instance.answerField.text == "6")
                    return true;
                return false;
            case 6:
                if (MasterManager.instance.answerField.text.ToLower() == "seventh" || MasterManager.instance.answerField.text == "7th" || MasterManager.instance.answerField.text == "7")
                    return true;
                return false;
            case 7:
                if (MasterManager.instance.answerField.text.ToLower() == "eight" || MasterManager.instance.answerField.text == "8th" || MasterManager.instance.answerField.text == "8")
                    return true;
                return false;
            case 8:
                if (MasterManager.instance.answerField.text.ToLower() == "ninth" || MasterManager.instance.answerField.text == "9th" || MasterManager.instance.answerField.text == "9")
                    return true;
                return false;
            case 9:
                if (MasterManager.instance.answerField.text.ToLower() == "tenth" || MasterManager.instance.answerField.text == "10th" || MasterManager.instance.answerField.text == "10")
                    return true;
                return false;
            case 10:
                if (MasterManager.instance.answerField.text.ToLower() == "eleventh" || MasterManager.instance.answerField.text == "11th" || MasterManager.instance.answerField.text == "11")
                    return true;
                return false;
            case 11:
                if (MasterManager.instance.answerField.text.ToLower() == "twelfth" || MasterManager.instance.answerField.text == "12th" || MasterManager.instance.answerField.text == "12")
                    return true;
                return false;
            case 12:
                if (MasterManager.instance.answerField.text.ToLower() == "thirteenth" || MasterManager.instance.answerField.text == "13th" || MasterManager.instance.answerField.text == "13")
                    return true;
                return false;
            default:
                return false;
        }
    }

    public void SubmitAnswer()
    {
        MasterManager.instance.AnswerSFX(CheckAnswer());

        if (CheckAnswer())
        {
            GetComponentInParent<StationActivation>().enabled = false;
            MasterManager.instance.ExitDialogue();
            MasterManager.instance.CompletedGame();
            isPlayerInRange = false;
            outline.enabled = false;
            isDone = true;
        }
        else
        {
            MasterManager.instance.ExitDialogue();
        }
    }
}
