using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public float Speed = 1f;
    public float Accel = 0.1f;

    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x > 0)
        {
            Speed -= Accel;
        }
        else if (pos.x < 0)
        {
            Speed += Accel;
        }
        pos.x += Speed;
        transform.position = pos;
    }
}
