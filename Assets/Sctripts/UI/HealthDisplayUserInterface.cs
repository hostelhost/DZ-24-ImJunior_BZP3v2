using UnityEngine;

public class HealthDisplayUserInterface : MonoBehaviour
{
    [SerializeField] private Health _health;

    private IDisplayHealth[] _displayHealths;

    private void Start()
    {
        _displayHealths = GetComponents<IDisplayHealth>();
        Initialize();
    }

    private void OnEnable()
    {
        _health.HealthHasChanged += Print;
    }

    private void OnDisable()
    {
        _health.HealthHasChanged -= Print;
    }

    private void Initialize()
    {
        foreach (IDisplayHealth displayHealth in _displayHealths)
        {
            displayHealth.Initialize(_health.GetMaximumLifeForce());
            displayHealth.Print(_health.LifeForce);
        }
    }

    private void Print()
    {
        foreach (IDisplayHealth displayHealth in _displayHealths)
            displayHealth.Print(_health.LifeForce);
    }
}
