using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ARButtonManager : MonoBehaviour
    {
        public static event Action<GameObject> OnArButtonClicked;
        public static ARButtonManager Instance { get; private set; }
        private int StepIndex { get; set; }
    
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this;
            }
        }

        private void Start()
        {
            StepIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))) return;
            var touch = Input.GetTouch(0);
            var pointerEventData = new PointerEventData(EventSystem.current){ position = touch.position};
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if(raycastResults.Count > 0)
            {
                foreach(var result in raycastResults)
                {
                    //creo un azione di button clicked
                    if (result.gameObject.CompareTag("ARButton"))
                    {
                        OnArButtonClicked?.Invoke(result.gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// Mostra il canvas successivo a "CanvasStep[n]"
        /// </summary>
        public void NextStep()
        {
            NavigateStep(StepIndex + 1);
        }
    
        /// <summary>
        /// Mostra il canvas precedente a "CanvasStep[n]"
        /// </summary>
        public void PreviousStep()
        {
            NavigateStep(StepIndex - 1);
        }

        private void NavigateStep(int newStepIndex)
        {
            Canvas[] canvases = FindObjectsOfType<Canvas>(true);
            foreach (var canvas in canvases)
            {
                //trova il canvas all'indice vecchio e lo nasconde
                if (canvas.name == $"CanvasStep{StepIndex}")
                {
                    canvas.gameObject.SetActive(false);
                    continue;
                }
                //trova il canvas all'indice nuovo e lo mostra
                if (canvas.name == $"CanvasStep{newStepIndex}")
                {
                    canvas.gameObject.SetActive(true);
                    continue;
                }
            }
            StepIndex = newStepIndex;
        }
    }
}
