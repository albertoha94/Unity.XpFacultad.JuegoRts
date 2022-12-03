using UnityEngine;

public class TriggerAtaque : MonoBehaviour
{

    internal int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unidad unidad))
        {
            unidad.Vida -= damage;
        }
    }
}
