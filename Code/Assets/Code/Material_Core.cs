using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Material_Core : MonoBehaviour
{
    public int Quantity;
    public Vector3 vel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        List<Component> scripts = new List<Component>(this.GetComponents(typeof(Component)));
        scripts.RemoveAt(0);
        scripts.RemoveAt(0);
    }
}
