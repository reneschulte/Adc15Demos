using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float Speed = 2;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * Speed, Space.World);
    }
}
