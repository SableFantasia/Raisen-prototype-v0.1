using System.Collections.Generic;
using UnityEngine;

namespace KollmannSoftware.Notes {
    [System.Serializable]
    public class Note : ScriptableObject {

        /// <summary>
        /// who created this note
        /// </summary>
        public string Creator;

        /// <summary>
        /// description/actual text of the note
        /// </summary>
        public string Description;

        /// <summary>
        /// time & date the comment was created at
        /// </summary>
        public string CreateTime;

        /// <summary>
        /// should the selected object be saved?
        /// </summary>
        public bool SaveSelectedObject;

        /// <summary>
        /// should the scene camera be saved?
        /// </summary>
        public bool SaveSceneCamera;

        /// <summary>
        /// is the note resolved?
        /// </summary>
        public bool IsResolved;

        /// <summary>
        /// is the user currently replying to the note?
        /// </summary>
        public bool IsReplying;

        /// <summary>
        /// ????
        /// </summary>
        public bool HasReplyingChanged;

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
        /// categorie of this note
        /// </summary>
        [SerializeField]
        public Category Category;

        /// <summary>
        /// List of all comments of the note
        /// </summary>
        public List<Comment> Comments = new List<Comment>();

        /// <summary>
        /// GUID of the GameObject this note is about
        /// </summary>
        public string selectedObjectGUID;
    }
}