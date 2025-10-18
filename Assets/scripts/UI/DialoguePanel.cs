using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialoguePanel : MonoBehaviour
    {
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Text;
        public Button NextButton;
    
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
