using UnityEngine;

public class ThrownSystem : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField]
    private GameObject currentWeaponPrefab;
    [SerializeField]
    private Transform throwPoint;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private ObjectPool weaponPool;
    [SerializeField] 
    private PlayerAnimation animationController;

    [Header("Траектория")]
    [SerializeField]
    private int linePoints = 30;
    [SerializeField]
    private float timeStep = 0.1f;

    [Header("Настройки броска")]
    [SerializeField]
    private float minThrowForce = 5f;
    [SerializeField]
    private float maxThrowForce = 25f;
    [SerializeField]
    private float maxHoldTime = 2f;
    [SerializeField]
    private float throwCooldown = 1.0f;

    
    
    
    private float holdTime;
    private bool isCharging;
    private float currentForce;
    private float lastThrowTime;
    private GameObject currentWeapon;
    private Camera mainCam;

    public void Initialized(GameObject prefab)
    {
        
        currentWeapon = prefab;
        mainCam = Camera.main;
        weaponPool.SetPrefab(currentWeaponPrefab);

        currentWeapon = Instantiate(currentWeaponPrefab, throwPoint.position, throwPoint.rotation, throwPoint);
        Destroy(currentWeapon.GetComponent<BoxCollider>());
        Destroy(currentWeapon.GetComponent<Rigidbody>());
    }

    public void SetCurrentWeapon(GameObject newPrefab)
    {
        Destroy(currentWeapon);

        currentWeaponPrefab = newPrefab;
        weaponPool.SetPrefab(currentWeaponPrefab);
        
        currentWeapon = Instantiate(currentWeaponPrefab, throwPoint.position, throwPoint.rotation, throwPoint);
        Destroy(currentWeapon.GetComponent<BoxCollider>());
        Destroy(currentWeapon.GetComponent<Rigidbody>());

    }
    private void Update()
    {
        if(GameManager.Instance.isPause) { return; }
        if (!GameManager.Instance.isPlay)
        {
            if (currentWeapon != null) Destroy(currentWeapon);
            lineRenderer.enabled = false;
            return;
        }
        if (Input.GetMouseButtonDown(1) && Time.time >= lastThrowTime)
        {
            animationController.PlayThrow();
            isCharging = true;
            holdTime = 0f;
        }
        if (Input.GetMouseButton(1) && isCharging)
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime/ maxHoldTime);
            currentForce = Mathf.Lerp(minThrowForce, maxThrowForce, t);
            ShowTrajectory(currentForce);
        }
        if(Input.GetMouseButtonUp(1) && isCharging && Time.time >= lastThrowTime)
        {
            animationController.ThrowEnd();
            lineRenderer.enabled = false;
            isCharging = false;
            lastThrowTime = Time.time + throwCooldown;
        }
        if (isCharging && !Input.GetMouseButtonUp(1))
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / maxHoldTime);
            currentForce = Mathf.Lerp(minThrowForce, maxThrowForce, t);
            ShowTrajectory(currentForce);
        }
    }
    public void StartCharging()
    {
        animationController.PlayThrow();
        isCharging = true;
        holdTime = 0f;
    }

    public void ReleaseCharge()
    {
        if (!isCharging) return;

        animationController.ThrowEnd();
        isCharging = false;
        //ThrowWeapon();
    }
    private void ShowTrajectory(float force)
    {
        lineRenderer.enabled = true;

        Vector3 velocity = throwPoint.forward * force;
        Vector3 currentPosition = throwPoint.position;

        lineRenderer.positionCount = linePoints;

        for (int i = 0; i < linePoints; i++)
        {
            lineRenderer.SetPosition(i, currentPosition);
            velocity += Physics.gravity * timeStep;
            currentPosition += velocity * timeStep;
        }
    }

    private void ThrowWeapon()
    {
        currentWeapon.SetActive(false);
        GameObject tempWeapon = weaponPool.GetFromPool(throwPoint.position, throwPoint.rotation);
        Rigidbody rb = tempWeapon.GetComponent<Rigidbody>();
        
        rb.linearVelocity = throwPoint.forward * currentForce;
        rb.angularVelocity = Vector3.zero;
        ThrownWeapon thrown = tempWeapon.GetComponent<ThrownWeapon>();
        thrown.Init(weaponPool);
        Invoke(nameof(SetOnWeapon), throwCooldown);
    }

    private void SetOnWeapon()
    {
        currentWeapon.SetActive(true);
    }
}
