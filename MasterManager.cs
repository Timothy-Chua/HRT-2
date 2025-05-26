using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
    public static MasterManager instance;

    public int puzzlesToComplete = 5;
    public GameState gameState;
    private int puzzlesLeft;

    [Header("Audio")]
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip sfxCorrect, sfxWrong;

    [Header("UI")]
    [SerializeField] private GameObject panelGame;
    [SerializeField] private GameObject panelPause, panelEnd, panelDialogue;
    [SerializeField] private TMP_Text txtPuzzlesLeft, txtDialogue;
    [SerializeField] private GameObject[] panelTutorial;
    public TMP_InputField answerField;

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
        puzzlesLeft = puzzlesToComplete;
        txtDialogue.text = null;
        answerField.gameObject.SetActive(false);
        panelDialogue.SetActive(false);
        foreach (GameObject panel in panelTutorial) { panel.SetActive(false); }
        _BtnResume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            switch (gameState)
            {
                case GameState.Paused:
                    _BtnResume();
                    break;
                case GameState.Play:
                    _BtnPause();
                    break;
                case GameState.Dialogue:
                    ExitDialogue();
                    break;
            }
        }
    }

    public void _BtnResume()
    {
        gameState = GameState.Play;

        panelPause.SetActive(false);
        panelGame.SetActive(true);
        panelEnd.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void _BtnPause()
    {
        gameState = GameState.Paused;

        panelPause.SetActive(true);
        panelGame.SetActive(false);
        panelEnd.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void _BtnMenu()
    {
        SceneManager.LoadScene("MAIN MENU");
    }

    public void _BtnEnd()
    {
        gameState = GameState.End;

        panelPause.SetActive(false);
        panelGame.SetActive(false);
        panelEnd.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CompletedGame()
    {
        puzzlesLeft--;

        if (puzzlesLeft == 0)
            _BtnEnd();

        txtPuzzlesLeft.text = puzzlesLeft.ToString();
    }

    public void EnterDialogue(string text, bool isQuestion)
    {
        gameState = GameState.Dialogue;
        panelDialogue.SetActive(true);

        if (isQuestion)
            answerField.gameObject.SetActive(true);
        txtDialogue.text = text;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitDialogue()
    {
        gameState = GameState.Play;
        panelDialogue.SetActive(false);

        answerField.text = null;
        answerField.gameObject.SetActive(false);
        txtDialogue.text = null;
        foreach (GameObject panel in panelTutorial) { panel.SetActive(false); }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EnterTutorial(GameObject panel)
    {
        gameState = GameState.Dialogue;
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void AnswerSFX(bool isCorrect)
    {
        if (isCorrect)
            sfxPlayer.PlayOneShot(sfxCorrect);
        else
            sfxPlayer.PlayOneShot(sfxWrong);
    }
}

public enum GameState
{
    Play, Paused, Dialogue, End
}
