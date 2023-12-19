using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 7f;
    public Transform target = null;
    public List<GameObject> targetList = new List<GameObject>();
    public NewCharacterStats targetGameObject = null;
    public SphereColliderOnEnemy colliderObject;
    public Interactable thisInteractable;
    public Interactable focus;
    PlayerMotor motor;
    void Start()
    {
        colliderObject.onTriggerEnter.AddListener(OnTriggerEnterrr);
        colliderObject.onTriggerExit.AddListener(OnTriggerExittt);
        //colliderObject.onTriggerStay.AddListener(OnTriggerStayyy);
        thisInteractable = GetComponent<Interactable>();
        motor = GetComponent<PlayerMotor>();
    }
    void Update()
    {
        // If getting hit by someone outside of the list, go to its location
        if(targetList.Count > 0)
        {
            target = targetList[0].transform;
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                SetFocus(target.gameObject);
            }
        }
        else 
        { 
            target = null;
            RemoveFocus();
        }
    }
    void SetFocus(GameObject newFocus)
    {
        Interactable newFocusInteractable = newFocus.GetComponent<Interactable>();
        if (newFocus != focus)
        {
            if (focus !=null)
            {
                thisInteractable.OnDefocused();
            }
            focus = newFocusInteractable;
            motor.FollowTarget(this.GetComponent<NewCharacterStats>().objectInformation,newFocus);
        }
        thisInteractable.OnFocused(newFocus);
    }
    void RemoveFocus()
    {
        if(focus !=null)
        {
            thisInteractable.OnDefocused();
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
            targetGameObject = targetsEnteringRange?.GetComponent<NewCharacterStats>();
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
