using System.Collections;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] GameObject _projectilePrefab;

    protected override IEnumerator MoveRoutine(Vector2Int movement)
    {
        yield return base.MoveRoutine(movement);
        OnAttack();
    }

    protected override IEnumerator AttackRoutine()
    {
        yield return base.AttackRoutine();
        Shoot();
    }

    void Shoot()
    {
        // angle from archer to player
        // player position is always (-0.5,0)
        var direction = new Vector3(-0.5f - transform.position.x, -transform.position.y);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        var rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Instantiate(_projectilePrefab, transform.position, rotation);
    }
}