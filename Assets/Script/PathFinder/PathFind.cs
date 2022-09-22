using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PathFind : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject pathfinderParticle;
    public Vector3 testPos;
    public int houseId;
    int segmentsToCreate;
    float distance;
    float lerpValue;
    Vector3 lerp;

    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        SceneManager.sceneLoaded += OnSceneLoaded;


    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CalculatePath();
    }
    public void CalculatePath(int houseId)
    {
        Vector3 positionToCalculate = new ();
        NavMeshPath path = new NavMeshPath();
        
        foreach (OrderHouse houseinfo in GameObject.FindObjectsOfType(typeof(OrderHouse)))
        {
            if (houseinfo.HouseId == houseId)
            {
                positionToCalculate = houseinfo.gameObject.transform.position;
                this.houseId = houseId;
            }
        }  
        NavMesh.SamplePosition(positionToCalculate, out NavMeshHit hit, 20f, 1);
        agent.CalculatePath(hit.position, path);
        GeneratePath(path.corners);
        //agent.enabled = false;
    }
    public void CalculatePath()
    {
        Vector3 positionToCalculate = new();
        NavMeshPath path = new NavMeshPath();

        foreach (OrderHouse houseinfo in GameObject.FindObjectsOfType(typeof(OrderHouse)))
        {
            if (houseinfo.HouseId == houseId)
            {
                positionToCalculate = houseinfo.gameObject.transform.position;
            }
        }
        NavMesh.SamplePosition(positionToCalculate, out NavMeshHit hit, 20f, 1);
        agent.CalculatePath(hit.position, path);
        GeneratePath(path.corners);
        //agent.enabled = false;
    }
    private void GeneratePath(Vector3[] corners)
    {

        for (int i = 0; i < corners.Length; i++)
        {
            if (i == 0)
            {
                segmentsToCreate = Mathf.RoundToInt(Vector3.Distance(transform.position, corners[0]) / 3f);
                Debug.Log(segmentsToCreate);
                lerp = Vector3.Lerp(transform.position, corners[0], segmentsToCreate / 2);
                lerpValue = 0;
                for (int j = 0; j < segmentsToCreate; j++)
                {
                    lerpValue += distance;
                    lerp = Vector3.Lerp(transform.position, corners[0], lerpValue);
                    Instantiate(pathfinderParticle, lerp, Quaternion.identity);
                }
            }
            else
            {
                lerpValue = 0;
                segmentsToCreate = Mathf.RoundToInt(Vector3.Distance(corners[i - 1], corners[i]) / 3f);
                Debug.Log(segmentsToCreate);
                distance = 1 / (float)segmentsToCreate;
                for (int j = 0; j < segmentsToCreate; j++)
                {
                    lerpValue += distance;
                    lerp = Vector3.Lerp(corners[i - 1], corners[i], lerpValue);
                    Instantiate(pathfinderParticle, lerp, Quaternion.identity);
                }
               
            }
        }
    }
}
