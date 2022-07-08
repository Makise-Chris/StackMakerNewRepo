using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    public Vector3 offset;
    [SerializeField] private float speed;
    
    void Awake()
    {
        instance = this;
        //offset = new Vector3(12.5f, 8f, 0.5f);
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
    }
}
