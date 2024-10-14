using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public class mySubclass
    {
        public int a;
    }


    [SerializeField]
    public Dictionary<string, int> Stats = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        Stats.Add("Health", 10);
        Stats.Add("HealthCurrernt", Stats["HealthMax"]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
