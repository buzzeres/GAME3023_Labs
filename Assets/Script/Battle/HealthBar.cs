using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject health;

    public void SetHealth(float healthValue)
    {
        // Ensure healthValue is between 0 and 1
        healthValue = Mathf.Clamp01(healthValue);

        //  GameObject has a RectTransform component
        RectTransform healthTransform = health.GetComponent<RectTransform>();

        // Update the scale of the health bar
        healthTransform.localScale = new Vector3(healthValue, 1f, 1f);
    }

    //public IEnumerator SetHPSmooth(float newHp)
    //{
    //    float curHp = health.transform.localScale.x; // Get current HP scale
    //    float targetHp = Mathf.Clamp(newHp, 0f, 1f); // Clamp target HP

    //    // Smoothly change the health bar scale over time
    //    while (Mathf.Abs(curHp - targetHp) > Mathf.Epsilon)
    //    {
    //        curHp = Mathf.Lerp(curHp, targetHp, Time.deltaTime * 5); // Smoothly interpolate between current and target
    //        health.transform.localScale = new Vector3(curHp, 1f, 1f); // Update health bar scale
    //        yield return null; // Wait for the next frame
    //    }

    //    health.transform.localScale = new Vector3(targetHp, 1f, 1f); // Ensure it ends exactly at the target
    //}
}
