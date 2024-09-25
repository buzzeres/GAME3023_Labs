using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector2 inputVector = new Vector2(x, y);

        Vector3 Velocity = inputVector * moveSpeed;

        transform.position = transform.position + Velocity * Time.deltaTime;
    }
}
