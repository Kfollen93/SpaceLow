using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAi : MonoBehaviour
{
    [Header("General AI Settings")]

    public NavMeshAgent _agentIfNPCMoves;
    [SerializeField] private Transform _targetTransform; 
    [SerializeField] private LayerMask _selectTargetLayer;
    [SerializeField] private float _sightRange, _attackRange; // Attack range should always be 0 if you want a melee type enemy
    private bool _targetInSightRange, _targetInAttackRange;
    [SerializeField] private Transform _moveToThisDestination;
    public bool _isRangeAI, _isTurretAI, _isFighterAI; // Set from the inspector for each model type
    private float _fly_height; // for flying enemies
    
    [Header("Laser Robot Settings")]

    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private GameObject robotHead;

    [Header("Patrol Settings")]

    [SerializeField] private bool _patrolWaiting;
    [SerializeField] private float _totalWaitTime;
    [SerializeField] private List<Waypoint> _patrolPoints; // reference to Waypoint class
    private int _currentPatrolIndex = 0;
    private bool _traveling;
    private bool _waiting;
    private bool _patrolForward = true;
    private float _waitTimer;
    private bool CanPatrol => _agentIfNPCMoves != null && _patrolPoints != null && _patrolPoints.Count >= 2;

    [Header("Projectile Settings")]

    [SerializeField] private float _attackDelay;
    private bool _alreadyAttacked;
    [SerializeField] private GameObject _projectile;
    private bool _isReloading = false;
    private Vector3 yOffset = new Vector3(0, 3, 0);
    
    [Header("Turret Settings")]

    [SerializeField] private float _turretReloadTime;
    [SerializeField] private int _turretMagMax;
    [SerializeField] private Transform turretBarrel;
    private int _turretMagCount;
    private Color originalTurretColor; 
    public MeshRenderer rend; 
    public Transform turretEmptyTip;
    public ParticleSystem turretSmoke;
    public bool useLaser; 
    [SerializeField] private LineRenderer lr; 

    [Header("Audio Settings")]

    public AudioSource audioSrcOne;
    public AudioSource audioSrcTwo;

    private void Awake()
    {
        _turretMagCount = _turretMagMax;
        
        // Edge case check to prevent enemy spawning and wanting to set the player as the target, right as the player dies.
        if (GameObject.FindWithTag("Player").transform && _targetTransform == null)
        {
            // Arena enemy setting player as target
            _targetTransform = GameObject.FindWithTag("Player").transform;
        }

        if (_isTurretAI && !useLaser && lr == null)
        { 
            return;
        }         
    }

    private void Start()
    {
        _agentIfNPCMoves = GetComponent<NavMeshAgent>();
        _isReloading = false;
        if (_isTurretAI)
        {
            originalTurretColor = rend.material.color;
        }
        CheckRangeStatus();
        CheckForPatrolPoints();
    }

    private void Update()
    {
        SetTargetRanges();
        AiPatrol();
        AiMoveTowardTarget();
        AiAttack();
    }

    private void AiAttack()
    {
        if (_targetInAttackRange && _targetInSightRange) 
        {
            AttackPlayer();
        }
    }

    private void ChaseTarget()
    {
        if (_isTurretAI)
        { 
            turretBarrel.LookAt(_targetTransform.position + yOffset); 
            TargetLaser();
        }
        else
        { 
            Vector3 target = _targetTransform.transform.position;
            _agentIfNPCMoves.SetDestination(target);
        }
    }

    private void SetMoveDestination()
    {
        if (_moveToThisDestination != null)
        {
            Vector3 targetVector = _moveToThisDestination.transform.position;
            _agentIfNPCMoves.SetDestination(targetVector);
        }
    }

    private void AiMoveTowardTarget()
    {
        // AI detects target and chases. Exceptions: turret 
        if (_targetInSightRange && !_targetInAttackRange)
        {   
            ChaseTarget();
        }
    }

    private void AiPatrol()
    {
        if (!_targetInSightRange && !_targetInAttackRange && !_isTurretAI)
        {
            CheckForDestinationDistance();
            CheckforPatrolWaiting();
        }
    }

    private void SetTargetRanges()
    {
        _targetInSightRange = Physics.CheckSphere(transform.position, _sightRange, _selectTargetLayer);
        _targetInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _selectTargetLayer);
    }

    private void CheckRangeStatus()
    {
        if (_isRangeAI)
        {
            _fly_height = GetComponent<NavMeshAgent>().baseOffset;
            _agentIfNPCMoves.SetDestination(Vector3.up * _fly_height);
        }

        if (!_targetInSightRange && !_targetInAttackRange)
        {
            CheckForPatrolPoints();
        }

        if (_targetInSightRange && !_targetInAttackRange)
        {
            ChaseTarget();
        }

        if (_targetInAttackRange && _targetInSightRange)
        {
            AttackPlayer();
        }
        else
        {
            SetMoveDestination();
        }
    }

    private void CheckForPatrolPoints()
    {
        if (CanPatrol)
        {
            _currentPatrolIndex = 0;
            SetPatrolDestination();
        }
    }

    private void CheckForDestinationDistance()
    {
        if (_traveling && _agentIfNPCMoves.remainingDistance <= 0.5f) // NOTE: adjustment here depending on AI behavior 
        {
            _traveling = false;

            if (_patrolWaiting)
            {
                _waiting = true; // OR set idle 
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetPatrolDestination();
            }
        }
    }

    private void CheckforPatrolWaiting()
    {
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;

            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetPatrolDestination();
            }
        }
    }

    public void SetPatrolDestination()
    {
        if (_patrolPoints != null)
        {
            // get the coordinates of patrol point; set that as the destination; start traveling
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _agentIfNPCMoves.SetDestination(targetVector);
            _traveling = true;
        }
    }

    public void ChangePatrolPoint()
    {
        // set next patrol point. need a check for end of list 
        if (_patrolForward) // NOTE: boolean here in case you want to move to mix it up and move to previous patrol point 
        {
            _currentPatrolIndex++;
            if (_currentPatrolIndex >= _patrolPoints.Count)
            {
                _currentPatrolIndex = 0;
            }
        }
        else
        {
            _currentPatrolIndex--;
            if (_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }

    private void AILaser() 
    { 
        float distance = Vector3.Distance(transform.position, _targetTransform.position);
        if (distance <= _attackRange)
        {
            PlayLaserAudio();
            ChaseTarget(); 
            enemyLaser.SetActive(true);
            robotHead.transform.Rotate(0f, 0f, 360f * Time.deltaTime);
        }
        else
        {
            enemyLaser.SetActive(false);
            robotHead.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    private void PlayLaserAudio() 
    { 
        audioSrcOne.Play(); 
    }
    
    public void AttackPlayer()
    {
        if (_isRangeAI && !_isTurretAI && !_isFighterAI)
        {
            AIShoot();
        }
        else if (_isTurretAI && !_isRangeAI && !_isFighterAI)
        {
            TurretShoot();
        }
        else if (_isFighterAI && !_isTurretAI && !_isRangeAI)
        {
            AILaser();
        }
        else
        {
            Debug.Log("ERROR: unknown attack state");
        }
    }

    private void TurretShoot()
    {
        turretBarrel.LookAt(_targetTransform.position + yOffset);
        TargetLaser();

        if (!_alreadyAttacked && _turretMagCount > 0)
        {
            Instantiate(_projectile, turretEmptyTip.transform.position, turretEmptyTip.transform.rotation);
            audioSrcOne.Play();
            _turretMagCount--;
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _attackDelay);
        }

        // reload 
        if (_turretMagCount <= 0 && !_isReloading)
        {
            StartCoroutine(TurretReload());
        }
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    private void TurretReloadAudio() 
    { 
        audioSrcTwo.Play();
        turretSmoke.Play();
    }

    private IEnumerator TurretReload()
    {
        _isReloading = true;

        TurretReloadAudio();          
        ReloadAnimation(); 

        // wait for reload time; ready gun; reset turret settings  
        yield return new WaitForSeconds(_turretReloadTime);  
        CockTurret();

        yield return new WaitForSeconds(1.0f); 
        ResetTurret(); 
    }

    private void ReloadAnimation() 
    { 
        rend.material.color = Color.red; 
    }

    private void CockTurret() 
    { 
        turretSmoke.Stop(); 
        audioSrcTwo.Stop(); 
        rend.material.color = Color.green;
    }

    private void ResetTurret() 
    { 
        rend.material.color = originalTurretColor; 
        _turretMagCount = _turretMagMax;
        _isReloading = false;
    }

    private void AIShoot()
    {
        // stop movement 
        _agentIfNPCMoves.SetDestination(transform.position);
        transform.LookAt(_targetTransform);
 
        // enemy shooting
        if (!_alreadyAttacked)
        {
            Instantiate(_projectile, transform.position, transform.rotation);
            audioSrcOne.Play();
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _attackDelay);
        }
    }

    private void TargetLaser() 
    { 
        if (useLaser)
        {
            ToggleLaser(); 
            lr.SetPosition(0, turretEmptyTip.position);
            lr.SetPosition(1, _targetTransform.position + yOffset);
        }
    }

    private void ToggleLaser() 
    { 
        if ((_targetInSightRange || _targetInAttackRange) && !_isReloading)
        { 
            if (useLaser)
                lr.enabled = true;
        }
        else
        { 
            lr.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
