using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using BurgerDomain;
using BurgerDomain.Classes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PhysicsGrabbableSupermarket : Grabbable
{
    //private  id;
    private Rigidbody _rigidbody;
    private Collider _collider;
    public Guid uid;
    public GameObject mioPrefab;
    public bool into_piatto { get; set; }
    private bool isgrabbed { get; set; }
    private bool is_duplicable = true;
    protected override void Start () // devo fare ovverride del grabable
    {
        base.Start(); // devo chiamare la start originale
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        into_piatto = false;
        uid = Guid.NewGuid();

    }

    private void OnCollisionEnter(Collision collision)
    {
 

        if (!isgrabbed)
        {
            
                
        }
    }

    public override void Grab(GameObject grabber)
    {
        is_duplicable = false;
        isgrabbed = true;
        _collider.enabled = false;
        _rigidbody.isKinematic = true; // lo rendocinematico cosi posso spostare tutto
        
    }

    public void duplicate (Vector3 v)
    {
        if (is_duplicable)
        {
            GameObject nuovaIstanza = Instantiate(mioPrefab, v, Quaternion.identity);
            nuovaIstanza.gameObject.GetComponent<PhysicsGrabbableSupermarket>().uid = Guid.NewGuid();
            nuovaIstanza.transform.position = v;
            nuovaIstanza.GetComponent<Collider>().enabled = true;
            // in modo che gli id siano differenti
        }

    }

    public override void Drop()
    {
        isgrabbed = false;
        _collider.enabled = true;
        _rigidbody.isKinematic = false; // tolgo tutto
    }
}
