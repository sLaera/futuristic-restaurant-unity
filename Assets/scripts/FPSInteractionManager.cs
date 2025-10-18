using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using BurgerDomain.Classes;

public class FPSInteractionManager : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    [SerializeField] private float _interactionDistance;
    public LayerMask layerOggetti; // Il layer degli oggetti con cui interagire
    //Material materialeContorno = new Material(Shader.Find("Standard")); // Il materiale per l'effetto di contorno
    public Color coloreContorno = Color.yellow; // Il colore del contorno
    public float spessoreContorno = 0.1f;       // Lo spessore del contorno
    public float velocitaZoom = 2f;
    public float distanzaMinima = 1f;
    public float distanzaMassima = 5f;
    private Material materialeOriginale;
    private Material materialeOriginaleOriginal;
    public bool intosupermarket = false;
    public bool intorestaurant = false;
    private FirstPersonController _fpsController;
    //[SerializeField] private Image _target;

    //private Interactable _pointingInteractable;
    private PhysicsGrabbable _pointingGrabbable;
    private PhysicsGrabbableSupermarket _pointingGrabbable_s;

    //private CharacterController _fpsController;
    private Vector3 _rayOrigin;

    private PhysicsGrabbableSupermarket _grabbedObject_s = null;
    private PhysicsGrabbable _grabbedObject = null;


    void Start()
    {
        materialeOriginale = GetComponent<MeshRenderer>().material;
        materialeOriginaleOriginal = GetComponent<MeshRenderer>().material;
        _fpsController = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        _rayOrigin = _fpsCameraT.position + 0.5f * _fpsCameraT.forward;

        if (_grabbedObject == null)
            if (intosupermarket == false)
            {
                CheckInteraction();
            }
            else
            {
                CheckInteractionSupermarket();
            }

        if (_grabbedObject != null && Input.GetMouseButtonDown(1))
            Drop();
        if (_grabbedObject != null)
            RegolaDistanzaOggetto(_grabbedObject.transform);
        if (_grabbedObject_s != null && Input.GetMouseButtonDown(1))
            Drop_s();
        if (_grabbedObject_s != null)
            RegolaDistanzaOggetto(_grabbedObject_s.transform);

        if (_debugRay)
            DebugRaycast();
    }
    void RegolaDistanzaOggetto(Transform oggetto)
    {
        // Ottieni l'input della rotellina del mouse
        float inputRotellina = Input.GetAxis("Mouse ScrollWheel");

        // Calcola la nuova distanza in base all'input della rotellina del mouse
        float nuovaDistanza = Mathf.Clamp(oggetto.localPosition.z - inputRotellina * velocitaZoom, distanzaMinima, distanzaMassima);

        // Imposta la nuova posizione lungo l'asse z
        oggetto.localPosition = new Vector3(oggetto.localPosition.x, oggetto.localPosition.y, nuovaDistanza);
    }

    private void CheckInteraction()
    {
        Ray ray = new Ray(_rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        //quando premo il pulsante destro del mouse posso fare alcune interazioni
        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (Input.GetMouseButtonDown(0) && _fpsController.is_cooking)
            {
                if (hit.collider.CompareTag("bottone"))
                {
                    GameObject bottoneCassa = hit.collider.gameObject;
                    bottoneCassa.GetComponent<ScriptCassa>().AcquistaCarrello();
                }
                else if (hit.collider.CompareTag("campanello"))
                {
                    GameObject campanella = hit.collider.gameObject;
                    campanella.GetComponent<Bell>().SuonaCanzone();
                }
                
                var grabbable = hit.transform.GetComponent<PhysicsGrabbable>();//mi da il grabbable dell'oggetto
                if (hit.collider.CompareTag("salsa"))
                {
                    _pointingGrabbable = grabbable;
                    Grab(grabbable, ray, hit);
                }
                if (grabbable != null)
                {
                    bool isPossibleToGrab = false;
                    //poso grubbare il piatto se ha ingredienti
                    if (hit.collider.CompareTag("piatto"))
                    {
                        //Posso prendere il piattop se ha ingredienti
                        isPossibleToGrab = true;
                    }else if ((hit.collider.CompareTag("pane") || hit.collider.CompareTag("ingrediente")) && !grabbable.into_piatto)
                    {
                        //Posso prendere il pane o un ingrediente se non è nel piatto
                        isPossibleToGrab = true;
                    }

                    if (isPossibleToGrab)
                    {
                        _pointingGrabbable = grabbable;
                        Grab(grabbable, ray, hit);
                        //se e' spowner duplica se stesso e si rende grabbable e gre un elemento spowner al suo posto solo se e' possibile prendere altri ingredienti dello stesso tipo
                        if (grabbable.IsSpawner)
                        {
                            grabbable.IsSpawner = false; //ora l'oggetto è gubbato non e' piu' uno spawner
                            // prendo l'oggetto. se posso prendere ancora allora verra' duplicato nei conrtolli dopo
                            if (hit.collider.CompareTag("pane"))
                            {
                                //il pane puo' essere preso infinite volte
                                var obj = grabbable.Duplicate().GetComponent<PhysicsGrabbable>(); //il duplicato diventa lo spawner e lo spawner diventa l'oggetto preso
                                obj.IsSpawner = true;
                            }else if (hit.collider.CompareTag("ingrediente"))
                            {
                                var ingrediente = grabbable.GetComponent<IngredientInfo>();
                                //posso prendere gli ingredienti solo se ce ne sono ancora. Quando sono finiti non posso fare piu nulla
                                var ownedIngredient = GameManager.Instance.Player
                                    .CurrentOwnedIngredients
                                    .Find(i => i.GetComponent<IngredientInfo>().Type == ingrediente.Type)?
                                    .GetComponent<IngredientInfo>();
                                var obj = grabbable.Duplicate().GetComponent<PhysicsGrabbable>(); //il duplicato diventa lo spawner e lo spawner diventa l'oggetto preso
                                obj.IsSpawner = true;
                                obj.gameObject.SetActive(ownedIngredient?.Quantity != null && ownedIngredient?.Quantity > 1); // se sono finiti non mostro più null
                                ownedIngredient.Quantity--;//decremento la quantità
                                // aggiornamento display
                                if (ownedIngredient.Quantity == 0)
                                {
                                    for (int i = 0; i < GameManager.Instance.ListaStringhe.Count; i++)
                                    {
                                        if (GameManager.Instance.ListaStringhe[i] == ownedIngredient.Type)
                                        {
                                            if (GameManager.Instance.Listaqta[i] > 1)
                                            {
                                                GameManager.Instance.Listaqta[i] -= 1;
                                            }
                                            else
                                            {
                                                GameManager.Instance.Listaqta.RemoveAt(i);
                                                GameManager.Instance.ListaStringhe.RemoveAt(i);
                                                GameManager.Instance.Listaprezzi.RemoveAt(i);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    
                                }

                            }
                        }
                    }
                }
            }

            //Raycast Interactive è un modo generico per interagire con oggetti con i raycast
            if (RaycastInteractable.IsACauseActive())
            {
                RaycastInteractable.CheckInteractionAndInvokeAction(hit.collider.gameObject);
            }

        }
        //If NOTHING is detected set all to null
        else
        {
            //_pointingInteractable = null;
            //DisattivaContorno(_pointingGrabbable);
            _pointingGrabbable = null;

        }
    }

    private void CheckInteractionSupermarket()
    {
        Ray ray = new Ray(_rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (hit.collider.CompareTag("BuyButton"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject.FindGameObjectWithTag("BuyButton").GetComponent<PagaButton>().Buy();
                }
            }
            if (hit.collider.CompareTag("remove_button"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bottoneRemove = GameObject.FindWithTag("remove_button");
                    bottoneRemove.GetComponent<RemoveButton>().Remove();
                }
            }
            if (hit.collider.CompareTag("add_button"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bottoneAdd = GameObject.FindWithTag("add_button");
                    bottoneAdd.GetComponent<AddButton>().Add();
                }
            }
            if (hit.collider.CompareTag("skip_button"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bottoneSkip = GameObject.FindWithTag("skip_button");
                    bottoneSkip.GetComponent<SkipButton>().Skip();
                }
            }

            _pointingGrabbable_s = hit.transform.GetComponent<PhysicsGrabbableSupermarket>();
            if (_grabbedObject_s == null && _pointingGrabbable_s)
            {
                //AttivaContorno(_pointingGrabbable);
                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log("grabbo");
                    _pointingGrabbable_s.duplicate(_pointingGrabbable_s.transform.position);
                    Grab_s(_pointingGrabbable_s, ray, hit);

                }

            }
        }
        //If NOTHING is detected set all to null
        else
        {
            //_pointingInteractable = null;
            //DisattivaContorno(_pointingGrabbable);
            _pointingGrabbable_s = null;

        }
    }



    private void Drop()
    {
        if (_grabbedObject == null)
            return;

        _grabbedObject.transform.parent = _grabbedObject.OriginalParent; // fa tornare al suo parent originale
        _grabbedObject.Drop();
        _grabbedObject = null;
    }
    private void Drop_s()
    {
        if (_grabbedObject_s == null)
            return;
        _grabbedObject_s.transform.parent = _grabbedObject_s.OriginalParent; // fa tornare al suo parent originale
        _grabbedObject_s.Drop();
        _grabbedObject_s = null;
    }

    private void Grab_s(PhysicsGrabbableSupermarket grabbable, Ray ray, RaycastHit hit)
    {
        _grabbedObject_s = grabbable;
        //Vector3 nuovaPosizione = hit.point - ray.direction * 1f;
        //grabbable.duplicate(_grabbedObject_s.transform.position);
        //hit.transform.position = nuovaPosizione;
        _pointingGrabbable_s.Grab(gameObject);
        grabbable.transform.SetParent(_fpsCameraT); // fa diventare figlio della camera

    }
    private void Grab(PhysicsGrabbable grabbable, Ray ray, RaycastHit hit)
    {
        //faccio grab solo se non sto già grabbando qualcosa
        if (_grabbedObject == null && grabbable != null)
        {
            _grabbedObject = grabbable;
            //Vector3 nuovaPosizione = hit.point - ray.direction * 1f;
            //hit.transform.position = nuovaPosizione;
            _pointingGrabbable.Grab(gameObject);
            grabbable.transform.SetParent(_fpsCameraT); // fa diventare figlio della camera
        }
    }
    /*
    void AttivaContorno(PhysicsGrabbable  grabbable)
    {
        // Attiva l'oggetto
        _grabbedObject = grabbable;
        materialeOriginale=grabbable.GetComponent<MeshRenderer>().material;
        materialeOriginaleOriginal=grabbable.GetComponent<MeshRenderer>().material;
        // Imposta il materiale di contorno sull'oggetto colpito
        //Material contornoMaterial = new Material(materialeContorno);
        materialeOriginale.SetColor("_OutlineColor", coloreContorno);
        materialeOriginale.SetFloat("_Outline", spessoreContorno);
        grabbable.GetComponent<MeshRenderer>().material = materialeOriginale;
    }
    void DisattivaContorno(PhysicsGrabbable  grabbable)
    {
        // Disattiva l'oggetto
        if (materialeOriginale != null)
        {
            _grabbedObject = grabbable;

            // Ripristina il materiale originale dell'oggetto
            GetComponent<MeshRenderer>().material = materialeOriginaleOriginal;

            // Azzera il materiale originale
            materialeOriginale = null;
        }
    }*/
    private void DebugRaycast()
    {
        Debug.DrawRay(_rayOrigin, _fpsCameraT.forward * _interactionDistance, Color.red);
    }
}
