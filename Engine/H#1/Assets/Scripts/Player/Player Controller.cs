using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private PlayerAnimation anim;
    [SerializeField]
    private Transform camTransform;
    [SerializeField]
    private GameObject weaponSelectPanel;
    private CharacterController controller;

    [SerializeField] private FixedJoystick joystick;

    private void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        CustomInput.joystick = joystick;
    }
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isPause) return;
        float h = CustomInput.GetAxis("Horizontal");
        float v = CustomInput.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);

        move = Quaternion.Euler(0f, camTransform.eulerAngles.y, 0f) * move;
        controller.SimpleMove(move.normalized * moveSpeed);

        if(Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.isPause = true;
            weaponSelectPanel.SetActive(true);
        }
    }

    public void OpenWeaponPanel()
    {
        GameManager.Instance.isPause = true;
        weaponSelectPanel.SetActive(true);
    }
    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        Debug.Log($"Player took {damage}. HP left {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.PlayDied();
        GameManager.Instance.PlayerDied();
        Debug.Log("<color=red>YOU DIED</color>");
    }
}
