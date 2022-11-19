using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movil : Unidad
{

    [Header("Atributos Movil")]
    [SerializeField] private float _velocidadMovimiento;

    private NavMeshAgent agent;

    private void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

        VelocidadMovimiento = _velocidadMovimiento;
    }

    public void Moverse(Vector3 posicion)
    {
        agent.stoppingDistance = 0f;
        agent.SetDestination(posicion);
    }

    public void Moverse(Vector3 posicion, float distancia)
    {
        agent.stoppingDistance = distancia;
        agent.SetDestination(posicion);
    }

    public void Parar()
    {
        agent.stoppingDistance = 0;
        agent.SetDestination(transform.position);
    }

    public float VelocidadMovimiento
    {
        get => _velocidadMovimiento;
        set
        {
            _velocidadMovimiento = value;
            agent.speed = value;
        }
    }
}
