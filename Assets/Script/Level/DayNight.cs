using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNight : MonoBehaviour
{
    [SerializeField] Gradient GracolorOverTimer;

    [SerializeField] Light2D Sun;

    [SerializeField] float timeScale = 0.1f;

    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time * timeScale;
        
        Sun.color = GracolorOverTimer.Evaluate(time);
    }
}
