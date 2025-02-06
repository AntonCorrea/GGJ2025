using UnityEngine;

public class NpcController : ActionTarget
{
    [Header("Settings")]
    Vector3 spawnPosition;
    Quaternion spawnRotation;
    public enum Status { Idle,Following,Attack,Work} 
    public Status status;
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Forward/backward movement speed
    public float rotationSpeed = 90f; // Rotation speed

    [Header("Lives")]
    public int lives = 1;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f; // Force of the knockback
    public float knockbackDuration = 0.5f; // Duration of the knockback
    private bool isKnockedBack = false; // Flag to prevent movement during knockback
    public ParticleSystem hitVFX;

    [Header("Targets")]
    public Transform alertTarget;//Target to be on the lookout, will gain currentTarget when in search area
    public Transform waypointTarget;//patrol route
    public Transform currentTarget;
    public bool moveToTarget = true;
    public bool turnToTarget = true;

    [Header("Navigation")]
    //public WaypointsController waypoints;
    int waypointIndex = 0;
    public int distanceToWaypoint = 1;

    [Header("Search & Attack Area")]
    public bool hostile;
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
            waypointTarget = waypoints.waypointList[0];
        }
        if (alertTarget)
        {
            alertTarget = player.transform;
        }

        status = Status.Idle;
        
        currentTarget = waypointTarget;

        weapon = GetComponentInChildren<WeaponBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget)
        {
            if (moveToTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

            }
            if (turnToTarget)
            {
                Vector3 direction = currentTarget.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (alertTarget)
            {
                LookForAlertTarget();
            }
            //else
            //{
            //    currentTarget = waypointTarget;
            //}


            
        }

        
        UpdateWaypoint();

    }

    void LookForAlertTarget()
    {
        if (Vector3.Distance(searchAreaOrigin.position, alertTarget.transform.position) < searchAreaSize)
        {
            currentTarget = alertTarget;
            currentSearchTime = 0f;
            moveToTarget = true;
            if (Vector3.Distance(transform.position, alertTarget.transform.position) < attackAreaSize)
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
                currentTarget = waypointTarget;
                moveToTarget = true;
            }
        }
    }

    void UpdateWaypoint()
    {
        if (Vector3.Distance(this.transform.position, waypointTarget.position) < distanceToWaypoint)
        {
            if (waypointIndex == waypoints.waypointList.Count - 1)
            {
                waypointIndex = 0;
            }
            else
            {
                waypointIndex++;
            }

            waypointTarget = waypoints.waypointList[waypointIndex];
            currentTarget = waypointTarget;
        }


        
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
