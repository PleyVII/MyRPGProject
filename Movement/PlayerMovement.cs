using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour
{
    Camera cam;
    PlayerMotor motor;
    public Interactable focus;
    public LayerMask movementMask;
    public LayerMask interactableMask;
    public Interactable thisInteractable;
    bool MouseHold;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        thisInteractable = GetComponent<Interactable>();
    }
    void Update()
    {
        // if (EventSystem.current.IsPointerOverGameObject())
        //     return;
        //motor.GotToPoint(new Vector3(Mathf.Round(hit.point.x * 2) / 2, hit.point.y, Mathf.Round(hit.point.z * 2) / 2));

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, movementMask))
            {
                motor.MoveToPoint(new Vector3(Mathf.Round(hit.point.x * 2) / 2, hit.point.y, Mathf.Round(hit.point.z * 2) / 2));
                RemoveFocus();
            }
            if (Physics.Raycast(ray, out hit, 100, interactableMask))
            {
                GameObject interactableObject = hit.collider.gameObject;
                if (interactableObject != this.gameObject && interactableObject != null) SetFocus(interactableObject);
            }
        }
    }
    void SetFocus(GameObject newFocus)
    {
        Interactable newFocusInteractable = newFocus.GetComponent<Interactable>();
        if (newFocus != focus)
        {
            if (focus != null)
            {
                thisInteractable.OnDefocused();
            }
            focus = newFocusInteractable;
            motor.FollowTarget(this.GetComponent<NewCharacterStats>().objectInformation, newFocus);
        }
        thisInteractable.OnFocused(newFocus);
    }
    public void RemoveFocus()
    {
        if (focus != null)
        {
            thisInteractable.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
    public RaycastHit RayHitOnMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100, movementMask);
        return hit;
    }
}
