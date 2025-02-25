using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] Health _health;

    public int TakeDamage(int damage)
    {
        if (_health.TryShortenHealth(damage, out int damageDone))
        {
            if (IsAlive() == false || _health == null)
                DeleteObject();
        }

        return damageDone;
    }

    private bool IsAlive() =>
       _health.LifeForce > 0;

    private void DeleteObject() =>
        Destroy(gameObject);
}
