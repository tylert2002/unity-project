using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class Melee : MonoBehaviour
{
    public GameObject melee;
    bool isAttacking = false; 
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();

        if(Input.GetMouseButtonDown(1))
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        if(!isAttacking)
        {
            melee.SetActive(true);
            isAttacking = true;
        }
    }

    void CheckMeleeTimer()
    {
        if(isAttacking)
        {
            atkTimer += Time.deltaTime; 
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0; 
                isAttacking = false;
                melee.SetActive(false);
            }
        }
    }
}
