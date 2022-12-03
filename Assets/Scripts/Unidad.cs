using UnityEngine;
using UnityEngine.UI;

public class Unidad : MonoBehaviour
{

    // Atributos
    public int vidaMax;
    private int _vida;

    public EquipoTest equipoTest;
    public enum EquipoTest { azul, rojo };
    private Equipo _equipo;
    private bool _seleccionado;

    [Header("Componentes")]
    [SerializeField] GameObject panelUI;
    [SerializeField] Canvas vidaUI;
    [SerializeField] Image barraDeVida;
    public Canvas seleccionadoUI;

    // Animator
    private Animator animator;

    public Animator Anim => animator;

    protected void Awake()
    {
        transform.GetChild(0).TryGetComponent(out animator);
    }

    // Start is called before the first frame update
    void Start()
    {
        Equipo =  GameManager.GetEquipo(((int)equipoTest));
    }

    // Update is called once per frame
    void Update()
    {
        panelUI.transform.LookAt(Camera.main.transform);
    }

    public bool Seleccionado
    {
        get { return _seleccionado; }
        set
        {
            _seleccionado = value;
            seleccionadoUI.gameObject.SetActive(value);
        }
    }

    public virtual void Morir()
    {
        Destroy(gameObject);
    }

    public Equipo Equipo
    {
        get { return _equipo; }
        set
        {
            _equipo= value;

            if (_equipo== null)
                return;

            barraDeVida.color = _equipo.color;
        }
    }

    public int Vida
    {
        get { return _vida; }
        set
        {
            if (value > vidaMax)
                _vida = vidaMax;
            else if (value <= 0)
            {
                Morir();
            }
            else
            {
                _vida= value;
            }
            barraDeVida.fillAmount = (float)_vida / vidaMax;
        }
    }
}
