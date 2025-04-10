using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public Transform Targer { get { return target;} }
    [SerializeField]
    private Animator anim;
    public Animator Anim { get { return anim; } }
    [SerializeField]
    private float stoppingDistance = 5;
    public float StoppingDistance {  get { return stoppingDistance; } }
    [SerializeField] 
    private GameObject weaponPrefab;
    public GameObject WeaponPrefab {  get { return weaponPrefab; } }
    
    [SerializeField]
    private Transform throwPoint;
    public Transform ThrowPoint { get { return throwPoint; } }

    [Header("Сила броска")]
    [SerializeField]
    private float minThrowForce;
    public float MinThrowForce { get { return minThrowForce; } }
    [SerializeField]
    private float maxThrowForce;
    public float MaxThrowForce { get { return maxThrowForce; } }


    private NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent;} }

    private IEnemyState currentState;
    [SerializeField]
    private float throwCooldown = 2f;
    public float ThrowCooldown {  get { return throwCooldown; } }
    private float lastThrowTime;
    public float LastThrowTime { get { return lastThrowTime; } set { lastThrowTime = value; } }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SwitchState(new IdleState(this));
    }
    private void Update()
    {
        currentState?.Update();
    }
    public void SwitchState(IEnemyState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
    private void Throw()
    {
        GameObject weapon = GameObject.Instantiate(WeaponPrefab, ThrowPoint.position, ThrowPoint.rotation);
        Rigidbody rb = weapon.GetComponent<Rigidbody>();

        float force = Random.Range(MinThrowForce, MaxThrowForce);
        rb.linearVelocity = (Targer.position - ThrowPoint.position).normalized * force;
        weapon.GetComponent<ThrownWeapon>().Init(null);
    }
}
