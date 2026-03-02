using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [Header("Components")]
    public PlayerMovement movement;
    public PlayerAttack Attack;
    public CharacterController cc;

    [Header("Stats")]
    public int PlayerMaxHealth = 10;
    [HideInInspector] public int PlayerCurrentHealth;

    [Header("States")]
    public bool IsDead = false;

    private void Awake()
    {
        Instance = this;
        PlayerCurrentHealth = PlayerMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        PlayerCurrentHealth -= damage;
        Debug.Log("Player Health: " + PlayerCurrentHealth);

        if (PlayerCurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        movement.enabled = false;
        Attack.enabled = false;
        Debug.Log("Game Over");
    }
}