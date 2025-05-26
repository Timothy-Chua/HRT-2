using NUnit.Framework;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HanoiManager : MonoBehaviour
{
    public static HanoiManager instance;

    [SerializeField] private List<HanoiDisc> pole1, pole2, pole3;
    [SerializeField] Transform pos1, pos2, pos3;
    [HideInInspector] public bool isDiscSelected;
    [HideInInspector] public List<HanoiDisc> currentList;
    [HideInInspector] public bool isDone, isPlayerInRange;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDiscSelected = false;
        currentList = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPole() && !isDone)
        {
            DeactivateOutlines();
            MasterManager.instance.AnswerSFX(true);
            MasterManager.instance.CompletedGame();
            isDone = true;
        }

        if (!isPlayerInRange)
        {
            DeactivateOutlines();
        }
    }

    public void SelectDisc(int currentPoleNum)
    {
        switch (currentPoleNum)
        {
            case 0:
                currentList = pole1; break;
            case 1:
                currentList = pole2; break;
            case 2:
                currentList = pole3; break;
            default:
                currentList = pole1; break;
        }

        foreach (HanoiDisc disc in currentList)
            disc.GetComponent<Outline>().enabled = false;

        currentList[0].gameObject.GetComponent<Outline>().enabled = true;
        isDiscSelected = true;
    }

    public void DeselectDisc()
    {
        currentList[0].gameObject.GetComponent<Outline>().enabled = false;
        currentList = null;
        isDiscSelected = false;
    }

    public void MoveDisc(int targetPole)
    {
        List<HanoiDisc> sourceList = currentList, targetList;
        Transform targetPos;

        switch (targetPole)
        {
            case 0:
                targetList = pole1;
                targetPos = pos1; break;
            case 1:
                targetList = pole2;
                targetPos = pos2; break;
            case 2:
                targetList = pole3;
                targetPos = pos3; break;
            default:
                targetList = pole1;
                targetPos = pos1; break;
        }

        if (targetList.Count == 0)
        {
            sourceList[0].transform.position = targetPos.position;

            targetList.Add(sourceList[0]);

            targetList[0].currentPole = targetPole;
            targetList[0].gameObject.GetComponent<Outline>().enabled = false;

            sourceList.RemoveAt(0);

            currentList = null;
            isDiscSelected = false;
        }
        else
        {
            if (sourceList[0].discNum > targetList[0].discNum)
            {
                MasterManager.instance.AnswerSFX(false);
            }
            else
            {
                sourceList[0].transform.position = targetPos.position;

                targetList.Insert(0, sourceList[0]);

                targetList[0].currentPole = targetPole;
                targetList[0].gameObject.GetComponent<Outline>().enabled = false;

                sourceList.RemoveAt(0);

                currentList = null;
                isDiscSelected = false;
            }
        }
    }

    private bool CheckPole()
    {
        if (pole3.Count == 5)
        {
            int prevDisc = -1;
            foreach (HanoiDisc disc in pole3)
            {
                // 0 -> 1 -> 2 -> 3 -> 4
                if (disc.discNum < prevDisc)
                    return false;
                prevDisc = disc.discNum;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DeactivateOutlines()
    {
        Outline[] outlines = GetComponentsInChildren<Outline>();
        foreach (Outline outline in outlines) { outline.enabled = false; }
    }
}
