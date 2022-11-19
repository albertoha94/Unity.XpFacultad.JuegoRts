using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Recolector : MonoBehaviour
{

    public float capacidad = 0.25f;
    [SerializeField] private Image energiaFill;

    private float _energiaRecolectada = 0;
    private Energia energiaAsignada;

    private float indiceRecoleccion = 0.001f;
    bool vaciando = false;

    // Componentes
    private Movil movil;

    private void Awake()
    {
        movil = GetComponent<Movil>();
    }

    public void Recolectar(Energia energia)
    {
        energiaAsignada = energia;

        if (Vector3.Distance(transform.position, energia.transform.position) <= 3f)
        {
            EmpezarRecoleccion(energia);
        }
        else
        {
            movil.Moverse(energia.transform.position, distancia: 2);
        }
    }

    void EmpezarRecoleccion(Energia mEnergia)
    {
        movil.Anim.SetBool("recolectando", true);
        energiaFill.color = Energia.GetColor(energiaAsignada.EnergiaTipo);
        StartCoroutine(Recolectando());
    }

    void TerminarRecoleccion()
    {
        movil.Anim.SetBool("recolectando", false);
        StopAllCoroutines();
    }

    void IrAVaciarEnergia(Vector3 posicionEdificio)
    {
        movil.Moverse(posicionEdificio);
        vaciando = true;
    }

    public IEnumerator Recolectando()
    {
        while (energiaAsignada && EnergiaRecolectada < capacidad)
        {
            EnergiaRecolectada += indiceRecoleccion;
            energiaAsignada.CantidadEnergia -= indiceRecoleccion;
            yield return new WaitForSeconds(0.1f);
        }

        TerminarRecoleccion();
        IrAVaciarEnergia(GameManager.EdificioRecolectorMasCercano(transform.position).transform.position);
    }

    public float EnergiaRecolectada
    {
        get { return _energiaRecolectada; }
        set
        {
            _energiaRecolectada = value;
            energiaFill.fillAmount = _energiaRecolectada/capacidad;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edificio"))
        {
            if (vaciando)
            {
                if (other.TryGetComponent(out EdificioRecolector edificioRecolector))
                {
                    VaciarEnergia();
                }
            }
        }
    }

    void VaciarEnergia()
    {
        switch (energiaAsignada.EnergiaTipo)
        {
            case EnergiaTipo.Roja:
                GameManager.EnergiaR += EnergiaRecolectada;
                break;
            case EnergiaTipo.Verde:
                GameManager.EnergiaG += EnergiaRecolectada;
                break;
            case EnergiaTipo.Azul:
                GameManager.EnergiaB += EnergiaRecolectada;
                break;
        }

        EnergiaRecolectada = 0;
        if (energiaAsignada)
        {
            Recolectar(energiaAsignada);
        }
    }

    public void OnZonaEnergiaEnter(Energia energia)
    {
        if (energiaAsignada == energia)
        {
            movil.Parar();
            EmpezarRecoleccion(energiaAsignada);
        }
    }

    public void OnZonaEnergiaExit(Energia energia)
    {
        if (energiaAsignada == energia)
        {
            TerminarRecoleccion();
        }
    }
}
