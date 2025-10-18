using UnityEngine;

namespace UI.Executer
{
    public class NextButtonExecuter : MonoBehaviour, IARButtonExecuter
    {

        public void DoAction(GameObject obj)
        {
            ARButtonManager.Instance.NextStep();
        }
    }
}
