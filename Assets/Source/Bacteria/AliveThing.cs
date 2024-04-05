
using UnityEngine;

public class AliveThing : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float HealthDecrease;

    private float Health;

    private void OnEnable()
    {
        Health = MaxHealth;
    }

    private void FixedUpdate()
    {
        Health -= HealthDecrease;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Increase(float value)
    {
        if(Health < MaxHealth)
        {
            Health += value;
        }
    }

    public void Decrease(float value)
    {
        if(Health > 0)
        {
            Health -= value;
        }
    }
}
