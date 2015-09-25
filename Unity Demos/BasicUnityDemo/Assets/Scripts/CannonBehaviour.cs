using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    private CardboardHead _head;

    public float ForceMagnitude = 300f;

    void Start()
    {
        _head = Camera.main.GetComponent<StereoController>().Head;
    }

    void Update()
    {
        if (Cardboard.SDK.Triggered)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            var rigidBody = sphere.AddComponent<Rigidbody>();
            rigidBody.mass = 0.1f;
            rigidBody.position = _head.transform.position;
            rigidBody.AddForce(_head.Gaze.direction * ForceMagnitude);
        }
    }
}
