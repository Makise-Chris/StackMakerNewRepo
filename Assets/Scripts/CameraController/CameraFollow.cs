using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    [SerializeField] private float speed;
    
    void Awake()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
    }
}
