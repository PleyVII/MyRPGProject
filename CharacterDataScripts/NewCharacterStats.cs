using UnityEngine;
using System.Collections;

public class NewCharacterStats : MonoBehaviour
{
    bool onRegenHealth = false;
    bool onRegenMana = false;
    public string playerName = "Test";
    EquipmentManager additionalOnEquip;
    public AllObjectInformation objectInformation;
    void Awake()
    {
        // spiderEnemy = Resources.Load<SpiderEnemy>("SpiderEnemy");
        objectInformation = ScriptableObject.CreateInstance<AllObjectInformation>();
        if (objectInformation == null) Debug.Log("Failed to create an instance of AllObjectInformation at the startup.");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamageAndRemoveMana(1000, 50);
        }
        if (objectInformation.CurrentHealth < objectInformation.MaxHealth && onRegenHealth == false)
        {
            onRegenHealth = true;
            StartCoroutine("RegenHealth");
        }
        if (objectInformation.CurrentMana < objectInformation.MaxMana && onRegenMana == false)
        {
            onRegenMana = true;
            StartCoroutine("RegenMana");
        }
    }
    IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(5);
        onRegenHealth = false;
        objectInformation.CurrentHealth += objectInformation.HealthRegen;
        if (objectInformation.CurrentHealth >= objectInformation.MaxHealth) objectInformation.CurrentHealth = objectInformation.MaxHealth;
    }
    IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(5);
        onRegenMana = false;
        objectInformation.CurrentMana += objectInformation.ManaRegen;
        if (objectInformation.CurrentMana >= objectInformation.MaxMana) objectInformation.CurrentMana = objectInformation.MaxMana;
    }
    public void TakeDamageAndRemoveMana(float damagee, float manadamage)
    {
        float damage = 0;
        damage = ((damagee * (100 - objectInformation.realDef)) / 100) - objectInformation.realDef;
        objectInformation.CurrentHealth -= damage;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        Debug.Log(transform.name + " takes " + damage + " damage ");
        objectInformation.CurrentMana -= manadamage;
        if (objectInformation.CurrentMana < 0) objectInformation.CurrentMana = 0;
        if (objectInformation.CurrentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log("Die Bitch");
    }
}
