using UnityEngine;

namespace KollmannSoftware.Notes {
    [System.Serializable]
    public class Comment : ScriptableObject {

        /// <summary>
        /// who created this comment
        /// </summary>
        public string Creator;

        /// <summary>
        /// time & date the comment was created at
        /// </summary>
        public string CreateTime;

        /// <summary>
        /// description/actual text of the comment
        /// </summary>
        public string Description;

        /// <summary>
        /// should the selected object be saved?
        /// </summary>
        public bool SaveSelectedObject;

        /// <summary>
        /// should the scene camera be saved?
        /// </summary>
        public bool SaveSceneCamera;

        /// <summary>
        /// Position of the scene camera
        /// </summary>
        public Vector3 SceneCameraPosition;

        /// <summary>
        /// Rotation of the scene camera
        /// </summary>
        public Quaternion SceneCameraRotation;

        /// <summary>
        /// Is the scene Camera orthographic?
        /// </summary>
        public bool SceneCameraOrthographic;

        /// <summary>
        /// Size/Zoom of the SceneCamera
        /// </summary>
        public float SceneCameraSize;

        /// <summary>
        /// is the scene camera in 2D-Mode?
        /// </summary>
        public bool SceneCameraIs2dMode;

        /// <summary>
        /// GUID of the selected object
        /// </summary>
        public string selectedObjectGUID;
    }
}