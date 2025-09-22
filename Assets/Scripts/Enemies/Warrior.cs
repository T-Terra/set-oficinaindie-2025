using System.Collections;

public class Warrior : Enemy
{

    protected override IEnumerator EndRoutine()
    {
        OnAttack();
        yield return StartCoroutine(base.EndRoutine());
    }

    protected override IEnumerator AttackRoutine()
    {
        yield return StartCoroutine(base.AttackRoutine());
        Attack();
    }

    void Attack()
    {
        GameManager.Instance.playerData.TakeDamage(_damage);
        return;
    }
}