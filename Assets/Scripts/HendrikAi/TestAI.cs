using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : BaseAi
{
    // Start is called before the first frame update
    //Update is called once per frame
    
    [SerializeField] GameObject goal;
    public override void Start()
    {
        base.Start();
        onChase += testChase;

    }
    void Update()
    {
        Chase(goal.transform.position);
    }

    private void Patrol()
    {

    }

    private void Chase(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void RunAway(Vector3 destination)
    {
        Vector3 AwayVector = destination - this.transform.position;
        agent.SetDestination(this.transform.position - AwayVector);
    }
    void testChase()
    {

    }
}
