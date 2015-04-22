using UnityEngine;
using System.Collections;

public class MenuManagerSpawn : MonoBehaviour {

    public GameObject MenuManagerPrefab;

    void Awake()
    {
        if (!OptionsBox.OnlyOne) { GameObject MMPrefab = Instantiate(MenuManagerPrefab, transform.position - transform.position, Quaternion.identity) as GameObject; }
    }
}
