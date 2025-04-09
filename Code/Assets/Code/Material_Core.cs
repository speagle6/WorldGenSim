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
    public int top_speed;

    //flow options
    public bool flow; //is it enabled
    public int height_diff_min; //minimum difference in height to flow to
    public float vel_per_dif; // how much velocity it gains depending on the difference
    public float vel_loss; //helps vel tend towards zero (keep a low number)
    public float flow_y_plus_mod; // capacity to flow in this axis, 0 is no flow, 1 is normal rate
    public float flow_y_neg_mod; // capacity to flow in this axis, 0 is no flow, 1 is normal rate
    public float flow_x_plus_mod; // capacity to flow in this axis, 0 is no flow, 1 is normal rate
    public float flow_x_neg_mod; // capacity to flow in this axis, 0 is no flow, 1 is normal rate
    public float flow_z_plus_mod; // capacity to flow in this axis, 0 is no flow, 1 is normal rate
    public float flow_z_neg_mod; // capacity to flow in this axis, 0 is no flow, 1 is normal rate


    void Start()
    {
        node_handler = this.transform.parent.GetComponent<Node_Core>();
    }

    public void Refresh()
    {
        handle_vel();
        if (gravity) {handle_grav();}
        if (flow) {handle_flow();}
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
            node_target.Add_Resource(this.name, functional_vel, vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        } else if (vel_x <0)
        {
            functional_vel = Mathf.Min(Mathf.Abs(vel_x), Quantity);
            var node_target = node_handler.X_neg_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel, vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        }

        // handle y
        int vel_y = (int) vel.y;
        if (vel_y > 0)
        {
            functional_vel = Mathf.Min(vel_y, Quantity);
            var node_target = node_handler.Y_plus_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel, vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        } else if (vel_y <0)
        {
            functional_vel = Mathf.Min(Mathf.Abs(vel_y), Quantity);
            var node_target = node_handler.Y_neg_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel, vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        }

        // handle z
        int vel_z = (int) vel.z;
        if (vel_z > 0)
        {
            functional_vel = Mathf.Min(vel_z, Quantity);
            var node_target = node_handler.Z_plus_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel, vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        } else if (vel_z <0)
        {
            functional_vel = Mathf.Min(Mathf.Abs(vel_z), Quantity);
            var node_target = node_handler.Z_neg_Adj.GetComponent<Node_Core>();
            functional_vel = Mathf.Min(functional_vel, node_target.Check_Space());
            node_target.Add_Resource(this.name, functional_vel, vel);
            node_handler.Remove_Resource(this.name, functional_vel);
        }
    }

    void handle_grav()
    {
        if (rigid)
        {
            float adj_mat_total = 0;
            List<Material_Core> resources_to_check = new List<Material_Core>();
            foreach (Transform child in node_handler.X_plus_Adj.transform)
            {
                resources_to_check.Add(child.gameObject.GetComponent<Material_Core>());
            }
            foreach (Transform child in node_handler.X_neg_Adj.transform)
            {
                resources_to_check.Add(child.gameObject.GetComponent<Material_Core>());
            }
            foreach (Transform child in node_handler.Y_plus_Adj.transform)
            {
                resources_to_check.Add(child.gameObject.GetComponent<Material_Core>());
            }
            foreach (Transform child in node_handler.Y_neg_Adj.transform)
            {
                resources_to_check.Add(child.gameObject.GetComponent<Material_Core>());
            }
            foreach (Transform child in node_handler.Z_plus_Adj.transform)
            {
                resources_to_check.Add(child.gameObject.GetComponent<Material_Core>());
            }
            foreach (Transform child in node_handler.Z_neg_Adj.transform)
            {
                resources_to_check.Add(child.gameObject.GetComponent<Material_Core>());
            }

            foreach (Material_Core material in resources_to_check)
            {
                if (material.rigid)
                {
                    if (rigid_same_only)
                    {
                        if (material.gameObject.name == gameObject.name)
                        {
                            adj_mat_total += material.Quantity;
                        }
                    }
                    else
                    {
                        adj_mat_total += material.Quantity;
                    }
                    if (adj_mat_total * support_per_adj > Quantity * support_need_per)
                    {
                        vel.z -= grav_accel;
                    }
                }
            }
        }
        else
        {
            vel.z -= grav_accel;
        }
        if (node_handler.Z_neg_Adj.GetComponent<Node_Core>().Check_Space() == 0)
        {
            vel.z = 0;
        }
        if (vel.z > -top_speed)
        {
            vel.z = -top_speed;
        }
    }

    void handle_flow()
    {
        vel.y = calculate_new_vel(node_handler.Y_plus_Adj.GetComponent<Node_Core>(), flow_y_plus_mod, node_handler.Y_neg_Adj.GetComponent<Node_Core>(), flow_y_neg_mod, vel.y);
        vel.x = calculate_new_vel(node_handler.X_plus_Adj.GetComponent<Node_Core>(), flow_x_plus_mod, node_handler.X_neg_Adj.GetComponent<Node_Core>(), flow_x_neg_mod, vel.x);
        vel.z = calculate_new_vel(node_handler.Z_plus_Adj.GetComponent<Node_Core>(), flow_z_plus_mod, node_handler.Z_neg_Adj.GetComponent<Node_Core>(), flow_z_neg_mod, vel.z);
    }

    float calculate_new_vel(Node_Core pos_node, float pos_direction_mod, Node_Core neg_node, float neg_direction_mod, float current_vel)
    {
        float vel_change = 0;
        if (!(pos_node.NodeTotalFill - height_diff_min < 0 && vel.z < 0))
        {
            vel_change += ((pos_node.NodeTotalFill - height_diff_min) * vel_per_dif) * pos_direction_mod;
        }
        if (!(neg_node.NodeTotalFill - height_diff_min < 0 && vel.z > 0))
        {
            vel_change -= ((neg_node.NodeTotalFill - height_diff_min) * vel_per_dif) * neg_direction_mod;
        }
        vel_change *= 1 - vel_loss;

        return current_vel + vel_change;
    }
}
