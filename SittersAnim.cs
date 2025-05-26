using System.Collections;
using UnityEngine;

public class SittersAnim : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float idleTime = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AnimRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AnimRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(idleTime);
            animator.SetTrigger("SleepyTrigger");
        }
    }
}
