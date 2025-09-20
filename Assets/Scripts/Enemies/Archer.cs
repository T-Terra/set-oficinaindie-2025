using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] GameObject _projectilePrefab;

    public override void OnAttack()
    {
        base.OnAttack();
        Shoot();
    }

    void Shoot()
    {
        Instantiate(_projectilePrefab, Vector3.down, Quaternion.identity);
    }
}