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
            jugador1.energiaR = value * 100;
            HUD_Energias.R.text = jugador1.energiaR.ToString();
        }
    }
    public static float EnergiaG
    {
        get => jugador1.energiaG;
        set
        {
            jugador1.energiaG = value * 100;
            HUD_Energias.G.text = jugador1.energiaG.ToString();
        }
    }
    public static float EnergiaB
    {
        get => jugador1.energiaB;
        set
        {
            jugador1.energiaB = value * 100;
            HUD_Energias.B.text = jugador1.energiaB.ToString();
        }
    }

    #endregion

    #region Equipos

    private static List<Equipo> equipos;

    public static Equipo GetEquipo(int indice)
    {
        // prevenir que existe el indice.
        if (indice > equipos.Count)
        {
            return null;
        }
        return equipos[indice];
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
        EnergiaR = jugador1.energiaR;
        EnergiaG = jugador1.energiaG;
        EnergiaB = jugador1.energiaB;
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

                // Si la unidad seleccionada es un movil.
                if (ObjetoSeleccionado.CompareTag("Movil"))
                {
                    Movil movil = ObjetoSeleccionado.GetComponent<Movil>();

                    // Revisamos si es un recolector.
                    if (movil.TryGetComponent(out Recolector recolector))
                    {

                        // Obtenemos el objecto al que le dimos clic derecho.
                        var go = Mouse.GO;
                        Debug.Log(go.name);

                        // Revisamos si es energia.
                        if (go.CompareTag("Energia"))
                        {

                            // Le pasamos la referencia de la energia.
                            recolector.Recolectar(go.GetComponent<Energia>());

                            // Si se va a recolectar energia, terminamos la funcion actual.
                            return;
                        }
                    }
                    else if (movil.TryGetComponent(out UnidadAtaque unidadAtaque))
                    {
                        var go = Mouse.GO;

                        // Revisamos si nuestro target es una unidad.
                        if (go.TryGetComponent(out Unidad target))
                        {
                            // Si no son del mismo equipo...
                            if (unidadAtaque.movil.Equipo.id != target.Equipo.id)
                            {

                            }
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

            if (_objetoSeleccionado)
            {
                _objetoSeleccionado.Seleccionado = false;
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
