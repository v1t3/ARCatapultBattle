using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private readonly int minDestroyHeight = -10;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minDestroyHeight)
        {
            Destroy(gameObject);
        }
    }
}
