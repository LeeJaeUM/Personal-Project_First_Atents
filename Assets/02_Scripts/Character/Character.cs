using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool isDieCheck = false;
    public int maxHp = 5;
    [SerializeField]
    private int hp;
    public int Hp
    {
        get => hp;
        private set
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
        if(isDieCheck)
        {
            return;
        }
        if(hp <= 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0.5f,0.5f,0.5f,0.5f);
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            isDieCheck = true;
        }
    }

    public void Checked(int index)
    {
        Debug.Log($"지금 {index}의 위치의 타일에 있다.");
    }
}
