using UnityEngine;

public class IdleState : IEnemyState
{
    private EnemyAI ai;

    public IdleState(EnemyAI ai) => this.ai = ai;
    public void Enter()
    {
        ai.Anim.SetTrigger("Idle");
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        if (ai.Targer != null)
        {
            ai.SwitchState(new ChaseState(ai));
        }
    }
}
