using System.Collections.Generic;
using UnityEngine;

public class DetectorVampire : MonoBehaviour
{
    [SerializeField] private float _targetReachMaxDistanse = 0.1f;

    private List<Enemy> _enemies = new List<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy> (out Enemy enemy))
            _enemies.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            _enemies.Remove(enemy);
    }

    public bool TryIdentifyNearestTarget(out Enemy enemy)
    {
        enemy = null;
        float minDistance = float.MaxValue;

        foreach (Enemy target in _enemies)
        {
            if (target == null) 
                continue;

            float distance = Vector2.SqrMagnitude(transform.position - target.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                enemy = target;
            }                   
        }

        return enemy != null;
    }
}
