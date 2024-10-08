using UnityEngine;

[RequireComponent(typeof(Flipper), typeof(EnemyAttackZone))]

public class EnemyMover : MonoBehaviour
{
    private EnemyAttackZone _enemyAttackZone;
    private Flipper _flipper;
    private Enemy _enemy;
    private float _targetPosition;

    private float _horizontalMove;

    private void Awake()
    {
        _enemyAttackZone = GetComponent<EnemyAttackZone>();
        _flipper = GetComponent<Flipper>();
        _enemy = GetComponent<Enemy>();
        _targetPosition = -5;
        _horizontalMove = 1f;
    }

    private void Update()
    {
        Walk(GetNewVector());
        SetSpeed();
        DetermineDirection();
    }

    private void Walk(Vector2 vector2)
    {
        transform.position = Vector2.MoveTowards(transform.position, vector2, _horizontalMove * Time.fixedDeltaTime);
    }

    private void DetermineDirection()
    {
        if (Mathf.Abs(_targetPosition) - Mathf.Abs(transform.position.x) == 0)
        {
            SetTargetPosition();

            if (transform.position.x >= _targetPosition)
            {
                SetDirection(-_horizontalMove);
            }
            else
            {
                SetDirection(_horizontalMove);
            }
        }
    }

    private void SetSpeed()
    {
        if (_enemyAttackZone.IsCame)
        {
            _horizontalMove = 0;
        }
        else
        {
            _horizontalMove = 1;
        }

        _enemy.GetHorizontalMove(_horizontalMove);
    }

    private void SetDirection(float horizontalMove)
    {
        _flipper.FlipCharacter(horizontalMove);
    }

    private Vector2 GetNewVector()
    {
        return new Vector2(_targetPosition, transform.position.y);
    }

    private void SetTargetPosition()
    {
        if (_enemyAttackZone.IsLocatedInTargetZone)
        {
            _targetPosition = _enemyAttackZone.TargetPosition;
        }
        else
        {
            _targetPosition = GetRandomTarget();
        }
    }

    private int GetRandomTarget(int minPositionX = -10, int maxPositionX = 0)
    {
        return Random.Range(minPositionX, maxPositionX);
    }
}