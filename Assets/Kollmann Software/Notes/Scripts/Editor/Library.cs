using System.Collections.Generic;
using UnityEngine;

namespace KollmannSoftware.Notes {
    [System.Serializable]
    public class Library : ScriptableObject {

        /// <summary>
        /// List of all notes
        /// </summary>
        public List<Note> Notes = new List<Note>();

        /// <summary>
        /// List of all categories
        /// </summary>
        public List<Category> Categories = new List<Category>();

        /// <summary>
        /// Which types of Notes should be shown (ShowSelection.All, ShowSelection.Open, ShowSelection.Resolved)
        /// </summary>
        public int ShowSelection;

        /// <summary>
        /// Which types of Notes should be visible (VisibilitySelection.All, VisibilitySelection.Selected)
        /// </summary>
        public int VisbilitySelection;

        /// <summary>
        /// Selected Category for filter
        /// </summary>
        public Category SelectedCategory;

        /// <summary>
        /// Search String
        /// </summary>
        public string searchString = "";
    }
}