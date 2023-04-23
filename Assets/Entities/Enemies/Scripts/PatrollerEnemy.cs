
using System;
using UnityEngine;


public class PatrollerEnemy : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _healthPrefab;
    [SerializeField] private Transform _canvas;
    [SerializeField] private DamageSystem _damageSystem;
    [SerializeField] private BaseController2D _controller;
    [SerializeField] private float _pathEndMargin;
    [SerializeField] private float _detectionRange; // Detection range starting from first path point and ending after last path point

    private int _pathIndex;
    private int _pathEnd;
    private bool _isNormal;
    private float _detectionRangeStart;
    private float _detectionRangeEnd;
    private EnemyState _state = EnemyState.Patrol;

    private void Awake()
    {
        var healthPrefab = Instantiate(_healthPrefab, _canvas);
        var healthUiScript = healthPrefab.GetComponent<EnemyHealthUi>();
        healthUiScript.SetOwner(transform);
        healthUiScript.SetDamageSystem(_damageSystem);
        _damageSystem.OnDead += HandleDeath;
    }

    

    private void Start()
    {
        Setup();
    }


    private void FixedUpdate()
    {
        if (_state == EnemyState.Dead)
        {
            _controller.Move(0);
            return;
        }
        _controller.SlopeCheck();
        if(TargetDetected())
        {
            if(Mathf.Abs(transform.position.x - _target.position.x) > 2f)
                Chase();
            else
                _controller.Move(0);
        }
        else
            Patrol();
    }

    private bool TargetDetected() => _target.position.x > _detectionRangeStart && _target.position.x < _detectionRangeEnd;

    private void Chase()
    {
        _state = EnemyState.Chase;
        var direction = GetDirection(_target.position);
        _controller.Move(direction);
    }
    private void Patrol()
    {
        _state = EnemyState.Patrol;
        int direction = GetDirection(_path.pathPoints[_pathIndex].position);
        _controller.Move(direction);

        if (_isNormal)
        {
            if (Mathf.Abs(transform.position.x - _path.pathPoints[_pathIndex].position.x) <= _pathEndMargin)
            {
                if (_pathIndex + 1 == _pathEnd + 1)
                {
                    _isNormal = false;
                    return;
                }
                _pathIndex++;
            }
        }

        else
        {
            if (Mathf.Abs(transform.position.x - _path.pathPoints[_pathIndex].position.x) <= _pathEndMargin)
            {
                if (_pathIndex - 1 == -1)
                {
                    _isNormal = true;
                    return;
                }
                _pathIndex--;
            }
        }
    }

    private int GetDirection(Vector2 targetDirection)
    {
        return targetDirection.x > transform.position.x ? 1 : -1;
    }

    private void Setup()
    {
        _pathEnd = _path.pathPoints.Length - 1;
        _detectionRangeStart = _path.pathPoints[0].position.x - 10f;
        _detectionRangeEnd = _path.pathPoints[_pathEnd].position.x + 10f;
        _isNormal = true;
        _pathIndex = 1;
    }
    private void HandleDeath(object sender, EventArgs e)
    {
        SetState(EnemyState.Dead);
    }

    public void SetState(EnemyState newState)
    {
        _state = newState;
    }
    private void OnDrawGizmos()
    {
        Vector3 center = new Vector2((_detectionRangeStart + _detectionRangeEnd) / 2f, transform.position.y);
        float radius = Mathf.Abs(_detectionRangeEnd - _detectionRangeStart) / 2f;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(center, radius);
    }

    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

}

