using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    public GameObject enable;

    private void OnDestroy()
    {
        enable.SetActive(true);
    }
}
