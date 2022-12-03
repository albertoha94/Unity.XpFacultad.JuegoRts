using UnityEngine;

public static class Mouse
{

    public static GameObject GO
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                return hit.collider.gameObject;
            }
            return null;
        }
    }

    public static Vector3 Posicion
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                return hit.point;
            return Vector3.zero;
        }
    }

    public static void DibujarRayo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            Debug.DrawLine(Camera.main.transform.position, hit.point);
    }
}
