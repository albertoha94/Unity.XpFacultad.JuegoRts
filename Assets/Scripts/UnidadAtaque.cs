using System;
using System.Collections;
using UnityEngine;

public class UnidadAtaque : Unidad
{

    // Inspector
    public int damage;
    [SerializeField] TriggerAtaque triggerAtaque;

    // Componentes
    internal Movil movil;
    private Unidad _target;

    private void Awake()
    {
        movil = GetComponent<Movil>();
        triggerAtaque.damage = damage;
    }

    internal void OnVisionEnter(Unidad unidad)
    {
        if (unidad)
            Target = unidad;
    }

    internal void OnVisionExit(Unidad unidad)
    {
        if (unidad == Target)
            Target = null;
    }

    public IEnumerator OnPerseguir()
    {
        while (Target)
        {
            movil.Moverse(Target.transform.position, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public Unidad Target
    {
        get { return _target; }
        set
        {
            if (!_target)
            {
                _target = value;
                StartCoroutine(OnPerseguir());
            }
        }
    }
}
