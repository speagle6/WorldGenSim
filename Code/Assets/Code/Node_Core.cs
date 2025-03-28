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
    

    GameObject Get_Resource(string name)
    {
        //gets resources by name
        return(transform.Find(name).gameObject);
    }

    public int Check_Space()
    {
        //allows other things to check if there is space
        return NodeFillCap - NodeTotalFill;
    }

    public void Add_Resource(string name, int quantity)
    {
        //adds resources based on name
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

    public void Remove_Resource(string name, int quantity)
    {
        //removes resources based on name
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
        // handles each refresh loop
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<Material_Core>().Refresh();
        }
    }
}
