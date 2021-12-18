using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GunUserInterface : MonoBehaviour
{
    public SimpleShoot parentShootClass;
    public Text Text;
    void Start()
    {
        Text = GetComponent<Text>();
    }
    void Update()
    { Text.text = parentShootClass.bulletsLeft.ToString();
    }

}
