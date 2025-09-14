using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrop : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;

    private void DestroyBox()
    {
        int index = Random.Range(0, guns.Length);
        Instantiate(guns[index]);
        guns[index].SetActive(true);
    }
}
