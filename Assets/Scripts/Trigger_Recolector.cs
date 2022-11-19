using UnityEngine;

public class Trigger_Recolector : MonoBehaviour
{

    [SerializeField] Recolector recolector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Energia"))
        {
            recolector.OnZonaEnergiaEnter(other.GetComponent<Energia>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Energia"))
        {
            recolector.OnZonaEnergiaExit(other.GetComponent<Energia>());
        }
    }
}
