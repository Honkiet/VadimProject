using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BaseAi : MonoBehaviour
{

    protected NavMeshAgent agent;
    //remove later
    //rename Units and give each piece a team number or whatever
    protected List<Transform> units;
    protected Piece piece;

    protected UnityAction onChase;
    protected UnityAction onAttack;
    protected UnityAction onFlee;
    protected UnityAction onPatrol;
    protected Action<Vector3> onMove;

    //Transform enemyTransform;

    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        units = new List<Transform>();
        piece = this.GetComponent<Piece>();
        
    }

    protected List<Transform> GetUnitsList()
    {
        return units;
    }
    
    protected bool CanSee(GameObject obj)
    {
        // if the enemy is inside the distance and inside vision angle
        Vector3 distanceFromMeToObjectVector = obj.transform.position - this.transform.position;

        float angleToObject = Vector3.Angle(distanceFromMeToObjectVector, this.transform.forward);

        //if the enemy is not behind walls
        RaycastHit hitInfo;
        Vector3 rayToObj = obj.transform.position - this.transform.position;

       

        if (Physics.Raycast(this.transform.position, rayToObj, out hitInfo))
        {
            //To Do If a teammate is in front the ray hits it first
            if (hitInfo.transform.gameObject.tag == "piece" && distanceFromMeToObjectVector.magnitude < piece.GetVisionDistance() && angleToObject < piece.GetVisionAngle())
            { 
                //avoid tilt
                distanceFromMeToObjectVector.y = 0;
                return true;
            }

        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //add if not in the list already
        if (!units.Contains(other.gameObject.transform))
        {
            switch ((Layer)other.gameObject.layer)
            {
                case Layer.Enemy:
                    units.Add(other.transform);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //remove if in the list
        if (units.Contains(other.gameObject.transform))
        {
            switch ((Layer)other.gameObject.layer)
            {
                case Layer.Enemy:
                    units.Remove(other.transform);
                    break;
                default:
                    break;
            }
        }

       
    }

}

public enum Layer
{
    Enemy = 8
}
