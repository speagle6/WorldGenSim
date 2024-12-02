using UnityEngine;

public class Node_Map_Generator : MonoBehaviour
{
    public Vector3 Size;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                for (int z = 0; z < Size.z; z++)
                {
                    GameObject objToSpawn = new GameObject("test");
                    objToSpawn.AddComponent<Node_Core>();
                    objToSpawn.transform.position = new Vector3(x, y, z);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
