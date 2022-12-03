using UnityEngine;

public class TriggerVision : MonoBehaviour
{

    private UnidadAtaque unidadAtaque;

    private void Awake()
    {
        unidadAtaque = transform.parent.GetComponent<UnidadAtaque>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggervision:" + other.gameObject.name);
        if (other.TryGetComponent(out Unidad unidad))
        {
            if (unidad.Equipo.id != unidadAtaque.movil.Equipo.id)
                unidadAtaque.OnVisionEnter(unidad);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unidad unidad))
        {
            if (unidad.Equipo.id != unidadAtaque.movil.Equipo.id)
                unidadAtaque.OnVisionExit(unidad);
        }
    }
}
