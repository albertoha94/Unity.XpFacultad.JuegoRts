using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdificioRecolector : MonoBehaviour
{
    private void Awake()
    {
        GameManager.AgregarEdificioRecoleccion(this);
    }
}
