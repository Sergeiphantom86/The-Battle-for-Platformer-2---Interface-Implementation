using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Health))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _characterAnimations;
    [SerializeField] private PaintAttack _paintAttack;

    private Health _health;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        _characterAnimations.Run(_enemyMover.HorizontalMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            TakeDamage(bullet.Damage);
            _paintAttack.ChangeColor();
        }
    }

    private void TakeDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.Amount <= 0)
        {
            WithdrawFromBattle();
        }
    }

    private void WithdrawFromBattle(bool died = true)
    {
        _characterAnimations.Die(died);
        _enemyMover.SetConfirmationDeath(died);
        gameObject.SetActive(_paintAttack.TryDelete());
    }
}