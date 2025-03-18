using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Node_Core : MonoBehaviour
{
    // stores adajacent nodes
    public GameObject Y_plus_Adj; // UP
    public GameObject Y_neg_Adj; // DOWN
    public GameObject X_plus_Adj; // EAST
    public GameObject X_neg_Adj; // WEST
    public GameObject Z_plus_Adj; // NORTH
    public GameObject Z_neg_Adj; // SOUTH

    // stores current fill level of the node
    public int NodeTotalFill;
    public int NodeFillCap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Add_Resource("Dirt", 1);
    }

    GameObject Get_Resource(string name)
    {
        return(transform.Find(name).gameObject);
    }

    int Check_Space(int quantity)
    {
        return (NodeFillCap - NodeTotalFill) - quantity;
    }

    // Update is called once per frame
    void Add_Resource(string name, int quantity)
    {
        var Resource = transform.Find(name);
        if (!Resource)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Materials/" + name + ".prefab", typeof(GameObject));
            GameObject NewRes = (GameObject) Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
            NewRes.transform.parent = transform;
            NewRes.GetComponent<Material_Core>().Quantity = quantity;
            NewRes.name = name;
            NodeTotalFill += quantity;
        }
        else
        {
            Resource.gameObject.GetComponent<Material_Core>().Quantity += quantity;
            NodeTotalFill += quantity;
        }
    }

    void Remove_Resource(string name, int quantity)
    {
        var Resource = transform.Find(name).gameObject;
        Resource.GetComponent<Material_Core>().Quantity -= quantity;
        NodeTotalFill -= quantity;
        if (Resource.GetComponent<Material_Core>().Quantity == 0)
        {
            Destroy(Resource);
        }
    }

    public void Refresh()
    {
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<Material_Core>().Refresh();
        }
    }
}
