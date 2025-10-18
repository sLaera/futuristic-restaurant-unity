using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using BurgerDomain;
using BurgerDomain.Classes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PhysicsGrabbable : Grabbable
{
    public GameObject PlatePosition;
    public GameObject mioPrefab;
    //private  id;
    private Rigidbody _rigidbody;
    private Collider _collider;
    public Guid uid { get; set; }

    [Header("se true allora l'oggetto non e' grabbabile in se ma genera oggeti grabbabili")]
    public bool IsSpawner;
    
    public bool into_piatto { get; set; }
    public bool Isgrabbed { get; set; }

    protected override void Start() // devo fare ovverride del grabable
    {
        base.Start(); // devo chiamare la start originale
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        into_piatto = false;
        uid = Guid.NewGuid();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Creazionepanino piatto = FindObjectOfType<Creazionepanino>();
        Rigidbody rigidBodyCollidedObject = collision.gameObject.GetComponent<Rigidbody>();
        if (piatto == null)// non c'è piatto allora non si fa nulla
        {
            return;
        }
        
        //se è preso allora non devo controllare i trigger
        if (Isgrabbed)
        {
            return;
        }
        var mustAddToList = false;//flag che indica che l'oggetto deve essere aggiunto alla lista
        //un piatto è physich grabbable. se è un piatto allora può colidere solo con il pane
        if (CompareTag("piatto"))
        {
            // Se collide con un pane allora lo posizione sul piatto. Inizia la creazione del panino. I successivi ingredienti sono gestiti da physucsGrabbable
            if (collision.gameObject.CompareTag("pane") && collision.gameObject.GetComponent<PhysicsGrabbable>() != null && !collision.gameObject.GetComponent<PhysicsGrabbable>().IsSpawner)
            {
                mustAddToList = true;
            }
        }else if (!CompareTag("piatto") && into_piatto)//se l'oggetto corrente è un ingrediente o un pane ed è nel piatto
        {
            // Verifica se l'oggetto con cui abbiamo colliso ha il tag "ingrediente" o pane
            if (collision.gameObject.GetComponent<PhysicsGrabbable>() != null && !collision.gameObject.GetComponent<PhysicsGrabbable>().IsSpawner && (collision.gameObject.CompareTag("ingrediente") || collision.gameObject.CompareTag("pane")))
            {
                // Ora puoi accedere alla variabile pubblica dell'oggetto con cui hai colliso
                List<GameObject> ingredientiInPiatto = piatto.ListaIngredienti;
                bool isAlreadyInPiatto = false;
                // controllo che sia nel piatto
                // se non è già nel piatto metto l'oggetto nella lista
                foreach (var ingrediente in ingredientiInPiatto)
                {
                    if (ingrediente.GetComponent<PhysicsGrabbable>().uid ==
                        collision.gameObject.GetComponent<PhysicsGrabbable>().uid)
                    {
                        isAlreadyInPiatto = true;
                        break;
                    }
                }
                
                if (!isAlreadyInPiatto)
                {
                    mustAddToList = true;
                }
            }
        }
        if (mustAddToList)
        {
            piatto.ListaIngredienti.Add(collision.gameObject);
            //se è stato aggiunto alla lista allora deve essere bloccato dove sta
            collision.gameObject.GetComponent<PhysicsGrabbable>().into_piatto = true;
            collision.gameObject.GetComponent<PhysicsGrabbable>().SetKinematicInTime(0.5f);
            /*
            rigidBodyCollidedObject.isKinematic = true;
            collision.gameObject.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, collision.gameObject.transform.localRotation.y, 0)), 0.3f);
            //-------Posizionare l'oggetto sopra l'altro------
            Bounds boundsOggettoSotto = gameObject.GetComponent<BoxCollider>().bounds;
            float newY = boundsOggettoSotto.max.y + collision.gameObject.GetComponent<BoxCollider>().bounds.extents.y;
            var position = collision.gameObject.transform.position;
            position = new Vector3(position.x, newY, position.z);
            collision.gameObject.transform.position = position;
            */
        }
    }

    public override void Grab(GameObject grabber)
    {
        //if (List<Ingredient> g=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentOwnedIngredients)
        {
            if (CompareTag("salsa"))
            {
                transform.rotation = Quaternion.Euler(90, 0, 0);
                //transform.GetChild(0).transform.Rotate(0, 0, -90);
            }

            Isgrabbed = true;
            _collider.enabled = false;
            _rigidbody.isKinematic = true; // lo rendocinematico cosi posso spostare tutto
        }
    }

    public GameObject Duplicate()
    {
        //crea un duplicato dell'oggetto nella stessa posizione
        var nuovaIstanza = Instantiate(mioPrefab, transform.position, Quaternion.identity);
        nuovaIstanza.gameObject.transform.localScale = new Vector3(transform.localScale.x*1.099f, transform.localScale.y*1.099f, transform.localScale.z*1.099f);
        nuovaIstanza.gameObject.GetComponent<PhysicsGrabbable>().uid = Guid.NewGuid(); // in modo che gli id siano differenti
        nuovaIstanza.gameObject.GetComponent<PhysicsGrabbable>().IsSpawner = IsSpawner;
        nuovaIstanza.GetComponent<Collider>().enabled = true;
        nuovaIstanza.GetComponent<PhysicsGrabbable>().into_piatto = false;
        return nuovaIstanza;
    }

    public override void Drop()
    {
        if (CompareTag("salsa"))
        {
            transform.Rotate(-90, 0, 0);
            //transform.GetChild(0).transform.Rotate(0, 0, 90);
        }
        Isgrabbed = false;
        _collider.enabled = true;
        _rigidbody.isKinematic = false; // tolgo tutto
        //se è un piatto allora torna alla posizione di partenza
        if (CompareTag("piatto"))
        {
            _rigidbody.isKinematic = true;//il piatto e' semopre kinematic
            transform.DOMove(PlatePosition.transform.position,0.8f);
        }
    }

    public void SetKinematicInTime(float time)
    {
        Invoke(nameof(SetKinematic), time);
    }

    public void SetKinematic()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Creazionepanino piatto = FindObjectOfType<Creazionepanino>();
        transform.SetParent(piatto.transform); //l'oggetto diventa filgio del piatto
    }
}
