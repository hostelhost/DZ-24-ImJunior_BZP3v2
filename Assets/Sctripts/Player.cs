using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    [SerializeField] private Bag _bag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (collectable is Gold gold)
                TakeCoin(gold.Execute());
            else if (collectable is AidKit aidKit)
                AddLifeForceIfValid(aidKit.Execute());
        }
    }

    public int TakeDamage(int damage)
    {
        if (_health.TryShortenHealth(damage))
        {
            if (IsAlive() == false || _health == null)
                DeleteObject();

            return _health.LifeForce - damage;
        }

        return 0;
    }

    public void AddLifeForceIfValid(int lifeForce) =>  
        _health.AddLifeForceIfValid(lifeForce);

    private void TakeCoin(int coin) =>   
        _bag.TakeCoin(coin);

    private bool IsAlive() =>
       _health.LifeForce >= 0;

    private void DeleteObject() =>    
        Destroy(gameObject);
}
