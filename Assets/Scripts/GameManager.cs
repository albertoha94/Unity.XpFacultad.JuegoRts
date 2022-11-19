using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Public
    #region Economia

    private static Jugador jugador1;
    public static float EnergiaR
    {
        get => jugador1.energiaR;
        set
        {
            jugador1.energiaR = value;
            // HUD_Energias.R.text = Mathf.Floor(jugador1.energiaR).ToString();
            HUD_Energias.R.text = jugador1.energiaR.ToString();
        }
    }
    public static float EnergiaG
    {
        get => jugador1.energiaG;
        set
        {
            jugador1.energiaG = value;
            HUD_Energias.G.text = Mathf.Floor(jugador1.energiaG).ToString();
        }
    }
    public static float EnergiaB
    {
        get => jugador1.energiaB;
        set
        {
            jugador1.energiaB = value;
            HUD_Energias.B.text = Mathf.Floor(jugador1.energiaB).ToString();
        }
    }

    #endregion

    #region Edificios

    private static List<EdificioRecolector> edificioRecolectors = new List<EdificioRecolector>();

    public static void AgregarEdificioRecoleccion(EdificioRecolector edificioRecolector)
    {
        edificioRecolectors.Add(edificioRecolector);
    }

    public static EdificioRecolector EdificioRecolectorMasCercano(Vector3 origen)
    {
        return edificioRecolectors.OrderBy(edificio => Vector3.Distance(origen, edificio.transform.position)).FirstOrDefault();
    }

    #endregion

    private List<Equipo> equipos;
    private Unidad _objetoSeleccionado;

    public void Awake()
    {

        // Creamos nuestros 2 equipos iniciales.
        Equipo equipoAzul = new Equipo("Equipo Azul", Color.blue);
        Equipo equipoRojo = new Equipo("Equipo Rojo", Color.red);
        equipos = new List<Equipo>() { equipoAzul, equipoRojo };

        jugador1 = new Jugador();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var go = Mouse.GO;
            if (go != null)
            {
                if (go.layer == 10)
                {
                    ObjetoSeleccionado = go.GetComponent<Unidad>();
                }
                else
                {
                    ObjetoSeleccionado = null;
                }
            }
        }

        // Clic derecho
        if (Input.GetMouseButtonDown(1))
        {

            // Si hay alguna unidad seleccionada y esta es un movil.
            if (ObjetoSeleccionado)
            {
                if (ObjetoSeleccionado.CompareTag("Movil"))
                {
                    Movil movil = ObjetoSeleccionado.GetComponent<Movil>();

                    if (movil.TryGetComponent(out Recolector recolector))
                    {
                        var go = Mouse.GO;
                        Debug.Log(go.name);

                        if (go.CompareTag("Energia"))
                        {
                            recolector.Recolectar(go.GetComponent<Energia>());
                            return;
                        }
                    }

                    Vector3 posicion = Mouse.Posicion;

                    if (posicion != Vector3.zero)
                    {
                        movil.Moverse(posicion);

                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Mouse.DibujarRayo();
    }

    public Unidad ObjetoSeleccionado
    {
        get => _objetoSeleccionado;
        set
        {

            // Objecto null / Objeto que no es una unidad.
            if (value == null)
            {
                if (_objetoSeleccionado)
                {
                    _objetoSeleccionado.Seleccionado = false;
                }

                _objetoSeleccionado = null;
                return;
            }


            // Si se selecciono al mismo objeto.
            if (_objetoSeleccionado == value)
                return;
            else
            {
                if (_objetoSeleccionado)
                {
                    _objetoSeleccionado.Seleccionado = false;
                }
            }

            _objetoSeleccionado = value;
            if (_objetoSeleccionado.tag == "Movil")
            {
                Movil movil = _objetoSeleccionado.GetComponent<Movil>();
                movil.Seleccionado = true;
            }
        }
    }
}
