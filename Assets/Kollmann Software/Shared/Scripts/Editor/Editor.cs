using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace KollmannSoftware.Shared {

    public abstract class Editor : UnityEditor.Editor {

        /// <summary>
        /// Just to not have to call serializedObject.Update();, serializedObject.ApplyModifiedProperties(); and Repaint(); everytime
        /// </summary>
        public override void OnInspectorGUI() {
            serializedObject.Update();            
            DoGUI();
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        /// <summary>
        /// Replacement for OnInspectorGUI
        /// </summary>
        public abstract void DoGUI();
    }
}