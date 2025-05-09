using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MyGizmos : MonoBehaviour
{
    public Color color = Color.red;

    public float radius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
