using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public void DestroyMagazine()
    {
        Destroy(this.gameObject);
    }
}