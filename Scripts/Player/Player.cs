using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Weapon), typeof(Health))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private CharacterAnimations _animation;
    [SerializeField] private PaintAttack _paintAttack;

    private Health _health;
    private InputReader _inputReader;
    private Weapon _weapon;

    private void Start()
    {
        _health = GetComponent<Health>();
        _inputReader = GetComponent<InputReader>();
        _weapon = GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        _animation.Run(_inputReader.Direction);
        _animation.Attack(_weapon.IsFire);

        if (_inputReader.GetIsJump())
        {
            _playerMover.Jump();
        }
    }

    public void TakeDamage(int damage)
    {
        _health.ApplyDamage(damage);
        _paintAttack.ChangeColor();

        if (_health.Amount <= 0)
        {
           gameObject.SetActive(false);
        }
    }

    public bool ApplyTreatment(int health)
    {
        return _health.ApplyTreatment(health);
    }
}