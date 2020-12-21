using UnityEngine;

namespace KollmannSoftware.Notes {
    [System.Serializable]
    public class Category : ScriptableObject {

        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name;

        /// <summary>
        /// Color of the category
        /// </summary>
        public Color color = Color.white;
    }
}