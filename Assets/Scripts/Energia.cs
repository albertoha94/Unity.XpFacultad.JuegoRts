using UnityEngine;

public class Energia : MonoBehaviour
{

    // Static
    public static Color GetColor(EnergiaTipo energiaTipo)
    {
        switch (energiaTipo)
        {
            case EnergiaTipo.Roja:
                return Color.red;
            case EnergiaTipo.Verde:
                return Color.green;
            case EnergiaTipo.Morada:
                return Color.magenta;
            case EnergiaTipo.Azul:
                return Color.yellow;
            default: return Color.clear;
        }
    }

    [SerializeField] private EnergiaTipo _energiaTipo;
    public EnergiaTipo EnergiaTipo => _energiaTipo;

    [SerializeField] private float _cantidadEnergia;

    private void OnValidate()
    {
        CantidadEnergia = _cantidadEnergia;
    }

    public float CantidadEnergia
    {
        get { return _cantidadEnergia; }
        set
        {
            if (value <= 0)
            {
                Destroy(gameObject);
                return;
            }
            _cantidadEnergia = value;
        }
    }
}

public enum EnergiaTipo
{
    Roja,
    Verde,
    Azul,
    Morada
}
