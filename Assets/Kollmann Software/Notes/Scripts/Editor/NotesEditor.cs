using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KollmannSoftware.Shared;
using UnityEditor;
using UnityEngine;

namespace KollmannSoftware.Notes {
    [InitializeOnLoad]
    public class NotesEditor : EditorWindow {

        /// <summary>
        /// Library with save data
        /// </summary>
        private Library library;

        /// <summary>
        /// Library Name
        /// </summary>
        private const string LIBRARY_NAME = "NotesLibrary";

        /// <summary>
        /// Property name to get the unity users name
        /// </summary>
        private const string PROPERTY_NAME_UNITY_NAME = "displayName";

        /// <summary>
        /// Property name to get the unity users info
        /// </summary>
        private const string PROPERTY_NAME_UNITY_USERINFO = "userInfo";

        /// <summary>
        /// Property name to see if the unity user is logged in
        /// </summary>
        private const string PROPERTY_NAME_UNITY_LOGGEDIN = "loggedIn";

        /// <summary>
        /// Unity connect class name
        /// </summary>
        private const string CLASS_NAME_UNITY_CONNECT = "UnityEditor.Connect.UnityConnect";

        /// <summary>
        /// Name of the cancel button
        /// </summary>
        private const string LABEL_CANCEL_BUTTON = "Cancel";

        /// <summary>
        /// Name of the save button
        /// </summary>
        private const string LABEL_SAVE_BUTTON = "Save";

        /// <summary>
        /// Label for the save scene camera toggle
        /// </summary>
        private const string LABEL_SAVE_SCENE_CAMERA_TOGGLE = "Save Scene Camera";

        /// <summary>
        /// Label for the Save selected object toggle
        /// </summary>
        private const string LABEL_SAVE_SELECTED_OBJECT_TOGGLE = "Save Selected Object";

        /// <summary>
        /// Category field label
        /// </summary>
        private const string LABEL_CATEGORY = "Category:";

        /// <summary>
        /// Creator field label
        /// </summary>
        private const string LABEL_CREATOR = "Creator:";

        /// <summary>
        /// Label for the Add New Note Button
        /// </summary>
        private const string LABEL_ADD_NEW_NOTE_BUTTON = "+ Add New Note";

        /// <summary>
        /// Label shown when no categories were created yet.
        /// </summary>
        private const string NO_CATEGORIES_TEXT = "Create some categories first";

        /// <summary>
        /// Label for the Reply Button
        /// </summary>
        private const string LABEL_REPLY_BUTTON = "Reply";

        /// <summary>
        /// Label for the Creator Field
        /// </summary>
        private const string LABEL_CREATOR_FIELD = "Name";

        /// <summary>
        /// Label for the delete dialogue
        /// </summary>
        private const string LABEL_DELETE_DIALOGUE = "Delete";

        /// <summary>
        /// Text for the delete dialogue
        /// </summary>
        private const string DELETE_DIALOGUE_TEXT = "Are you sure you want to delete the note?";

        /// <summary>
        /// Label for the delete button
        /// </summary>
        private const string LABEL_DELETE_BUTTON = "Delete";

        /// <summary>
        /// Label for Name
        /// </summary>
        private const string LABEL_NAME = "Name";

        /// <summary>
        /// Label for Color
        /// </summary>
        private const string LABEL_COLOR = "Color";
        
        /// <summary>
        /// Label for the "Add new Category" button
        /// </summary>
        private const string LABEL_ADD_NEW_CATEGORY = "+ Add New Category";

        /// <summary>
        /// Label for the "All" Item in the generic menu
        /// </summary>
        private const string LABEL_GENERIC_MENU_ALL = "All";

        /// <summary>
        /// "Resolved" Text
        /// </summary>
        private const string TEXT_RESOLVED = "Resolved";

        /// <summary>
        /// "Open" Text
        /// </summary>
        private const string TEXT_OPEN = "Open";

        /// <summary>
        /// "All Notes" Text
        /// </summary>
        private const string TEXT_ALL_NOTES = "All Notes";

        /// <summary>
        /// Label for the filter dropdown
        /// </summary>
        private const string LABEL_SHOW = "Show:";

        /// <summary>
        /// Label for the Categories Toggle
        /// </summary>
        private const string LABEL_CATEGORIES = "Categories";

        /// <summary>
        /// Label for the Notes Toggle
        /// </summary>
        private const string LABEL_NOTES = "Notes";

        /// <summary>
        /// Delete Icon Name
        /// </summary>
        private const string ICON_DELETE_NAME = "remove";

        /// <summary>
        /// Reopen Icon Name
        /// </summary>
        private const string ICON_NAME_REOPEN = "reopen";

        /// <summary>
        /// Check Icon Name
        /// </summary>
        private const string ICON_NAME_CHECK = "check";

        /// <summary>
        /// Eye Icon Name
        /// </summary>
        private const string ICON_NAME_EYE = "eye";

        /// <summary>
        /// Label for the "Selected" Item in the generic menu
        /// </summary>
        private const string LABEL_GENERIC_MENU_SELECTED = "Only When Selected";

        /// <summary>
        /// Label for the "Always" Item in the generic menu
        /// </summary>
        private const string LABEL_GENERIC_MENU_ALWAYS = "Always";

        /// <summary>
        /// selected Menu type
        /// </summary>
        private Menu selectedMenu = Menu.Notes;

        /// <summary>
        /// Menu Types
        /// </summary>
        private enum Menu {
            Notes,
            NotesSelectedObject,
            Categories
        }

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle MultilineLabel;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle Background = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle NoteBackground = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle BackgroundColor = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle Seperator = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle PreSeperator = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle NoteHeader = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle NoteSubHeader = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ShowInSceneButton = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ShowInSceneButtonComment = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle AnswerArea = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ResolveButton = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ResolveButtonSolo = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle CategorieLabel = null;

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle DeleteButton = null;

        /// <summary>
        /// Position of the scrollbar
        /// </summary>
        private Vector2 scrollbarPosition;

        /// <summary>
        /// State-Filters for notes (All, Open, Resolved)
        /// </summary>
        private enum ShowSelection {
            All,
            Open,
            Resolved
        }
        
        /// <summary>
        /// State-Filters for Visibility (All, Selected)
        /// </summary>
        private enum VisibilitySelection {
            All,
            Selected
        }

        /// <summary>
        /// is the user currently adding a new note?
        /// </summary>
        private bool isAddingNewNote;

        /// <summary>
        /// is the user currently adding a new category?
        /// </summary>
        private bool isAddingNewCategory;

        /// <summary>
        /// used for creating a new note
        /// </summary>
        private Note newNote;

        /// <summary>
        /// used for creating a new category
        /// </summary>
        private Category newCategory;

        /// <summary>
        /// used for creating a new comment
        /// </summary>
        private Comment newComment;

        /// <summary>
        /// if a note is currently being commented on it is set here
        /// </summary>
        private Note currentlyCommentedOnNote;

        /// <summary>
        /// has loading the Name from the Unity Connect class already been tried?
        /// </summary>
        private bool triedLoadingName;

        /// <summary>
        /// selected objects GUID
        /// </summary>
        private string selectedGUID;

        /// <summary>
        /// List of all selected objects parents guids
        /// </summary>
        private List<string> selectedParentGUIDs = new List<string>();

        /// <summary>
        /// Initialization
        /// </summary>
        [MenuItem("Window/Kollmann Software/Editor Tools/Notes")]
        static void Init() {
            NotesEditor window = (NotesEditor)EditorWindow.GetWindow(typeof(NotesEditor), false, "Notes");
            window.minSize = new Vector2(295, 200);
            window.Show();
        }

        /// <summary>
        /// Adds SelectionChanged to the Selection.selectionChanged event call
        /// </summary>
        void OnEnable() {
            Selection.selectionChanged += SelectionChanged;
            SelectionChanged();
        }

        /// <summary>
        /// Removes SelectionChanged from the Selection.selectionChanged event call when not needed anymore
        /// </summary>
        void OnDisable() {
            Selection.selectionChanged -= SelectionChanged;
        }

        /// <summary>
        /// Checks if the selection has changed and updates the selectedGUID, selectedParentGUIDs values accordingly
        /// </summary>
        private void SelectionChanged() {
            if (!Selection.activeObject) {
                selectedGUID = "";
                return;
            }
            if (Selection.activeObject != null && AssetDatabase.Contains(Selection.activeObject)) {
                long itemID;
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out selectedGUID, out itemID);
            }
            else if (Selection.activeGameObject != null) {
                selectedGUID = GetGUID(Selection.activeGameObject.transform);
                if (selectedGUID == "") {
                    selectedGUID = CreateGUID(Selection.activeGameObject.transform);
                }
                selectedParentGUIDs = new List<string>();
                var currentParent = Selection.activeGameObject.transform.parent;
                while (currentParent != null) {
                    var parentGUID = GetGUID(currentParent);
                    if (parentGUID == "") {
                        parentGUID = CreateGUID(currentParent);
                    }
                    selectedParentGUIDs.Add(parentGUID);
                    currentParent = currentParent.parent;
                }
            }
        }

        /// <summary>
        /// Checks if the given menu is the selected one
        /// </summary>
        /// <param name="menu">Menu to check</param>
        /// <returns>boolean: true if the given menu is the selected one, false if not</returns>
        private bool IsSelectedMenu(Menu menu) {
            return menu == selectedMenu;
        }

        /// <summary>
        /// Draws the menu selection
        /// </summary>
        private void DrawMenuSelection() {
            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Toggle(IsSelectedMenu(Menu.Notes) || IsSelectedMenu(Menu.NotesSelectedObject), LABEL_NOTES, EditorStyles.toolbarButton)) {
                    if (IsSelectedMenu(Menu.Categories)) {
                        selectedMenu = Menu.Notes;
                    }
                }
                if (GUILayout.Toggle(IsSelectedMenu(Menu.Categories), LABEL_CATEGORIES, EditorStyles.toolbarButton)) {
                    if (!IsSelectedMenu(Menu.Categories)) {
                        selectedMenu = Menu.Categories;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the GUI
        /// </summary>
        void OnGUI() {
            if (library == null) { library = (Library)Common.LoadLibrary<Library>(LIBRARY_NAME); }
            TryLoadingName();
            InitializeStyles();
            DrawMenuSelection();
            DrawToolbar();
            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollbarPosition)) {
                scrollbarPosition = scrollView.scrollPosition;
                switch (selectedMenu) {
                    case Menu.Notes:
                        DrawNotes();
                        break;
                    case Menu.NotesSelectedObject:
                        DrawNotes();
                        break;
                    case Menu.Categories:
                        DrawCategories();
                        break;
                    default:
                        break;
                }
            }
            Repaint();
        }

        /// <summary>
        /// Tries to load the Name from the Unity Connect data.
        /// </summary>
        private void TryLoadingName() {
            if (!triedLoadingName) {
                var userName = GetUserName();
                if (userName != "") {
                    newNote = CreateInstance<Note>();
                    newNote.Creator = userName;
                    newComment = CreateInstance<Comment>();
                    newComment.Creator = userName;
                }
                triedLoadingName = true;
            }
        }

        /// <summary>
        /// Updates the "ShowSelection" value in the library
        /// </summary>
        /// <param name="selected"></param>
        void ShowSelectionChange(ShowSelection selected) {
            library.ShowSelection = (int)selected;
        }

        /// <summary>
        /// Updates the "VisbilitySelection" value in the library
        /// </summary>
        /// <param name="selected">currently selected option</param>
        void VisibilitySelectionChange(VisibilitySelection selected) {
            library.VisbilitySelection = (int)selected;
        }

        /// <summary>
        /// Draws the color box for a note (a rectangular single-colored box)
        /// </summary>
        /// <param name="color">Color the box should be</param>
        void DrawNoteColor(Color color) {
            var bgColor = GUI.color;
            GUI.color = color;
            GUILayout.Box("", BackgroundColor);
            GUI.color = bgColor;
        }

        /// <summary>
        /// Draws a seperator line
        /// </summary>
        void DrawSeperator() {
            GUILayout.Space(12);
            var bgColor = GUI.color;
            GUI.color = Color.gray;
            GUILayout.Box(GUIContent.none, Seperator);
            GUI.color = bgColor;
            GUILayout.Space(5);
        }

        /// <summary>
        /// Draws the toolbar
        /// </summary>
        private void DrawToolbar() {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar)) {
                library.searchString = GUILayout.TextField(library.searchString, GUI.skin.FindStyle("ToolbarSeachTextField"), GUILayout.MaxWidth(400), GUILayout.ExpandWidth(true));
                if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton"))) {
                    library.searchString = "";
                    GUI.FocusControl(null);
                }
            }
        }

        /// <summary>
        /// Draws the full Notes page (Header, "Add new Note" section and all the notes)
        /// </summary>
        private void DrawNotes() {
            DrawNoteHeader();
            DrawAddNewNote();
            DrawAllNotes();
        }

        /// <summary>
        /// Draws the Header for the Notes page - the section where the filters are
        /// </summary>
        private void DrawNoteHeader() {
            selectedMenu = Selection.activeObject == null ? Menu.Notes : Menu.NotesSelectedObject;
            GUILayout.Space(5);
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.Label(IsSelectedMenu(Menu.NotesSelectedObject) ? $"Notes for: {Selection.activeObject.name}" : TEXT_ALL_NOTES);
                GUILayout.FlexibleSpace();
                GUILayout.Label(LABEL_SHOW);
                if (EditorGUILayout.DropdownButton(new GUIContent(((ShowSelection)library.ShowSelection).ToString()), FocusType.Keyboard, GUILayout.Width(80))) {
                    GenericMenu toolsMenu = new GenericMenu();
                    toolsMenu.AddItem(new GUIContent(LABEL_GENERIC_MENU_ALL), (ShowSelection)library.ShowSelection == ShowSelection.All, () => ShowSelectionChange(ShowSelection.All));
                    toolsMenu.AddItem(new GUIContent(TEXT_OPEN), (ShowSelection)library.ShowSelection == ShowSelection.Open, () => ShowSelectionChange(ShowSelection.Open));
                    toolsMenu.AddItem(new GUIContent(TEXT_RESOLVED), (ShowSelection)library.ShowSelection == ShowSelection.Resolved, () => ShowSelectionChange(ShowSelection.Resolved));

                    var s = new GUIStyle();
                    var width = s.CalcSize(new GUIContent(TEXT_RESOLVED)).x;
                    toolsMenu.DropDown(new Rect(Screen.width - 90 - width, 6, 0, 16));
                    EditorGUIUtility.ExitGUI();
                }
            }

            GUILayout.Space(5);
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.Label("Visibility:");
                if (EditorGUILayout.DropdownButton(new GUIContent((VisibilitySelection)library.VisbilitySelection == VisibilitySelection.All ? LABEL_GENERIC_MENU_ALWAYS : LABEL_GENERIC_MENU_SELECTED), FocusType.Keyboard, GUILayout.MinWidth(80))) {
                    GenericMenu toolsMenu = new GenericMenu();

                    toolsMenu.AddItem(new GUIContent(LABEL_GENERIC_MENU_ALWAYS), (VisibilitySelection)library.VisbilitySelection == VisibilitySelection.All, () => VisibilitySelectionChange(VisibilitySelection.All));
                    toolsMenu.AddItem(new GUIContent(LABEL_GENERIC_MENU_SELECTED), (VisibilitySelection)library.VisbilitySelection == VisibilitySelection.Selected, () => VisibilitySelectionChange(VisibilitySelection.Selected));
                                        
                    var s = new GUIStyle();
                    var width = s.CalcSize(new GUIContent(LABEL_GENERIC_MENU_SELECTED)).x;
                    toolsMenu.DropDown(new Rect(9, 28, 0, 16));
                    EditorGUIUtility.ExitGUI();
                }
                GUILayout.FlexibleSpace();
                GUILayout.Label(LABEL_CATEGORY);
                if (EditorGUILayout.DropdownButton(new GUIContent(library.SelectedCategory == null ? LABEL_GENERIC_MENU_ALL : library.SelectedCategory.Name), FocusType.Keyboard, GUILayout.MinWidth(80))) {
                    GenericMenu toolsMenu = new GenericMenu();
                    toolsMenu.AddItem(new GUIContent(LABEL_GENERIC_MENU_ALL), library.SelectedCategory == null, () => library.SelectedCategory = null);
                    foreach (var categorie in library.Categories) {

                        toolsMenu.AddItem(new GUIContent(categorie.Name), library.SelectedCategory == categorie, () => library.SelectedCategory = categorie);
                    }
                    var s = new GUIStyle();
                    var width = s.CalcSize(new GUIContent(library.Categories.Aggregate((max, cur) => max.Name.Length > cur.Name.Length ? max : cur).Name)).x;
                    toolsMenu.DropDown(new Rect(Screen.width - 87 - width, 28, 0, 16));
                    EditorGUIUtility.ExitGUI();
                }
            }
            GUILayout.Space(10);
        }

        /// <summary>
        /// Draws the full categories page, meaning all the categories and hte "add new category" section
        /// </summary>
        private void DrawCategories() {
            DrawAddNewCategory();
            DrawAllCategories();
        }

        /// <summary>
        /// Draws all categories
        /// </summary>
        private void DrawAllCategories() {
            var categories = library.Categories.Where(q => (library.searchString.ToLower().Length > 0 ? q.Name.ToLower().Contains(library.searchString.ToLower()) : true)).ToList();
            for (int i = 0; i < categories.Count; i++) {
                DrawCategoryEntry(categories[i]);
            }
        }

        /// <summary>
        /// Draws a single category entry
        /// </summary>
        /// <param name="category">Category to be drawn</param>
        private void DrawCategoryEntry(Category category) {
            using (new EditorGUILayout.VerticalScope(NoteBackground)) {
                using (new EditorGUILayout.HorizontalScope()) {
                    using (new EditorGUILayout.VerticalScope()) {
                        GUILayout.Space(4);
                        category.Name = EditorGUILayout.TextField(LABEL_NAME, category.Name);
                        category.color = EditorGUILayout.ColorField(LABEL_COLOR, category.color);
                    }
                    var cColor = GUI.contentColor;
                    GUI.contentColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                    if (GUILayout.Button(Common.GetTexture(ICON_DELETE_NAME), GUILayout.Width(30), GUILayout.Height(EditorGUIUtility.singleLineHeight + 1))) {
                        var categoryNotes = library.Notes.Where(q => q.Category == category).ToList();
                        if (EditorUtility.DisplayDialog(LABEL_DELETE_BUTTON, $"Are you sure you want to delete {category.Name}?\nThis will delete {categoryNotes.Count} notes in that category!", LABEL_DELETE_BUTTON, LABEL_CANCEL_BUTTON)) {
                            for (int i = 0; i < categoryNotes.Count; i++) {
                                library.Notes.Remove(categoryNotes[i]);
                                AssetDatabase.RemoveObjectFromAsset(categoryNotes[i]);
                            }
                            if (library.SelectedCategory == category) {
                                library.SelectedCategory = null;
                            }
                            library.Categories.Remove(category);
                            newNote.Category = null;
                            AssetDatabase.RemoveObjectFromAsset(category);
                        }
                    }
                    GUI.contentColor = cColor;
                }
            }
        }

        /// <summary>
        /// Draws the "Add new category" section
        /// </summary>
        private void DrawAddNewCategory() {
            if (newCategory == null) {
                newCategory = ScriptableObject.CreateInstance<Category>();
            }
            isAddingNewCategory = GUILayout.Toggle(isAddingNewCategory, LABEL_ADD_NEW_CATEGORY, "Button");
            if (isAddingNewCategory) {
                using (new EditorGUILayout.VerticalScope(NoteBackground)) {
                    newCategory.Name = EditorGUILayout.TextField(LABEL_NAME, newCategory.Name);
                    newCategory.color = EditorGUILayout.ColorField(LABEL_COLOR, newCategory.color);
                    using (new EditorGUILayout.HorizontalScope()) {
                        if (GUILayout.Button(LABEL_SAVE_BUTTON, GUILayout.Width(80))) {
                            Category categoryCopy = CreateInstance<Category>();
                            categoryCopy.Name = newCategory.Name.ToString();
                            categoryCopy.color = newCategory.color;
                            library.Categories.Add(categoryCopy);
                            AssetDatabase.AddObjectToAsset(categoryCopy, library);
                            newCategory.Name = "";
                            newCategory.color = Color.white;
                            isAddingNewCategory = false;
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button(LABEL_CANCEL_BUTTON, GUILayout.Width(80))) {
                            newCategory.Name = "";
                            newCategory.color = Color.white;
                            isAddingNewCategory = false;
                            GUI.FocusControl(null);
                        }
                        GUILayout.FlexibleSpace();
                    }
                }
            }
            DrawSeperator();
        }
        
        /// <summary>
        /// Draws a single note entry
        /// </summary>
        /// <param name="note">Note to be drawn</param>
        void DrawNoteEntry(Note note) {
            using (new EditorGUILayout.VerticalScope(Background)) {
                DrawNoteColor(note.Category.color);
                using (new EditorGUILayout.VerticalScope()) {
                    using (new EditorGUILayout.HorizontalScope()) {
                        using (new EditorGUILayout.VerticalScope()) {
                            GUILayout.Label(note.Creator, NoteHeader);
                            GUILayout.Label(System.DateTime.Parse(note.CreateTime).ToShortDateString() + " " + System.DateTime.Parse(note.CreateTime).ToShortTimeString(), NoteSubHeader);
                        }
                        GUILayout.FlexibleSpace();
                        using (new EditorGUILayout.VerticalScope()) {
                            using (new EditorGUILayout.HorizontalScope()) {
                                GUILayout.FlexibleSpace();

                                var cColor = GUI.contentColor;
                                GUI.contentColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                                if ((note.SaveSceneCamera || note.SaveSelectedObject) && GUILayout.Button(Common.GetTexture(ICON_NAME_EYE), ShowInSceneButton)) {
                                    if (note.SaveSceneCamera) {
                                        SceneView.lastActiveSceneView.in2DMode = note.SceneCameraIs2dMode;
                                        SceneView.lastActiveSceneView.LookAt(note.SceneCameraPosition, note.SceneCameraRotation, note.SceneCameraSize, note.SceneCameraOrthographic);
                                    }
                                    if (note.SaveSelectedObject) {
                                        var objPath = AssetDatabase.GUIDToAssetPath(note.selectedObjectGUID);
                                        if (objPath == "") {
                                            Selection.activeGameObject = GetGameObjectByGUID(note.selectedObjectGUID);
                                        }
                                        else {
                                            EditorUtility.FocusProjectWindow();
                                            Selection.activeObject = (AssetDatabase.LoadAssetAtPath(objPath, AssetDatabase.GetMainAssetTypeAtPath(objPath)));
                                        }
                                    }
                                }
                                if (GUILayout.Button(note.IsResolved ? Common.GetTexture(ICON_NAME_REOPEN) : Common.GetTexture(ICON_NAME_CHECK), note.SaveSceneCamera || note.SaveSelectedObject ? ResolveButton : ResolveButtonSolo)) {
                                    note.IsResolved = !note.IsResolved;
                                }
                                if (GUILayout.Button(Common.GetTexture(ICON_DELETE_NAME), DeleteButton)) {
                                    if (EditorUtility.DisplayDialog(LABEL_DELETE_DIALOGUE, DELETE_DIALOGUE_TEXT, LABEL_DELETE_BUTTON, LABEL_CANCEL_BUTTON)) {
                                        library.Notes.Remove(note);
                                        AssetDatabase.RemoveObjectFromAsset(note);
                                    }
                                }
                                GUI.contentColor = cColor;
                            }
                            using (new EditorGUILayout.HorizontalScope()) {
                                GUILayout.FlexibleSpace();
                                GUILayout.Label(note.Category.Name, CategorieLabel, GUILayout.MinWidth(80));
                            }
                        }
                    }
                    GUILayout.Label(note.Description, MultilineLabel);
                }
                if (note.Comments != null && note.Comments.Count > 0) {
                    foreach (var comment in note.Comments) {
                        DrawCommentEntry(comment);
                    }
                }
                else if (note.Comments == null) {
                    note.Comments = new List<Comment>();
                }
                if (!note.IsResolved) {
                    DrawSeperator();
                }
                if (newComment == null) {
                    newComment = ScriptableObject.CreateInstance<Comment>();
                    newComment.Creator = GetUserName();
                }
                if (!note.IsResolved && GUILayout.Toggle(note.IsReplying, LABEL_REPLY_BUTTON, "Button")) {
                    if (currentlyCommentedOnNote != note) {
                        newComment.Description = "";
                        newComment.SaveSceneCamera = false;
                        newComment.SaveSelectedObject = false;
                        library.Notes.ForEach(q => q.IsReplying = false);
                        note.IsReplying = true;
                        currentlyCommentedOnNote = note;
                    }
                }
                else {
                    if (currentlyCommentedOnNote == note) {
                        currentlyCommentedOnNote = null;
                    }
                    note.IsReplying = false;
                }
                if (note.IsReplying) {
                    using (new EditorGUILayout.VerticalScope(AnswerArea)) {
                        using (new EditorGUILayout.HorizontalScope()) {
                            GUILayout.Label(LABEL_CREATOR_FIELD, GUILayout.Width(75));
                            newComment.Creator = GUILayout.TextField(newComment.Creator);
                        }
                        GUILayout.Space(12);
                        newComment.Description = GUILayout.TextArea(newComment.Description, GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 3));
                        if (Selection.activeObject != null) {
                            newComment.SaveSelectedObject = GUILayout.Toggle(newComment.SaveSelectedObject, LABEL_SAVE_SELECTED_OBJECT_TOGGLE);
                        }
                        else {
                            newComment.SaveSelectedObject = false;
                        }
                        newComment.SaveSceneCamera = GUILayout.Toggle(newComment.SaveSceneCamera, LABEL_SAVE_SCENE_CAMERA_TOGGLE);

                        GUILayout.Space(10);
                        using (new EditorGUILayout.HorizontalScope()) {
                            if (GUILayout.Button(LABEL_REPLY_BUTTON, GUILayout.Width(80))) {
                                var nComment = CreateInstance<Comment>();
                                nComment.Creator = newComment.Creator;
                                nComment.Description = newComment.Description;
                                nComment.CreateTime = DateTime.Now.ToString();
                                nComment.SaveSelectedObject = newComment.SaveSelectedObject;
                                nComment.SaveSceneCamera = newComment.SaveSceneCamera;
                                if (nComment.SaveSceneCamera) {
                                    nComment.SceneCameraPosition = SceneView.lastActiveSceneView.pivot;
                                    nComment.SceneCameraRotation = SceneView.lastActiveSceneView.rotation;
                                    nComment.SceneCameraOrthographic = SceneView.lastActiveSceneView.orthographic;
                                    nComment.SceneCameraSize = SceneView.lastActiveSceneView.size;
                                    nComment.SceneCameraIs2dMode = SceneView.lastActiveSceneView.in2DMode;
                                }
                                if (nComment.SaveSelectedObject) {
                                    nComment.selectedObjectGUID = selectedGUID;
                                }
                                note.Comments.Add(nComment);
                                AssetDatabase.AddObjectToAsset(nComment, library);
                                newComment.Description = "";
                                newComment.SaveSceneCamera = false;
                                newComment.SaveSelectedObject = false;
                                note.IsReplying = false;
                                GUI.FocusControl(null);
                            }
                            if (GUILayout.Button(LABEL_CANCEL_BUTTON, GUILayout.Width(80))) {
                                newComment.Description = "";
                                newComment.SaveSceneCamera = false;
                                newComment.SaveSelectedObject = false;
                                note.IsReplying = false;
                                GUI.FocusControl(null);
                            }
                            GUILayout.FlexibleSpace();
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Creates a GUID by a GameObjects transform
        /// </summary>
        /// <param name="t">Transform of the GameObject that a GUID should be created and returned for</param>
        /// <returns>GUID by a GameObjects transform</returns>
        string CreateGUID(Transform t) {
            var guid = System.Guid.NewGuid();
            var guidGO = new GameObject(guid.ToString());
            guidGO.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSaveInBuild | HideFlags.NotEditable;
            guidGO.transform.SetParent(t);
            return guid.ToString();
        }

        /// <summary>
        /// Returns a GUID by a GameObjects transform
        /// </summary>
        /// <param name="t">Transform of the GameObject whose GUID should be returned</param>
        /// <returns>GUID by a GameObjects transform</returns>
        string GetGUID(Transform t) {
            foreach (Transform child in t) {
                if (t.hideFlags == (HideFlags.HideInHierarchy & HideFlags.DontSaveInBuild & HideFlags.NotEditable)) {
                    return child.name;
                }
            }
            return "";
        }

        /// <summary>
        /// Returns a GameObject by a GUID
        /// </summary>
        /// <param name="guid">GUID of the GameObject that should be returned</param>
        /// <returns> GameObject by a GUID</returns>
        GameObject GetGameObjectByGUID(string guid) {
            var go = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Where(q => q.name == guid).FirstOrDefault() as GameObject;
            return go ? go.transform.parent.gameObject : null;
        }

        /// <summary>
        /// Draws a single comment entry
        /// </summary>
        /// <param name="comment">comment to draw</param>
        void DrawCommentEntry(Comment comment) {
            DrawSeperator();
            using (new EditorGUILayout.VerticalScope()) {
                using (new EditorGUILayout.HorizontalScope()) {
                    using (new EditorGUILayout.VerticalScope()) {
                        GUILayout.Label(comment.Creator, NoteHeader);
                        GUILayout.Label(System.DateTime.Parse(comment.CreateTime).ToShortDateString() + " " + System.DateTime.Parse(comment.CreateTime).ToShortTimeString(), NoteSubHeader);
                    }

                    var cColor = GUI.contentColor;
                    GUI.contentColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                    if ((comment.SaveSceneCamera || comment.SaveSelectedObject) && GUILayout.Button(Common.GetTexture("eye"), ShowInSceneButtonComment)) {
                        if (comment.SaveSceneCamera) {
                            SceneView.lastActiveSceneView.in2DMode = comment.SceneCameraIs2dMode;
                            SceneView.lastActiveSceneView.LookAt(comment.SceneCameraPosition, comment.SceneCameraRotation, comment.SceneCameraSize, comment.SceneCameraOrthographic);
                        }
                        if (comment.SaveSelectedObject) {
                            var objPath = AssetDatabase.GUIDToAssetPath(comment.selectedObjectGUID);
                            if (objPath == "") {
                                Selection.activeGameObject = GetGameObjectByGUID(comment.selectedObjectGUID);
                            }
                            else {
                                EditorUtility.FocusProjectWindow();
                                Selection.activeObject = (AssetDatabase.LoadAssetAtPath(objPath, AssetDatabase.GetMainAssetTypeAtPath(objPath)));
                            }
                        }
                    }
                    GUI.contentColor = cColor;
                }
                GUILayout.Space(10);
                GUILayout.Label(comment.Description, MultilineLabel);
            }
        }

        /// <summary>
        /// Draws all Notes
        /// </summary>
        void DrawAllNotes() {
            var notes = library.Notes.Where(q => (library.searchString.Length > 0 ? q.Description.ToLower().Contains(library.searchString.ToLower()) : true)
                && (((ShowSelection)library.ShowSelection) == ShowSelection.Resolved ? q.IsResolved : ((ShowSelection)library.ShowSelection) == ShowSelection.Open ? !q.IsResolved : true)
                && (q.Category == library.SelectedCategory ? true : library.SelectedCategory == null ? true : false)
                && (selectedGUID == "" || (VisibilitySelection)library.VisbilitySelection == VisibilitySelection.All ? true : (selectedParentGUIDs.Any(g => g == q.selectedObjectGUID) || q.selectedObjectGUID == selectedGUID))).ToList();
            for (int i = 0; i < notes.Count; i++) {
                DrawNoteEntry(notes[i]);
            }
        }

        /// <summary>
        /// Draws the "Add New Note" section
        /// </summary>
        private void DrawAddNewNote() {
            if (newNote == null) {
                newNote = ScriptableObject.CreateInstance<Note>();
            }
            if (newNote.Category == null && library.Categories.Count > 0) {
                newNote.Category = library.Categories[0];
            }
            else if (library.Categories == null || library.Categories.Count == 0) {
                EditorGUILayout.HelpBox(NO_CATEGORIES_TEXT, MessageType.Error);
                return;
            }
            isAddingNewNote = GUILayout.Toggle(isAddingNewNote, LABEL_ADD_NEW_NOTE_BUTTON, "Button");
            if (isAddingNewNote) {
                using (new EditorGUILayout.VerticalScope(NoteBackground)) {
                    using (new EditorGUILayout.HorizontalScope()) {
                        GUILayout.Label(LABEL_CREATOR, GUILayout.Width(75));
                        newNote.Creator = GUILayout.TextField(newNote.Creator);
                    }
                    GUILayout.Space(12);
                    using (new EditorGUILayout.HorizontalScope()) {
                        GUILayout.Label(LABEL_CATEGORY);
                        if (EditorGUILayout.DropdownButton(new GUIContent(newNote.Category.Name), FocusType.Keyboard)) {
                            GenericMenu toolsMenu = new GenericMenu();
                            foreach (var category in library.Categories) {
                                toolsMenu.AddItem(new GUIContent(category.Name), newNote.Category == category, () => newNote.Category = category);
                            }
                            toolsMenu.DropDown(new Rect(Screen.width - 208, 123, 0, 16));
                            EditorGUIUtility.ExitGUI();
                        }
                    }
                    GUILayout.Space(5);
                    newNote.Description = GUILayout.TextArea(newNote.Description, GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 3));

                    if (Selection.activeObject != null) {
                        newNote.SaveSelectedObject = GUILayout.Toggle(newNote.SaveSelectedObject, LABEL_SAVE_SELECTED_OBJECT_TOGGLE);
                    }
                    else {
                        newNote.SaveSelectedObject = false;
                    }
                    if (!Selection.activeObject) {
                        newNote.SaveSelectedObject = false;
                    }
                    newNote.SaveSceneCamera = GUILayout.Toggle(newNote.SaveSceneCamera, LABEL_SAVE_SCENE_CAMERA_TOGGLE);
                    GUILayout.Space(10);
                    using (new EditorGUILayout.HorizontalScope()) {
                        if (GUILayout.Button(LABEL_SAVE_BUTTON, GUILayout.Width(80))) {
                            var nNote = CreateInstance<Note>();
                            nNote.Creator = newNote.Creator;
                            nNote.CreateTime = DateTime.Now.ToString();
                            nNote.Category = newNote.Category;
                            nNote.Description = newNote.Description;
                            nNote.SaveSceneCamera = newNote.SaveSceneCamera;
                            nNote.SaveSelectedObject = newNote.SaveSelectedObject;
                            if (nNote.SaveSceneCamera) {
                                nNote.SceneCameraPosition = SceneView.lastActiveSceneView.pivot;
                                nNote.SceneCameraRotation = SceneView.lastActiveSceneView.rotation;
                                nNote.SceneCameraOrthographic = SceneView.lastActiveSceneView.orthographic;
                                nNote.SceneCameraSize = SceneView.lastActiveSceneView.size;
                                nNote.SceneCameraIs2dMode = SceneView.lastActiveSceneView.in2DMode;
                            }
                            if (nNote.SaveSelectedObject) {
                                nNote.selectedObjectGUID = selectedGUID;
                            }

                            library.Notes.Add(nNote);
                            AssetDatabase.AddObjectToAsset(nNote, library);
                            newNote.Description = "";
                            newNote.SaveSceneCamera = true;
                            newNote.SaveSelectedObject = true;
                            isAddingNewNote = false;
                        }
                        if (GUILayout.Button(LABEL_CANCEL_BUTTON, GUILayout.Width(80))) {
                            newNote.Description = "";
                            newNote.SaveSceneCamera = true;
                            newNote.SaveSelectedObject = true;
                            isAddingNewNote = false;
                        }
                        GUILayout.FlexibleSpace();
                    }
                }
            }
            DrawSeperator();
        }

        /// <summary>
        /// Returns the name of the user if logged into unity
        /// </summary>
        /// <returns>username of the user if logged into unity</returns>
        private static string GetUserName() {
            try {
                var assembly = Assembly.GetAssembly(typeof(UnityEditor.EditorWindow));
                object uc = assembly.CreateInstance(CLASS_NAME_UNITY_CONNECT, false, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null, null);
                if ((bool)(uc.GetType().GetProperty(PROPERTY_NAME_UNITY_LOGGEDIN).GetValue(uc, null)) == false) {
                    return "";
                }
                var userInfo = uc.GetType().GetProperty(PROPERTY_NAME_UNITY_USERINFO).GetValue(uc, null);
                return userInfo.GetType().GetProperty(PROPERTY_NAME_UNITY_NAME).GetValue(userInfo, null) as string;
            }
            catch {
                return "";
            }
        }

        /// <summary>
        /// Initializing all the Styles
        /// </summary>
        private void InitializeStyles() {
            if (Background == null) {
                NoteBackground = new GUIStyle("Box");
                NoteBackground.margin = new RectOffset(7, 7, 10, 10);
                NoteBackground.padding = new RectOffset(10, 10, 10, 10);
                NoteBackground.fixedHeight = 0;
                NoteBackground.stretchHeight = false;

                Background = new GUIStyle("Box");
                Background.margin = new RectOffset(7, 7, 10, 10);
                Background.padding = new RectOffset(10, 10, 0, 10);
                Background.fixedHeight = 0;
                Background.stretchHeight = false;

                BackgroundColor = new GUIStyle("WhiteBackground");
                BackgroundColor.margin = new RectOffset(0, 0, 0, 0);
                BackgroundColor.padding = new RectOffset(0, 0, 0, 0);
                BackgroundColor.fixedHeight = 10;
                BackgroundColor.fixedWidth = 65;
                BackgroundColor.stretchHeight = false;
                BackgroundColor.stretchWidth = false;

                CategorieLabel = new GUIStyle(EditorStyles.helpBox);
                CategorieLabel.padding = new RectOffset(5, 5, 2, 2);
                CategorieLabel.fixedHeight = EditorGUIUtility.singleLineHeight + 1;
                CategorieLabel.stretchWidth = true;
                CategorieLabel.alignment = TextAnchor.MiddleCenter;
                CategorieLabel.fontStyle = FontStyle.Bold;
                CategorieLabel.wordWrap = false;

                MultilineLabel = new GUIStyle(EditorStyles.label);
                MultilineLabel.margin = new RectOffset(2, 2, 0, 2);
                MultilineLabel.padding = new RectOffset(2, 2, 2, 2);
                MultilineLabel.wordWrap = true;

                NoteHeader = new GUIStyle(EditorStyles.boldLabel);
                NoteHeader.margin = new RectOffset(2, 2, 2, 0);
                NoteHeader.padding = new RectOffset(2, 2, 2, 0);
                NoteHeader.fontSize = 11;
                NoteHeader.wordWrap = true;

                NoteSubHeader = new GUIStyle(EditorStyles.boldLabel);
                NoteSubHeader.margin = new RectOffset(2, 2, 3, 0);
                NoteSubHeader.padding = new RectOffset(2, 2, 2, 0);
                NoteSubHeader.fontSize = 10;
                NoteSubHeader.wordWrap = false;

                Seperator = new GUIStyle();
                Seperator.normal.background = EditorGUIUtility.whiteTexture;
                Seperator.margin = new RectOffset(0, 0, 4, 4);
                Seperator.fixedHeight = 1;

                PreSeperator = new GUIStyle();
                PreSeperator.normal.background = EditorGUIUtility.whiteTexture;
                PreSeperator.margin = new RectOffset(0, 0, 0, 0);
                PreSeperator.fixedHeight = 1;

                ShowInSceneButton = new GUIStyle(EditorStyles.miniButtonLeft);
                ShowInSceneButton.fixedHeight = EditorGUIUtility.singleLineHeight - 1;
                ShowInSceneButton.fixedWidth = 40;
                ShowInSceneButton.stretchHeight = false;
                ShowInSceneButton.stretchWidth = false;
                ShowInSceneButton.padding = new RectOffset(0, 0, 0, 0);

                ShowInSceneButtonComment = new GUIStyle(EditorStyles.miniButton);
                ShowInSceneButtonComment.fixedHeight = EditorGUIUtility.singleLineHeight - 1;
                ShowInSceneButtonComment.fixedWidth = 40;
                ShowInSceneButtonComment.stretchHeight = false;
                ShowInSceneButtonComment.stretchWidth = false;
                ShowInSceneButtonComment.padding = new RectOffset(0, 0, 0, 0);

                ResolveButton = new GUIStyle(EditorStyles.miniButtonMid);
                ResolveButton.fixedHeight = EditorGUIUtility.singleLineHeight - 1;
                ResolveButton.fixedWidth = 40;
                ResolveButton.stretchHeight = false;
                ResolveButton.stretchWidth = false;
                ResolveButton.padding = new RectOffset(0, 0, 0, 0);

                ResolveButtonSolo = new GUIStyle(EditorStyles.miniButtonLeft);
                ResolveButtonSolo.fixedHeight = EditorGUIUtility.singleLineHeight - 1;
                ResolveButtonSolo.fixedWidth = 40;
                ResolveButtonSolo.stretchHeight = false;
                ResolveButtonSolo.stretchWidth = false;
                ResolveButtonSolo.padding = new RectOffset(0, 0, 0, 0);

                DeleteButton = new GUIStyle(EditorStyles.miniButtonRight);
                DeleteButton.fixedHeight = EditorGUIUtility.singleLineHeight - 1;
                DeleteButton.fixedWidth = 40;
                DeleteButton.stretchHeight = false;
                DeleteButton.stretchWidth = false;
                DeleteButton.padding = new RectOffset(0, 0, 0, 0);

                AnswerArea = new GUIStyle("Box");
                AnswerArea.stretchHeight = false;
                AnswerArea.margin = new RectOffset(5, 5, 5, 5);
                AnswerArea.padding = new RectOffset(9, 9, 9, 9);
            }
        }
    }
}