using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    AllObjectInformation thisObjectStateInformation;
    Transform target;
    NavMeshAgent agent;
    // Transform targetForUnpausingMovement;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (target != null)
        {
            if (!StateExtensions.CanMove(thisObjectStateInformation.currentStatuses)) StopCharacter();
            if (thisObjectStateInformation == null || StateExtensions.CanMove(thisObjectStateInformation.currentStatuses))
            {
                MoveToPoint(target.position);
                FaceTarget();
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    public void GotToPoint(Vector3 point)
    {
        //if (transform.position == point)
        //    agent.ResetPath();
    }
    public void FollowTarget(AllObjectInformation ThisObjectStateInformation, GameObject newTarget, float Range = 0f)
    {
        thisObjectStateInformation = ThisObjectStateInformation;
        target = newTarget.transform;
        Interactable NewTargetInteractable = newTarget.GetComponent<Interactable>();
        if (Range > 0) { if (NewTargetInteractable.isItem) { return; } else { agent.stoppingDistance = Range; } }
        else
        {
            NewCharacterStats MyRange = GetComponent<NewCharacterStats>();
            if (NewTargetInteractable.isItem) agent.stoppingDistance = NewTargetInteractable.item.pickUpRadius;
            else agent.stoppingDistance = MyRange.objectInformation.WeaponRange.GetValue();
        }
    }
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        thisObjectStateInformation = null;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    public void StopCharacter()
    {
        agent.ResetPath(); // Stop the character's movement
    }
    // public void StopCharacterWithIntentOfHavingItBack()
    // {
    //     targetForUnpausingMovement = target;
    //     target = null;
    //     agent.ResetPath();
    // }
    // public void GivingTheMovementBackToACharacter()
    // {
    //     target = targetForUnpausingMovement;
    // }
}
