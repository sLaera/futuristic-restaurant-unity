using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GattoFollow : MonoBehaviour
{
    public NavMeshAgent gatto;
    public Transform player;
    public Animator aiAnim;
    public Transform firstPersonController;

    public bool start_voice = true;
    // Start is called before the first frame update
    void Start()
    {
        start_voice = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<Player>().GetComponent<FirstPersonController>().isWalking)
        {
            gatto.SetDestination(player.position);

            Vector3 forward = firstPersonController.forward;
            Quaternion rotation = Quaternion.Euler(0, -90, 0);
            forward = rotation * forward;
            transform.forward = forward;
            
            if (start_voice && Math.Abs(Vector3.Distance(FindObjectOfType<RobotEmpty>().transform.position, transform.position)) <= 1)
            {
                Debug.Log("parla");
                GetComponent<RobotAudioScript>().update_voice();
                start_voice = false;
            }

            if (!gatto.pathPending)
            {
                if (gatto.remainingDistance <= (gatto.stoppingDistance))
                {
                    aiAnim.ResetTrigger("TriggerWalking");
                    aiAnim.SetTrigger("TriggerIdle");
                }
            }
            else
            {
                aiAnim.ResetTrigger("TriggerIdle");
                aiAnim.SetTrigger("TriggerWalking");
            }
        }
    }
}
