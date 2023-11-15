using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation_Enemy : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        spriteRenderer.enabled = true;
        anim.Play("Attack");
    }

    public void DeactivateSelf()
    {
        spriteRenderer.enabled  = false;
    }

}
