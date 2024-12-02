using UnityEngine;
using System.Collections.Generic;

public class Node_Core : MonoBehaviour
{
    // stores adajacent nodes
    public GameObject North_Adj;
    public GameObject South_Adj;
    public GameObject West_Adj;
    public GameObject East_Adj;
    public GameObject Above_Adj;
    public GameObject Below_Adj;
    // stores list of all materials
    public List<Material_Core> materials = new List<Material_Core>();
    // stores current fill level of the node
    public int NodeTotalFill;
    public int NodeFillCap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
