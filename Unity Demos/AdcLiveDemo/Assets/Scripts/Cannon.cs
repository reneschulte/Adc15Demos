using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public float ForceMagnitude = 300f;

    void Update()
    {
        if (Cardboard.SDK.Triggered)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            var rigidBody = sphere.AddComponent<Rigidbody>();
            rigidBody.mass = 0.2f;
            rigidBody.position = transform.position;
            rigidBody.AddForce(transform.forward * ForceMagnitude);
        }
    }
}
