using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttack") || collision.CompareTag("EnemyAttack"))
        {
            AttackObject attackObject = collision.GetComponent<AttackObject>();

        }
    }

    void TakeDamage()
    {

    }
}
