using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public void disableObject()
    {
        gameObject.SetActive(false);
    }
}
