using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private enum SwordAttackMovement
    {
        NONE,
        FOREHAND,
        BACKHAND,
        THRUST
    }

    [SerializeField] private GameObject _swordPivot;

    [SerializeField] private float _attackDuration = 0.15f;
    [SerializeField] private float _thrustDuration = 0.1f;
    [SerializeField] private float _comboResetTime = 0.5f;
    [SerializeField] private float _cooldownDuration = 0.1f;

    private SwordAttackMovement _lastAttack = SwordAttackMovement.NONE;
    private bool _isAttacking;
    private float _comboTimer;
    private float _cooldownTimer;

    private void Start()
    {
        _swordPivot.SetActive(false);
    }
    void Update()
    {
        // attack cooldown
        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;

        // combo cooldown
        if (_comboTimer > 0)
        {
            _comboTimer -= Time.deltaTime;
            if (_comboTimer <= 0)
                _lastAttack = SwordAttackMovement.NONE;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !_isAttacking && _cooldownTimer <= 0)
        {
            SwordAttackMovement nextAttack;

            // FOREHAND -> BACKHAND -> THRUST -> loop to FOREHAND
            if (_lastAttack == SwordAttackMovement.THRUST || _lastAttack == SwordAttackMovement.NONE)
            {
                nextAttack = SwordAttackMovement.FOREHAND;
            }
            else
            {
                nextAttack = (SwordAttackMovement)((int)_lastAttack + 1);
            }

            StartCoroutine(AttackRoutine(nextAttack));
        }
    }

    private IEnumerator AttackRoutine(SwordAttackMovement attackMovement)
    {
        _isAttacking = true;
        _swordPivot.SetActive(true);

        float timer = 0;
        float duration = (attackMovement == SwordAttackMovement.THRUST) ? _thrustDuration : _attackDuration;

        Quaternion startRot = Quaternion.identity;
        Quaternion endRot = Quaternion.identity;
        Vector3 startPos = new Vector3(0, 0, -0.5f);
        Vector3 endPos = new Vector3(0, 0, 0.5f);

        // movement rotations
        if (attackMovement == SwordAttackMovement.FOREHAND)
        {
            startRot = Quaternion.Euler(0, 75, 0);
            endRot = Quaternion.Euler(0, -90, 0);
        }
        else if (attackMovement == SwordAttackMovement.BACKHAND)
        {
            startRot = Quaternion.Euler(0, -75, 0);
            endRot = Quaternion.Euler(0, 90, 0);
        }

        while (timer < duration)
        {
            float attackDuration = timer / duration;

            if (attackMovement == SwordAttackMovement.THRUST)
            {
                _swordPivot.transform.localPosition = Vector3.Lerp(startPos, endPos, attackDuration);

            }
            else
            {
                _swordPivot.transform.localRotation = Quaternion.Lerp(startRot, endRot, attackDuration);

            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Final reset and state update
        _swordPivot.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _swordPivot.SetActive(false);

        _lastAttack = attackMovement;
        _comboTimer = _comboResetTime;
        _cooldownTimer = _cooldownDuration;
        _isAttacking = false;
    }
}