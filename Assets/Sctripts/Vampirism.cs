using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _detectorZoneDisplay;
    [SerializeField] private Player _player;
    [SerializeField] private DetectorVampire _detector;
    [SerializeField] private InputReader _inputReader;

    [SerializeField] private int _workTimeAbility = 6;
    [SerializeField] private int _reloadTimeAbility = 4;
    [SerializeField] private int _pauseTimeBetweenTicks = 1;
    [SerializeField] private int _powerAbility = 5;

    private WaitForSeconds _waitForSecondsReloadTimeAbility;
    private WaitForSeconds _waitForSecondsPauseTimeBetweenTicks;

    private bool _isBusy = false;

    private void Awake()
    {
        _detectorZoneDisplay.enabled = false;
        _waitForSecondsReloadTimeAbility = new WaitForSeconds(_reloadTimeAbility);
        _waitForSecondsPauseTimeBetweenTicks = new WaitForSeconds(_pauseTimeBetweenTicks);
    }

    private void OnEnable()
    {
        _inputReader.KeyHasPressed += TryApplyAbility;
    }

    private void OnDisable()
    {
        _inputReader.KeyHasPressed -= TryApplyAbility;
    }

    private IEnumerator StartVampirise()
    {
        int ticksCount = _workTimeAbility / _pauseTimeBetweenTicks;
        int damageDone;

        for (int i = 0; i < ticksCount; i++)
        {
            if (_detector.TryIdentifyNearestTarget(out Enemy enemy))
            {
                damageDone = enemy.TakeDamage(_powerAbility);
                _player.AddLifeForceIfValid(damageDone);
            }

            yield return _waitForSecondsPauseTimeBetweenTicks;
        }

        _detectorZoneDisplay.enabled = false;

        yield return _waitForSecondsReloadTimeAbility;

        _isBusy = false;
    }

    private void TryApplyAbility()
    {
        if (_isBusy == false)
        {
            _detectorZoneDisplay.enabled = true;
            StartCoroutine(StartVampirise());
            _isBusy = true;
        }
    }
}
