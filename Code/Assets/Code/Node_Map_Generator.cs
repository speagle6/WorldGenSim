using UnityEngine;
using System.Collections.Generic;

public class Node_Map_Generator : MonoBehaviour
{
    public GameObject NodePrefab;
    public Vector3 Size;
    public float update_freq;

    private GameObject newObject;
    private GameObject YObj;
    private GameObject XObj;
    private GameObject ZObj;


    private List<GameObject> objects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                for (int z = 0; z < Size.z; z++)
                {
                    newObject = Instantiate(NodePrefab, new Vector3(x,y,z), Quaternion.identity);
                    newObject.name = "X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + z.ToString();
                    YObj = GameObject.Find("X: " + x.ToString() + " Y: " + (y-1).ToString() + " Z: " + z.ToString());
                    XObj = GameObject.Find("X: " + (x-1).ToString() + " Y: " + y.ToString() + " Z: " + z.ToString());
                    ZObj = GameObject.Find("X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + (z-1).ToString());
                    if (YObj != null)
                    {
                        newObject.GetComponent<Node_Core>().Y_neg_Adj = YObj;
                        YObj.GetComponent<Node_Core>().Y_plus_Adj = newObject;
                    }
                    if (XObj != null)
                    {
                        newObject.GetComponent<Node_Core>().X_neg_Adj = XObj;
                        XObj.GetComponent<Node_Core>().X_plus_Adj = newObject;
                    }
                    if (ZObj != null)
                    {
                        newObject.GetComponent<Node_Core>().Z_neg_Adj = ZObj;
                        ZObj.GetComponent<Node_Core>().Z_plus_Adj = newObject;
                    }
                    objects.Add(newObject);
                }
            }
        }

        InvokeRepeating("Refresh", 2.0f, update_freq);
    }

    void Refresh()
    {
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Node_Core>().Refresh();
        }
    }

}
