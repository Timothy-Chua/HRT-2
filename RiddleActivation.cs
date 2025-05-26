using UnityEngine;

public class RiddleActivation : MonoBehaviour
{
    [SerializeField] RiddlePerson riddlePerson;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        riddlePerson = GetComponentInChildren<RiddlePerson>();
        GetComponentInChildren<Outline>().enabled = false;
        riddlePerson.enabled = false;
    }

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.CompareTag("Player") && !riddlePerson.isDone && !PigpenManager.instance.isDone)
        {
            Debug.Log("In Range");
            riddlePerson.enabled = true;
            riddlePerson.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.CompareTag("Player") && !riddlePerson.isDone && !PigpenManager.instance.isDone)
        {
            Debug.Log("Out Range");
            riddlePerson.enabled = false;
            riddlePerson.isPlayerInRange = true;
            GetComponentInChildren<Outline>().enabled = false;
        }
    }
}
