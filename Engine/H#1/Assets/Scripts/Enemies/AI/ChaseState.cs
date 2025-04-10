using UnityEngine;

public class ChaseState : IEnemyState
{
    private EnemyAI ai;

    public ChaseState(EnemyAI ai) => this.ai = ai;

    public void Enter()
    {
        ai.Anim.SetTrigger("Walk");
    }
    public void Update()
    {
        if(GameManager.Instance.isPause || !GameManager.Instance.isPlay) return;
        float distance = Vector3.Distance(ai.transform.position, ai.Targer.position);
        if (distance <= ai.StoppingDistance)
        {
            ai.SwitchState(new AttackState(ai));
            return;
        }
        ai.Agent.SetDestination(ai.Targer.position);
    }
    public void Exit()
    {
       ai.Agent.ResetPath();
    }

    
}
