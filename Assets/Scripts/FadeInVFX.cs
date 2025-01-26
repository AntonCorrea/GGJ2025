using System.Collections;
using UnityEngine;

public class FadeInVFX : MonoBehaviour
{
    [Header("Objects to Fade")]
    public Renderer[] objectsToFade; // Array of objects to fade
    public float fadeDuration = 2f;  // Duration of the fade-in effect

    public void Fade()
    {
        // Start the fade-in process
        StartCoroutine(FadeIn());
        print("fade");
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // Initialize objects to be fully transparent
        foreach (Renderer obj in objectsToFade)
        {
            if (obj.material.HasProperty("_Color"))
            {
                Color initialColor = obj.material.color;
                obj.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
            }
        }

        // Gradually increase alpha over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            foreach (Renderer obj in objectsToFade)
            {
                if (obj.material.HasProperty("_Color"))
                {
                    Color color = obj.material.color;
                    obj.material.color = new Color(color.r, color.g, color.b, alpha);
                }
            }

            yield return null; // Wait for the next frame
        }

        // Ensure alpha is fully opaque at the end
        foreach (Renderer obj in objectsToFade)
        {
            if (obj.material.HasProperty("_Color"))
            {
                Color color = obj.material.color;
                obj.material.color = new Color(color.r, color.g, color.b, 1);
            }
        }
    }
}

