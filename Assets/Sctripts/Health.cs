using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maximumLifeForce;

    public event Action HealthHasChanged;

    public int LifeForce { get; private set; }

    private void Awake()
    {
        LifeForce = _maximumLifeForce;
    }

    public bool TryShortenHealth(int damage)
    {
        if (IsCorrectValue(damage))
        {
            LifeForce -= damage;
            HealthHasChanged?.Invoke();

            return true;
        }

        return false;
    }

    public bool TryShortenHealth(int damage, out int damageDone)
    {
        if (IsCorrectValue(damage))
        {
            if (LifeForce >= damage)
            {
                LifeForce -= damage;
                damageDone = damage;
            }
            else
            {
                damageDone = LifeForce;
                LifeForce = 0;
            }

            HealthHasChanged?.Invoke();

            return true;
        }

        damageDone = 0;

        return false;
    }

    public void AddLifeForceIfValid(int lifeForce)
    {
        if (IsCorrectValue(lifeForce))
        {
            if (_maximumLifeForce <= lifeForce + LifeForce)
                LifeForce = _maximumLifeForce;
            else
                LifeForce += lifeForce;

            HealthHasChanged?.Invoke();
        }
    }

    public int GetMaximumLifeForce() =>
         _maximumLifeForce;

    private bool IsCorrectValue(int Incoming) =>
           Incoming > 0;
}
