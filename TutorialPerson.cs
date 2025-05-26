using UnityEngine;

[RequireComponent (typeof(Outline))]
public class TutorialPerson : MonoBehaviour
{
    private Outline outline;
    [HideInInspector] public bool isDone, isPlayerInRange;
    [SerializeField] private GameObject panelTutorial;

    private void Awake()
    {
        isDone = false;
        isPlayerInRange = false;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if (this.enabled)
        {
            if (!isDone && this.isPlayerInRange && !outline.enabled && MasterManager.instance.gameState == GameState.Play)
            {
                outline.enabled = true;
            }
        }
    }

    private void OnMouseExit()
    {
        if (this.enabled)
        {
            if (!isDone && this.isPlayerInRange && MasterManager.instance.gameState == GameState.Play)
            {
                outline.enabled = false;
            }
        }
    }

    private void OnMouseUp()
    {
        if (this.enabled)
        {
            if (!isDone && this.isPlayerInRange && MasterManager.instance.gameState == GameState.Play)
            {
                this.AskQuestion();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AskQuestion()
    {
        MasterManager.instance.EnterTutorial(panelTutorial);
    }
}
