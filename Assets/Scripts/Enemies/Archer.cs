using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] GameObject _projectilePrefab;

    public override void OnMove(Vector2Int movement)
    {
        base.OnMove(movement);
        OnAttack();
    }

    public override void OnAttack()
    {
        base.OnAttack();
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