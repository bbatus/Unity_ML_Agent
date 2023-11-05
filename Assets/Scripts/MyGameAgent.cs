using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MyGameAgent : Agent
{
    public Renderer ground;
    public Transform target;
    public float speed = 1.0f;


    public override void OnActionReceived(ActionBuffers actions)
    {
        float x = actions.ContinuousActions[0];
        float z = actions.ContinuousActions[1];

        Debug.Log("Action x: " + x + "Action z :" + z);
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(transform.localPosition.z);//3 float xyz
        sensor.AddObservation(target.localPosition.x);
        sensor.AddObservation(target.localPosition.y);
        sensor.AddObservation(target.localPosition.z); //3 float xyz
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            Debug.Log("duvara carpti");
            AddReward(-1);
            EndEpisode();
            ground.material.color = Color.red;
        }

        if(other.tag == "Target")
        {
            Debug.Log("basarili");
            AddReward(1);
            EndEpisode();
            ground.material.color = Color.green;
        }
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("episode basladi");
        transform.localPosition = new Vector3(2, 1, -3);
    }
}
