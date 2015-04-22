using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public GameObject MenuManagerPrefab;

    public float Speed = 1f;
    public float Accel = 0.1f;

    void Awake()
    {
        if (!OptionsBox.OnlyOne) { GameObject MMPrefab = Instantiate(MenuManagerPrefab, transform.position - transform.position, Quaternion.identity) as GameObject; }
    }

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
