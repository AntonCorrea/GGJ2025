using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    public bool active;
    public float radious;
    ParticleSystem particle;
    public Collider[] objectsInRange;
    public List<NpcController> filteredObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();


        UsePower(false);
    }

    private void Update()
    {
        
    }


    public void UsePower(bool use)
    {
        if (use)
        {
            active = true;
            particle.Play();

            // Find all colliders in the radius
            objectsInRange = Physics.OverlapSphere(transform.position, radious);

            // Filter only objects that have the specific class (e.g., MyClass)
            filteredObjects = objectsInRange
                .Select(c => c.GetComponent<NpcController>()) // Try to get MyClass component
                .Where(c => c != null) // Remove null entries (objects that don't have MyClass)
                .OrderBy(c => Vector3.Distance(transform.position, c.transform.position)) // Sort by distance
                .ToList();
            if (filteredObjects.Count>0 && filteredObjects[0])
            {
                GameManager.Instance.OpenPowerBubble(filteredObjects[0]);
            }
            particle.Stop();
        }
        else
        {
            active = false;
            particle.Stop();

        }
    }
}
