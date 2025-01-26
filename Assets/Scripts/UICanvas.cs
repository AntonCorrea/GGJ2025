using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public GameObject heartOriginal;
    public List<GameObject> lives = new List<GameObject>();

    public Image fade;
    public float fadeTime = 2f;

    public GameObject gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit(int dmg)
    {
        int i = 0;
        while( i < dmg && lives.Count > 0)
        {
            GameObject.Destroy(lives[lives.Count - 1]);
            lives.Remove(lives[lives.Count - 1]);
            i++;
        }
        
    }

    public void FadeToBlack()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float newAlpha = Mathf.Lerp(0, 1f, elapsedTime / fadeTime);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        gameOver.SetActive(true);
    }

    public void ResetUI()
    {
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);
        gameOver.SetActive(false);

        lives.Clear();
        for(int i=0;i < GameManager.Instance.player.lives; i++)
        {
            GameObject heartClone = GameObject.Instantiate(heartOriginal, heartOriginal.transform.parent);
            lives.Add(heartClone);
            heartClone.SetActive(true);
        }
    }
}
