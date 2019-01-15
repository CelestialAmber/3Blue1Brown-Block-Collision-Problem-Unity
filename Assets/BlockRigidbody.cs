using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRigidbody : MonoBehaviour
{
    public float mass;
    public float velocityX;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
    }
    // Update is called once per frame
    public void UpdateMotion()
    {
        transform.Translate(new Vector3(velocityX * 0.00001f, 0, 0)); 
    }
}
