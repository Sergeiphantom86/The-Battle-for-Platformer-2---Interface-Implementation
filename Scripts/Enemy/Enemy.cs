using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _characterAnimations;
    [SerializeField] private PaintAttack _paintAttack;
    [SerializeField] private Health _health;

    private float _horizontalMove;
    private bool _died;

    private void Update()
    {
        _characterAnimations.Run(_horizontalMove);
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
            _died = true;
            _characterAnimations.Die(_died);
            StartCoroutine(DelayBeforeDeleting());
        }
    }

    public void GetHorizontalMove(float horizontalMove)
    {
        _horizontalMove = horizontalMove;
    }

    private IEnumerator DelayBeforeDeleting()
    {
        float delay = 0.7f;
        
        WaitForSeconds wait = new (delay);

        yield return wait;

        gameObject.SetActive(false);
    }
}