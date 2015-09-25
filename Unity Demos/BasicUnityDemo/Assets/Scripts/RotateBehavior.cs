using UnityEngine;

public class RotateBehavior : MonoBehaviour
{
    public float Speed = 2;

	void Update ()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * Speed, Space.World);
    }
}
