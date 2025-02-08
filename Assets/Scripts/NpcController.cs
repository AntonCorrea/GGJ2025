using UnityEngine;

public class NpcController : ActionTarget
{
    [Header("Settings")]
    Vector3 spawnPosition;
    Quaternion spawnRotation;
    public enum Status { Idle,FollowingWaypoint, FollowingTarget, Attacking, Working, Patroling} 
    public Status status;
    public enum Mode { FollowTarget, FollowWaypoint}
    public Mode mode;
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Forward/backward movement speed
    public float rotationSpeed = 90f; // Rotation speed

    [Header("Lives")]
    public int lives = 1;

    //[Header("Knockback Settings")]
    //public float knockbackForce = 5f; // Force of the knockback
    //public float knockbackDuration = 0.5f; // Duration of the knockback
    //private bool isKnockedBack = false; // Flag to prevent movement during knockback
    //public ParticleSystem hitVFX;

    [Header("Targets")]
    //public ActionTarget waypointsTarget;//patrol route
    public ActionTarget target;
    public Transform currentTargetTransform;
    public bool moveToTarget = true;
    public bool turnToTarget = true;
    public bool hostile;
    public ActionTarget hostileTarget;//Target to be on the lookout, will gain currentTarget when in search area

    [Header("Navigation")]
    //public WaypointsController waypoints;
    int waypointIndex = 0;
    public int distanceToWaypoint = 1;

    [Header("Search & Attack Area")]
    public int searchAreaSize = 6;
    public Transform searchAreaOrigin;
    public float searchTime = 1f;
    public float currentSearchTime = 0f;
    public int attackAreaSize = 2;


    WeaponBase weapon;
    PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        player = GameManager.Instance.player;

        if (waypoints)
        {
            currentTargetTransform = waypoints.waypointList[0];
        }
        if (target)
        {
            currentTargetTransform = target.targetTransform;
        }
        if (hostileTarget)
        {
            hostile = true;           
        }

        status = Status.Idle;
        
        //currentTarget = waypointTarget;

        weapon = GetComponentInChildren<WeaponBase>();
    }

    // Update is called once per frame
    void Update()
    {

        if (moveToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTargetTransform.position, moveSpeed * Time.deltaTime);

        }
        if (turnToTarget)
        {
            Vector3 direction = currentTargetTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (hostileTarget)
        {
            LookForAlertTarget();
        }          
        

        if (waypoints)
        {
            UpdateWaypoint();
        }
        

    }

    void LookForAlertTarget()
    {
        if (Vector3.Distance(searchAreaOrigin.position, hostileTarget.transform.position) < searchAreaSize)
        {
            target = hostileTarget;
            currentSearchTime = 0f;
            moveToTarget = true;
            if (Vector3.Distance(transform.position, hostileTarget.transform.position) < attackAreaSize)
            {
                moveToTarget = false;
                if (weapon)
                {
                    if (hostile == true)
                    {
                        weapon.Attack();
                    }
                }
            }

        }
        else
        {
            currentSearchTime += Time.deltaTime;
            if (currentSearchTime > searchTime)
            {
                //target = waypointsTarget;
                moveToTarget = true;
            }
        }
    }

    void UpdateWaypoint()
    {
        if (Vector3.Distance(this.transform.position, currentTargetTransform.position) < distanceToWaypoint)
        {
            if (waypointIndex == waypoints.waypointList.Count - 1)
            {
                waypointIndex = 0;
            }
            else
            {
                waypointIndex++;
            }

            currentTargetTransform = waypoints.waypointList[waypointIndex];
            //target = waypointsTarget;
        }


        
    }

    public void SetTarget(ActionTarget t)
    {
        target = t;
        currentTargetTransform = t.targetTransform;
    }

    public void ResetNPC()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(searchAreaOrigin.position, searchAreaSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAreaSize);
    }


}
