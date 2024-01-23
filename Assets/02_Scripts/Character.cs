using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int attackDamage = 1;
    public int maxHp = 3;
    [SerializeField]
    private int hp = 3;
    private int Hp
    {
        get => hp;
        set
        {
            if(hp != value)
            {
                hp = value; 
            }
        }
    }

    private void Awake()
    {
        Hp = maxHp;
    }

    private void Update()
    {
        Die();
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
    }

    void Die()
    {
        if(hp <= 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0.5f,0.5f,0.5f,0.5f);
        }
    }

}
