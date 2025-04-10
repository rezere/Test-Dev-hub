using UnityEngine;

public class DamageZone: MonoBehaviour
{
    [SerializeField]
    private float damageMultiplier = 1f;

    public float GetMultiplier() => damageMultiplier;
}
