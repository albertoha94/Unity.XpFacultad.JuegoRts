using UnityEngine;

public class Equipo
{

    // Auto Id
    private static short autoId = 0;

    //Atributos
    public readonly short id;
    public readonly string nombre;
    public readonly Color color;

    public Equipo(string nombre, Color color)
    {
        this.id = autoId++;
        this.nombre = nombre;
        this.color = color;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
