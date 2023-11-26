using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public List<Transform> RandomWayPoints;
    public NavMeshAgent AIAgent;
    int currentWIndex;
    public Animator anim;
        
    void Start()
    {

        RandomWay();
    }
    private void Update()
    {
        if (currentWIndex != 0 && Vector3.Distance(transform.position, RandomWayPoints[currentWIndex].transform.position) <= 1f)
        {
            RandomWay();
        }
        anim.SetFloat("Speed",AIAgent.velocity.magnitude);
    }
    void RandomWay()
    {
        currentWIndex = RandomNum();
        AIAgent.SetDestination(RandomWayPoints[currentWIndex].transform.position);
    }

    int RandomNum()
    {
        int num = Random.Range(0,RandomWayPoints.Count - 1);
        return num;
    }
}
