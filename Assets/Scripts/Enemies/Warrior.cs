using UnityEngine;

public class Warrior : Enemy
{

    public override void OnEnd()
    {
        OnAttack();

        base.OnEnd();
    }

    public override void OnAttack()
    {
        if (_position.y < _range)
        {
            base.OnAttack();
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Warrior Attack!");
        GameManager.Instance.playerData.TakeDamage(_damage);
        return;
    }
}