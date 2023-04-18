using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BillboardChild : MonoBehaviour
{
    Transform child;

    void Start() 
    {
        child = transform.GetChild(0);
    }

    void Update()
    {
        if (child) 
        {
            child.transform.rotation = Camera.main.transform.rotation;
        }
    }
}
