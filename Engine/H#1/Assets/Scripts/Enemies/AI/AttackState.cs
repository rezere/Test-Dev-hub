using UnityEngine;

public class AttackState : IEnemyState
{
    private EnemyAI ai;

    public AttackState(EnemyAI ai) => this.ai = ai;

    public void Enter()
    {
      
    }
    public void Update()
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isPause) return;
        float distance = Vector3.Distance(ai.transform.position, ai.Targer.position);
        ai.transform.LookAt(ai.Targer);
        
        if(distance > ai.StoppingDistance+1)
        {
            ai.SwitchState(new ChaseState(ai));
            return;
        }
        if(Time.time > ai.LastThrowTime)
        {
            ai.Anim.SetTrigger("Thrown");
            ai.LastThrowTime = Time.time + ai.ThrowCooldown;
        }
    }
    public void Exit()
    {
        
    }

   
}
