using UnityEngine;
using System.Collections;

public class bublse : MonoBehaviour
{
    public GameObject particlesObject;
    public string metal;
    public float reactionTime = 5f;

    private ParticleSystem particles;
    private float baseSpeed = 1f;
    private bool isReacting = false;
    private float reactionTimer = 0f;
    public Experiment experiment;
    
    private Vector3 originalScale;
    private Coroutine scaleCoroutine;

    void OnDisable()
    {
        particlesObject.SetActive(false);
        transform.localScale = originalScale;
    }

    private void Start()
    {
        particles = particlesObject.GetComponent<ParticleSystem>();
        originalScale = transform.localScale;
        particlesObject.SetActive(false);
    }

    public void StartExperiment()
    {
        particlesObject.SetActive(false);
        transform.localScale = originalScale;
    }

    public GameObject explosionPrefab;
    public float timeBeforeExplosion = 9f;
    
    private void OnTriggerEnter(Collider other)
    {
        isReacting = true;
        reactionTimer = reactionTime;
        
        transform.localScale = originalScale;
        
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleDownOverTime(reactionTime));

        if (metal == "K")
        {
            StartCoroutine(DeactivateAfterDelay(timeBeforeExplosion));
            baseSpeed = 3f;
        }
        else if (metal == "Na")
        {
            baseSpeed = 2f;
        }
        else if (metal == "Li")
        {  
            baseSpeed = 1f;
        }

        UpdateParticles();
        particlesObject.SetActive(true);
    }
    
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (explosionPrefab != null)
        {
            explosionPrefab.SetActive(true);
            ParticleSystem ps = explosionPrefab.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                yield return new WaitForSeconds(ps.main.duration);
                explosionPrefab.SetActive(false);
            }
        }
    }
    
    private IEnumerator ScaleDownOverTime(float duration)
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t * t);
            yield return null;
        }
        
        transform.localScale = targetScale;
    }

    private void Update()
    {
        if (isReacting && particlesObject.activeSelf && experiment != null)
        {
            if (reactionTimer > 0)
            {
                float heatRate = 0f;
                if (metal == "K") heatRate = 15f;
                else if (metal == "Na") heatRate = 10f;
                else if (metal == "Li") heatRate = 5f;

                experiment.waterTemperature += heatRate * Time.deltaTime;
                UpdateParticles();
                reactionTimer -= Time.deltaTime;
            }
            else
            {
                StopReaction();
            }
        }
    }

    private void StopReaction()
    {
        isReacting = false;
        particlesObject.SetActive(false);
        
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        
        gameObject.SetActive(false);
    }
    
    public void ResetScale()
    {
        transform.localScale = originalScale;
        
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }
    }

    private void UpdateParticles()
    {
        var main = particles.main;
float tempMultiplier = 1f + (experiment.waterTemperature - 20f) / 50f;
        main.startSpeed = baseSpeed * tempMultiplier;
        main.startSize = 0.1f * tempMultiplier;

        if (experiment.waterTemperature > 50f)
        {
            main.startColor = Color.red;
        }
        else if (experiment.waterTemperature > 30f)
        {
            main.startColor = Color.yellow;
        }
        else
        {
            main.startColor = Color.white;
        }
    }
}