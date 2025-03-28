using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Material_Core : MonoBehaviour
{
    // core material variables
    public int Quantity;
    public Vector3 vel;
    Node_Core node_handler;

    //gravity options
    public bool gravity; //is it enabled
    public float grav_accel; //acceleration per tick from gravity
    public bool rigid; //is rigid enabled
    public float support_per_adj; //support adjacent materials provide
    public float support_need_per; //support needed for every unit of material
    public bool rigid_same_only; // can it be supported by other rigid materials.

    //flow options
    public bool flow; //is it enabled
    public int height_diff_min; //minimum difference in height to flow to
    public float vel_loss; // loss of velocity everytime it moves
    public float vel_per_dif; // how much velocity it gains depending on the difference
    public bool flow_y_plus; // can it flow in this axis
    public bool flow_y_plus_mod;
    public bool flow_y_neg; // can it flow in this axis
    public bool flow_z_plus; // can it flow in this axis
    public bool flow_z_neg; // can it flow in this axis
    public bool flow_x_plus; // can it flow in this axis
    public bool flow_x_neg; // can it flow in this axis


    void Start()
    {
        node_handler = this.transform.parent.GetComponent<Node_Core>();
    }

    public void Refresh()
    {
        handle_vel();
    }

    void handle_vel()
    {
        //vel setup
        int functional_vel = 0;

        // handle x
        int vel_x = (int) vel.x;
        if (vel_x > 0)
        {
            functional_vel = Mathf.Min(vel_x, Quantity);
            var node_target = node_handler.X_plus_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        } else if (vel_x <0)
        {
            functional_vel = Mathf.Min(Mathf.Abs(vel_x), Quantity);
            var node_target = node_handler.X_neg_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        }

        // handle y
        int vel_y = (int) vel.y;
        if (vel_y > 0)
        {
            functional_vel = Mathf.Min(vel_y, Quantity);
            var node_target = node_handler.Y_plus_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        } else if (vel_y <0)
        {
            functional_vel = Mathf.Min(Mathf.Abs(vel_y), Quantity);
            var node_target = node_handler.Y_neg_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        }

        // handle z
        int vel_z = (int) vel.z;
        if (vel_z > 0)
        {
            functional_vel = Mathf.Min(vel_z, Quantity);
            var node_target = node_handler.Z_plus_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        } else if (vel_z <0)
        {
            functional_vel = Mathf.Min(Mathf.Abs(vel_z), Quantity);
            var node_target = node_handler.Z_neg_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        }
    }


}
