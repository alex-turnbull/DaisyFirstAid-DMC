using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOnMenuOpen : MonoBehaviour
{
    public UIControl ui;

    private void Update()
    {
        GetComponent<Button>().enabled = !ui.menuOpen;
    }
}
