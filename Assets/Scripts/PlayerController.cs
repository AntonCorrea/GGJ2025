using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Forward/backward movement speed
    public bool canMove = true; 

    [Header("Raycast")]
    Ray ray;
    RaycastHit raycastHit;
    public LayerMask groundLayer;

    [Header("Lives")]
    public int lives = 3;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f; // Force of the knockback
    public float knockbackDuration = 0.5f; // Duration of the knockback
    public float knockbackRecoverTime = 1f;
    float currentKnockbackRecoverTime = 0f;
    private bool isKnockedBack = false; // Flag to prevent movement during knockback
    public ParticleSystem hitVFX;

    [Header("Power")]
    public PowerController power;

    private void Start()
    {

    }
    void Update()
    {

        if(canMove)
        {
            if (Input.GetMouseButton(0))
            {
                ///navMeshAgent.isStopped = false;
                ray = GameManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out raycastHit, 100f, groundLayer))
                {
                    if (isKnockedBack == false)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, raycastHit.point, moveSpeed * Time.deltaTime);
                        transform.LookAt(raycastHit.point);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                power.UsePower(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                power.UsePower(false);
            }

            currentKnockbackRecoverTime += Time.deltaTime;
        }

    }

    public void GetHit(int dmg)
    {
        if(currentKnockbackRecoverTime > knockbackRecoverTime)
        {
            if (isKnockedBack == false)
            {
                lives -= dmg;
                StartCoroutine(KnockBack());
                currentKnockbackRecoverTime = 0f;

                GameManager.Instance.uiCanvas.PlayerHit(dmg);
            }
        }     
    }

    IEnumerator KnockBack()
    {
        isKnockedBack = true;

        //hitVFX.Play();
        ParticleSystem copyhitVFX = GameObject.Instantiate(hitVFX,this.transform);
        copyhitVFX.transform.parent = null;
        copyhitVFX.Play();
        GameObject.Destroy(copyhitVFX.gameObject, 3f);

        // Calculate the knockback direction (opposite to the forward direction of the player)
        Vector3 knockbackDirection = -transform.forward;

        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            // Move the player in the knockback direction
            transform.Translate(knockbackDirection * knockbackForce * Time.deltaTime, Space.World);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        isKnockedBack = false;
    }

    public void ResetPlayer()
    {
        lives = 3;
        isKnockedBack = false;
    }
}

