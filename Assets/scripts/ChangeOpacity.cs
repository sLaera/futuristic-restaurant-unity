using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChangeOpacity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var material = GetComponent<Renderer>().material;
        material.DOFade(0.01f,1).SetLoops(-1,LoopType.Yoyo);
    }

}
