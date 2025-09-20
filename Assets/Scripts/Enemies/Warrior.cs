using UnityEngine;

public class Warrior : Enemy
{
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
        // deal damage to barrier
        return;
    }
}