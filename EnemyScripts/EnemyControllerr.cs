using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerr : Interactable
{
    public float lookRadius = 7f;
    public Transform target = null;
    public List<GameObject> targetList = new List<GameObject>();
    public NewCharacterStats targetThatEnteredRange = null;
    public SphereColliderOnEnemy enterRangeColliderObject;
    public SphereColliderOnEnemy exitRangeColliderObject;
    // public Interactable focus;
    PlayerMotor motor;
    void Start()
    {
        enterRangeColliderObject.onTriggerEnter.AddListener(OnTriggerEnterrr);
        exitRangeColliderObject.onTriggerExit.AddListener(OnTriggerExittt);
        //colliderObject.onTriggerStay.AddListener(OnTriggerStayyy);
        motor = this.GetComponent<PlayerMotor>();
        objectInformation = GetComponent<NewCharacterStats>().objectInformation;
    }
    public override void Update()
    {
        // If getting hit by someone outside of the list, go to its location
        if(targetList.Count > 0)
        {
            target = targetList[0].transform;
            float distance = Vector3.Distance(target.position, transform.position);
            SetFocus(target.gameObject);
            // if (distance <= lookRadius)
            // {
            //     SetFocus(target.gameObject);
            // }
        }
        else 
        { 
            target = null;
            RemoveFocus();
        }
    }
    void SetFocus(GameObject newFocus)
    {
        Transform newFocusInteractable = newFocus.transform;
        // Interactable newFocusInteractable = newFocus.GetComponent<Interactable>();
        if (newFocus != focus)
        {
            if (focus !=null)
            {
                OnDefocused();
            }
            focus = newFocusInteractable;
            motor.FollowTarget(this.GetComponent<NewCharacterStats>().objectInformation,newFocus);
        }
        OnFocused(newFocus);
    }
    void RemoveFocus()
    {
        if(focus !=null)
        {
            OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
    void FaceTarget()
    {
        if (target !=null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
    private void OnTriggerEnterrr(Collider targetsEnteringRange) 
    {
        if (targetsEnteringRange.tag == "Player")
        {
            targetThatEnteredRange = targetsEnteringRange?.GetComponent<NewCharacterStats>();
            if (!targetList.Contains(targetsEnteringRange.gameObject))
                targetList.Add(targetsEnteringRange.gameObject);
        }
    }
    // private void OnTriggerStayyy(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         if (!targetList.Contains(other.gameObject))
    //             targetList.Add(other.gameObject);
    //     }
    // }
    private void OnTriggerExittt(Collider targetsExitingRange)
    {
        if (targetsExitingRange.tag == "Player")
        {
            if (targetList.Contains(targetsExitingRange.gameObject))
                targetList.Remove(targetsExitingRange.gameObject);
            if  (targetsExitingRange.transform == target)
            {
                for (int i = 0; i < targetList.Count; i++) 
                {
                    GameObject temp = targetList[i];
                    int randomIndex = Random.Range(i, targetList.Count);
                    targetList[i] = targetList[randomIndex];
                    targetList[randomIndex] = temp;
                }
            }
        }
    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}