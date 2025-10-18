using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CauseOfActivation
{
    LeftMouseButton,
    RightMouseButton,
    None
}
public class RaycastInteractable : MonoBehaviour
{
    public List<CauseOfActivation> PossibleActionButtons;
    public CauseOfActivation CauseOfActivation { get; private set; }

    private void Start()
    {
        CauseOfActivation = CauseOfActivation.None;
    }

    public void SetCause()
    {
        CauseOfActivation = CauseOfActivation.None;
        
        if (Input.GetMouseButton(0))
        {
            CauseOfActivation = CauseOfActivation.LeftMouseButton;
        }else if (Input.GetMouseButton(1))
        {
            CauseOfActivation = CauseOfActivation.RightMouseButton;
        }
    }

    /// <summary>
    /// controlla se è stato premuto almeno uno dei pulsanti dell'enum
    /// </summary>
    /// <returns></returns>
    public static bool IsACauseActive()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButton(1);
    }

    public static void CheckInteractionAndInvokeAction(GameObject obj)
    {
        //l'ogetto potrebbe essere interactable. Seleziono il componente. se non è nullo allora è interagibile
        var interactable = obj.GetComponent<RaycastInteractable>();
        if (interactable != null)
        {
            interactable.SetCause();
            if (interactable.PossibleActionButtons.Contains(interactable.CauseOfActivation))
            {
                GameManager.InvokeRaycastInteract(interactable);
            }
        }
    }
}
