using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float Teleport = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Commence(float amount)
    {
        Teleport -= amount;
        if (Teleport<= 0f)
        {
            Go();

        }
    }
    void Go()
    {
        Destroy(gameObject);
    }
}
