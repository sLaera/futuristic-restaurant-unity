using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class riepilogo_ordini : MonoBehaviour
{
    public int ordini_tot = 0;
    public int ordini_corretti = 0;
    public int ordini_errati = 0;
    public TextMeshPro testo;
    void Start()
    {
        ordini_tot = 0;
        ordini_tot = 0;
        ordini_errati = 0;
        Update_testo();
        GameManager.Instance.n_ordini_corretti = 0;
    }

    // Update is called once per frame
    public void Update_testo()
    {
        testo.text = "Riepologo ordini\n<color=red>Errati: " + ordini_errati + "</color>\n<color=green>Corretti: " + ordini_corretti + "</color>\nTotali: " +
                     ordini_tot;
        GameManager.Instance.n_ordini_corretti = ordini_corretti;
    }
}
