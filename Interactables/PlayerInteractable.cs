using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerInteractable : Interactable
{
    public SkillBars skillBars;
    public float attackCooldown = 1;
    public float abilityCooldown = 1;
    public bool isDuringCastingOrCharging = false;
    public float castingOrChargingTimer = 10;
    PlayerMotor motor;
    public event System.Action OnAttack;
    public ActiveSkill skill;
    public KeyCode key;
    public bool UsingSkill = false;
    public bool SkillIsBeingUsed = false;
    public LayerMask movementMask;
    public LayerMask interactableMask;
    public GameObject targetToCastSkillOn = null;
    public Vector3 groundToCastSkillOn = Vector3.negativeInfinity;
    bool isGroundToCastSkillOnValid;
    public Dictionary<KeyCode, int> keyToSlotIndexMap;
    Camera cam;
    private void Awake()
    {
        isItem = false;
    }
    private void Start()
    {
        cam = Camera.main;
        objectInformation = GetComponent<NewCharacterStats>().objectInformation;
        motor = GetComponent<PlayerMotor>();
        isGroundToCastSkillOnValid = !groundToCastSkillOn.Equals(Vector3.negativeInfinity);
        InitializeKeyToSlotIndexMap();
    }
    private void InitializeKeyToSlotIndexMap()
    {
        keyToSlotIndexMap = new Dictionary<KeyCode, int>();
        // Add key-slot index associations here
        keyToSlotIndexMap[KeyCode.Alpha1] = 0;
        keyToSlotIndexMap[KeyCode.Alpha2] = 1;
        keyToSlotIndexMap[KeyCode.Alpha3] = 2;
        keyToSlotIndexMap[KeyCode.Alpha4] = 3;
        keyToSlotIndexMap[KeyCode.Alpha5] = 4;
        keyToSlotIndexMap[KeyCode.Alpha6] = 5;
        keyToSlotIndexMap[KeyCode.Alpha7] = 6;
        keyToSlotIndexMap[KeyCode.Alpha8] = 7;
        keyToSlotIndexMap[KeyCode.Alpha9] = 8;
    }
    private void ActivateSkillInSlot(int slotIndex)
    {
        if (skillBars == null) return;
        if (slotIndex >= 0 && slotIndex < skillBars.skillBarSlots.Length)
        {
            if (skillBars.skillBarSlots[slotIndex] != null)
            {
                skill = skillBars?.skillBarSlots[slotIndex].currentSkill as ActiveSkill;
                if (skill != null)
                {
                    UsingSkill = true;
                    castingOrChargingTimer = skill.castTime + skill.chargeTime;
                }
            }
        }
    }
    public override void Update()
    {
        // Debug.Log (isDuringCastingOrCharging);
        foreach (var kvp in keyToSlotIndexMap)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                ActivateSkillInSlot(kvp.Value);
            }
        }
        if (UsingSkill && (castingOrChargingTimer > 0) && !skill.isTargeted)
        {
            if (abilityCooldown <= 0)
            {
                UsingSkill = false;
                Debug.Log("Is This really fucking happening");
                isDuringCastingOrCharging = true;
            }
        }
        if (isDuringCastingOrCharging == false)
        {
            attackCooldown -= Time.deltaTime;
            abilityCooldown -= Time.deltaTime;
        }
        else if (isDuringCastingOrCharging == true)
        {
            motor.StopCharacter();
            castingOrChargingTimer -= Time.deltaTime;
            if (castingOrChargingTimer <= 0)
            {
                CastingSpellActions();
                isDuringCastingOrCharging = false;
            }

        }
        // if (skill && Input.GetKeyDown(key))
        // {
        //     UsingSkill = true;
        //     Debug.Log("Pressed button");
        // }
        if (UsingSkill)
        {
            if (skill.isTargeted == false)
            {
                if (castingOrChargingTimer <= 0)
                {
                    UsingSkill = false;
                    motor.StopCharacter();
                    if (abilityCooldown <= 0)
                    {
                        ResetCooldownsAfterSpell();
                        skill.Cast(this.gameObject, null, null);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && (!EventSystem.current.IsPointerOverGameObject()) && StateExtensions.CanCast(objectInformation.currentStatuses))
            {
                UsingSkill = false;
                isDuringCastingOrCharging = false;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (skill.isTargeted)
                {
                    if (Physics.Raycast(ray, out RaycastHit hit, 100, interactableMask))
                    {
                        targetToCastSkillOn = hit.collider.gameObject;
                        if (targetToCastSkillOn != this.gameObject)
                        {
                            SetFocus(targetToCastSkillOn, skill.range);
                        }
                    }
                }
                else
                {
                    if (Physics.Raycast(ray, out RaycastHit hit, 100, movementMask))
                    {
                        groundToCastSkillOn = new Vector3(Mathf.Round(hit.point.x * 2) / 2, hit.point.y, Mathf.Round(hit.point.z * 2) / 2);
                        isGroundToCastSkillOnValid = !groundToCastSkillOn.Equals(Vector3.negativeInfinity);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && (!EventSystem.current.IsPointerOverGameObject()))
        {
            isDuringCastingOrCharging = false;
            targetToCastSkillOn = null;
            groundToCastSkillOn = Vector3.negativeInfinity;
            isGroundToCastSkillOnValid = !groundToCastSkillOn.Equals(Vector3.negativeInfinity);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
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
        if (isFocused || isGroundToCastSkillOnValid)
        {
            if (isGroundToCastSkillOnValid) Debug.Log(groundToCastSkillOn + "IsWorkingIncorrectly");
            if (focusedObject == null) { isFocused = false; return; }
            if (!focusedObjectInteractable.isItem)
            {
                if (targetToCastSkillOn != null || isGroundToCastSkillOnValid)
                { InteractWithSkill(); }
                else
                { InteractWithAutoAttack(); }
            }
            else if (focusedObjectInteractable.isItem)
            {
                if (focusedObject == null)
                {
                    isFocused = false;
                }
                else
                { InteractPickUpItems(); }
            }
        }
    }
    public override void InteractWithAutoAttack()
    {
        radius = objectInformation.WeaponRange.GetValue();
        float distance = Vector3.Distance(this.transform.position, focusedObject.transform.position);
        if (distance <= radius)
        {
            TargetedAttack(objectInformation.focusedObject.GetComponent<NewCharacterStats>().objectInformation);
        }
    }
    public override void InteractWithSkill()
    {
        float distanceToObject = Vector3.Distance(this.transform.position, focusedObjectInteractable.gameObject.transform.position);
        if (skill.range != 0)
        {
            if (distanceToObject <= skill?.range && abilityCooldown <= 0)
            {
                if (castingOrChargingTimer > 0) isDuringCastingOrCharging = true;
                else CastingSpellActions();
            }
        }
        else if (distanceToObject <= objectInformation.WeaponRange.GetValue() && abilityCooldown <= 0)
        {
            if (castingOrChargingTimer > 0) isDuringCastingOrCharging = true;
            else CastingSpellActions();
        }
    }
    public void CastingSpellActions()
    {
        ResetCooldownsAfterSpell();
        RemoveFocus();
        motor.StopCharacter();
        skill.Cast(this.gameObject, targetToCastSkillOn, null);
        targetToCastSkillOn = null;
    }
    public void TargetedAttack(AllObjectInformation targetStats)
    {
        {
            //Enemy is getting attacked with myStats
            if (attackCooldown <= 0)
            {
                Debug.Log(targetStats);
                targetStats.GetAttacked(objectInformation.atk.GetValue(), 0, 1, Element.Neutral);
                ResetCooldownsAfterAttack();
                OnAttack?.Invoke();
            }
        }
    }
    private void ResetCooldownsAfterAttack()
    {
        attackCooldown = 1 / (objectInformation.aspd.GetValue() / 100);
        abilityCooldown = 1 / (objectInformation.aspd.GetValue() / 60);
    }
    private void ResetCooldownsAfterSpell()
    {
        attackCooldown = 1 / (objectInformation.aspd.GetValue() / 60);
        abilityCooldown = 1 / (objectInformation.aspd.GetValue() / 100);
    }
    public void RemoveFocus()
    {
        if (focus != null)
        {
            OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
    void SetFocus(GameObject newFocus, float Range = 0)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                OnDefocused();
            }
            OnFocused(newFocus);
            if (Range > 0)
            {
                motor.FollowTarget(objectInformation, newFocus, Range);
            }

            else
            {
                motor.FollowTarget(objectInformation, newFocus);
            }
        }
    }
}