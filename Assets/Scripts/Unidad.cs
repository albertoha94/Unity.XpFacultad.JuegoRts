using UnityEngine;

public class Unidad : MonoBehaviour
{

    // Atributos
    public int vidaMax;
    private int _vida;

    private Equipo _equipo;
    private bool _seleccionado;

    [Header("Componentes")]
    [SerializeField] GameObject panelUI;
    [SerializeField] Canvas vidaUI;
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
}
