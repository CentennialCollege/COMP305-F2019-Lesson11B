using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    public float health = 1.0f;
    public float damage = 0.0f;
    public float damageStep = 0.01f;

    public Transform healthBarForeground;
    public Transform healthBarDMG;

    public float healthbarlerp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (damage <= Mathf.Epsilon)
        {
            StopCoroutine(TakeDamage());
            if (Mathf.Approximately(health, 0.0f))
            {
                health = 0.0f;
            }

            healthbarlerp = Mathf.Lerp(healthBarDMG.localScale.x, healthBarForeground.localScale.x, Time.deltaTime * 3.0f);
            healthBarDMG.localScale = new Vector3(healthbarlerp, 1.0f, 1.0f);

        }

        
    }

    public void SetDamage(float dmg)
    {
        if (health > 0.0f)
        {
            damage = dmg;
            StartCoroutine(TakeDamage());
        }
    }

    // Coroutine
    private IEnumerator TakeDamage()
    {
        for (; damage > 0.0f; damage -= damageStep)
        {
            health -= damageStep;
            if (health < 0.0f)
            {
                health = 0.0f;
            }
            healthBarForeground.localScale = new Vector3(health, 1.0f, 1.0f);
            yield return null;
        }
    }
}
