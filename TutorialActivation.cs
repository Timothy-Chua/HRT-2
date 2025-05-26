using UnityEngine;

public class TutorialActivation : MonoBehaviour
{
    [SerializeField] TutorialPerson tutorialPerson;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialPerson = GetComponentInChildren<TutorialPerson>();
        GetComponentInChildren<Outline>().enabled = false;
        tutorialPerson.enabled = false;
    }

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.CompareTag("Player"))
        {
            Debug.Log("In Range");
            tutorialPerson.enabled = true;
            tutorialPerson.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.CompareTag("Player"))
        {
            Debug.Log("Out Range");
            tutorialPerson.enabled = false;
            tutorialPerson.isPlayerInRange = true;
            GetComponentInChildren<Outline>().enabled = false;
        }
    }
}
