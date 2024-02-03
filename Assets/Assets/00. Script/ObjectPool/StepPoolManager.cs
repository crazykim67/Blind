using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StepPoolManager : MonoBehaviour
{
    #region Instance

    private static StepPoolManager instance;

    public static StepPoolManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new StepPoolManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    [SerializeField]
    private GameObject leftStep;
    [SerializeField]
    private GameObject rightStep;

    [SerializeField]
    private Transform playerTr;

    private Queue<Step> stepQueue = new Queue<Step>();

    [SerializeField]
    private int stepIndex = 0;

    private void Awake()
    {
        if(Instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        Initialize(10);
        playerTr = FindFirstObjectByType<PlayerController>().GetComponent<Transform>();
    }

    private void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++) 
            stepQueue.Enqueue(CreateNewObject());
    }

    private Step CreateNewObject()
    {
        Step step = null;

        if(stepIndex == 0)
        {
            step = Instantiate(leftStep).GetComponent<Step>();
            stepIndex = -1;
        }
        else
        {
            step = Instantiate(rightStep).GetComponent<Step>();
            stepIndex = 0;
        }

        step.StepDisable();
        step.transform.SetParent(this.transform);

        return step;
    }

    public Step GetStep()
    {
        Vector3 pos = playerTr.position;
        Quaternion rot = playerTr.rotation;

        if (stepQueue.Count > 0)
        {
            var step = stepQueue.Dequeue();
            step.transform.SetParent(null);
            step.transform.position = new Vector3(pos.x, 0, pos.z);
            step.transform.rotation = rot;

            step.gameObject.SetActive(true);

            step.OnStep();

            return step;
        }
        else
        {
            var newStep = Instance.CreateNewObject();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            return newStep;
        }
    }

    public void ReturnStep(Step step)
    {
        step.transform.SetParent(this.transform);
        stepQueue.Enqueue(step);
    }
}
