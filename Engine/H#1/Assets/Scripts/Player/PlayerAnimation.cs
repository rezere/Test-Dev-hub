using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float speed = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void PlayThrow()
    {
        animator.SetTrigger("Thrown");
    }
    public void ThrowEnd()
    {
        animator.SetTrigger("ThrownEnd");
    }
    public void PlayDied()
    {
        animator.SetTrigger("Died");
    }

}
