using UnityEngine;

public class NpcController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Forward/backward movement speed
    public float rotationSpeed = 100f; // Rotation speed
    //public bool followAttackTarget = false;


    [Header("Lives")]
    public int lives = 1;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f; // Force of the knockback
    public float knockbackDuration = 0.5f; // Duration of the knockback
    private bool isKnockedBack = false; // Flag to prevent movement during knockback
    public ParticleSystem hitVFX;

    [Header("Targets")]
    public Transform objectTarget;
    public Transform waypointTarget;
    public Transform target;
    public bool followTarget = true;
    public bool turnToTarget = true;

    [Header("Navigation")]
    public WaypointsController waypoints;
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

        player = GameManager.Instance.player;
        waypointTarget = waypoints.waypointList[0];

            objectTarget = player.transform;
        

        target = waypointTarget;

        weapon = GetComponentInChildren<WeaponBase>();

    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            
        }
        if(turnToTarget)
        {
            transform.LookAt(target.position);
        }


        if (objectTarget)
        {
            if (Vector3.Distance(searchAreaOrigin.position, objectTarget.transform.position) < searchAreaSize)
            {
                target = objectTarget;
                currentSearchTime = 0f;
                followTarget = true;
                if (Vector3.Distance(transform.position, objectTarget.transform.position) < attackAreaSize)
                {
                    if (weapon)
                    {
                        if (hostile == true)
                        {
                            weapon.Attack();
                        }
                    }

                    //if (followAttackTarget == false)
                    {
                        followTarget = false;
                    }

                }

            }
            else
            {
                currentSearchTime += Time.deltaTime;
                if (currentSearchTime > searchTime)
                {
                    target = waypointTarget;
                    followTarget = true;
                }
            }
        }
        else
        {
            target = waypointTarget;
        }

        
        if(Vector3.Distance(this.transform.position,waypointTarget.position) < distanceToWaypoint)
        {
            UpdateWaypoint();
        }

       
    }

    void UpdateWaypoint()
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(searchAreaOrigin.position, searchAreaSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAreaSize);
    }
}
