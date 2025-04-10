using UnityEngine;

public class Enemies : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;
    [SerializeField]
    private Animator anim;
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage}. HP left {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        anim.SetTrigger("Died");
        GameManager.Instance.UnregisterEnemy();
        Debug.Log("Enemie died");
        Destroy(gameObject);
    }
}
