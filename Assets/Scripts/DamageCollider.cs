using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerController>().GetHit();
            GameManager.Instance.PlayerHit(damage);
        }
    }
}
