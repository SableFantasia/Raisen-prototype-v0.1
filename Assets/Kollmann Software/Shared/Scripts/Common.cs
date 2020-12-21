using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace KollmannSoftware.Shared {
    public class Common {

        /// <summary>
        /// Path to commonly used sprites
        /// </summary>
        private const string SPRITE_PATH = "Assets/Kollmann Software/Shared/Sprites/";

        /// <summary>
        /// Save Path for the assets libraries
        /// </summary>
        public const string LIBRARY_FOLDER = "Assets/Kollmann Software/Shared/Resources";

        /// <summary>
        /// Returns the path of the library with the name
        /// </summary>
        /// <param name="name">Library Name</param>
        /// <returns>the path of the library with the name</returns>
        public static string GetLibraryPath(string name) {
            return LIBRARY_FOLDER + "/" + name + ".asset";
        }
#if UNITY_EDITOR
        /// <summary>
        /// Returns the Texture with the name
        /// </summary>
        /// <param name="name">Texture/Sprite Name</param>
        /// <returns>the Texture with the name</returns>
        public static Texture2D GetTexture(string name) {
            return (Texture2D)AssetDatabase.LoadAssetAtPath(SPRITE_PATH + name + ".png", typeof(Texture2D));
        }

        /// <summary>
        /// Creates a button that is only a icon to click on
        /// </summary>
        /// <param name="textureName">Texture/Sprite Name</param>
        /// <param name="height">height of the button</param>
        /// <param name="width">width of the button</param>
        /// <returns>bool: Was the button pressed?</returns>
        public static bool IconButton(string textureName, float height, float width) {
            return GUILayout.Button(GetTexture(textureName), EditorStyles.label, GUILayout.Height(height), GUILayout.Width(width)) ? true : false;
        }

        /// <summary>
        /// Creates a normal button with a image on it
        /// </summary>
        /// <param name="textureName">Texture/Sprite Name</param>
        /// <param name="height">height of the button</param>
        /// <param name="width">width of the button</param>
        /// <param name="textureColor">Color of the texture/sprite</param>
        /// <returns>bool: was the button pressed?</returns>
        public static bool ImageButton(string textureName, float height, float width, Color textureColor) {
            using (new EditorGUILayout.VerticalScope(new GUIStyle("Button"))) {
                var bgColor = GUI.color;
                GUI.color = textureColor;
                if (IconButton(textureName, height, width)) {
                    GUI.color = bgColor;
                    return true;
                }
                GUI.color = bgColor;
            }
            return false;
        }

        /// <summary>
        /// Creates a folder if it doesn't exist
        /// </summary>
        /// <param name="path">Path of the folder to be created</param>
        public static void CreateFolderIfNotExists(string path) {
            string[] splitFolderPaths = path.Split('/');
            string combinedPath = "";
            for (int i = 0; i < splitFolderPaths.Length; i++) {
                if (!AssetDatabase.IsValidFolder((combinedPath.Length > 0 ? combinedPath + "/" : "") + splitFolderPaths[i])) {
                    AssetDatabase.CreateFolder(combinedPath, splitFolderPaths[i]);
                }
                combinedPath += (combinedPath.Length > 0 ? "/" : "") + splitFolderPaths[i];
            }
        }
#endif

        /// <summary>
        /// Loads a library
        /// </summary>
        /// <typeparam name="T">Type of the Library</typeparam>
        /// <param name="libraryName">Name of the Library</param>
        /// <returns>library of given type and name</returns>
        public static Object LoadLibrary<T>(string libraryName) {
            var library = Resources.Load(libraryName, typeof(T));
            if (!library) {
#if UNITY_EDITOR
                library = ScriptableObject.CreateInstance(typeof(T));
                Common.CreateFolderIfNotExists(Common.LIBRARY_FOLDER);
                AssetDatabase.CreateAsset(library, Common.GetLibraryPath(libraryName));
                EditorUtility.SetDirty(library);
#endif
            }
            if (library == null) {
                Debug.LogError($"No Library of with name {libraryName} and type {typeof(T)} found! Please first open the corresponding EditorWindow or add the needed MonoBehaviours.");
            }
            return library;
        }

        public static Color GetVisibleColor(Color background) {
            return (0.299 * background.r + 0.587 * background.g + 0.114 * background.b) < 0.5f ? Color.white : new Color(0.2f, 0.2f, 0.2f);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Style for a visible label
        /// </summary>
        /// <param name="c">background color</param>
        /// <returns></returns>
        public static GUIStyle VisibleLabel(Color c) {
            var s = new GUIStyle(EditorStyles.label);
            s.normal.textColor = Common.GetVisibleColor(c);
            return s;
        }
#endif
    }
}