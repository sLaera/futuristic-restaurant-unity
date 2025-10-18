using UnityEngine;

namespace Utils
{
    public class DataAssetLoader
    {
        public static T Load<T>(TextAsset asset)
        {
            return JsonUtility.FromJson<T>(asset.text);
        }
    }
}
