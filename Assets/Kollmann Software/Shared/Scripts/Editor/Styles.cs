using UnityEditor;
using UnityEngine;
namespace KollmannSoftware.Shared {
    public class Styles {

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ButtonLeft = new GUIStyle(EditorStyles.miniButtonLeft) {
            fixedHeight = 20
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ButtonLeftToggled = new GUIStyle(EditorStyles.miniButtonLeft) {
            normal = EditorStyles.miniButtonLeft.active,
            focused = EditorStyles.miniButtonLeft.active,
            hover = EditorStyles.miniButtonLeft.active,
            fontStyle = FontStyle.Bold
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ButtonMid = new GUIStyle(EditorStyles.miniButtonMid) {
            fixedHeight = 20
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ButtonMidToggled = new GUIStyle(EditorStyles.miniButtonMid) {
            normal = EditorStyles.miniButtonMid.active,
            focused = EditorStyles.miniButtonMid.active,
            hover = EditorStyles.miniButtonMid.active,
            fontStyle = FontStyle.Bold
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ButtonRight = new GUIStyle(EditorStyles.miniButtonRight) {
            fixedHeight = 20
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle ButtonRightToggled = new GUIStyle(EditorStyles.miniButtonRight) {
            normal = EditorStyles.miniButtonRight.active,
            focused = EditorStyles.miniButtonRight.active,
            hover = EditorStyles.miniButtonRight.active,
            fontStyle = FontStyle.Bold
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle RoundBox = new GUIStyle(EditorStyles.helpBox) {
            padding = new RectOffset(5, 5, 5, 5)
        };

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle Box = new GUIStyle("BOX") {
            padding = new RectOffset(5, 5, 5, 5)
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="margin"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        private static GUIStyle Empty(RectOffset margin, RectOffset padding) {
            return new GUIStyle() { margin = margin, padding = padding };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="margin"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        private static GUIStyle Empty(int margin, int padding) {
            return new GUIStyle() { margin = new RectOffset(margin, margin, margin, margin), padding = new RectOffset(padding, padding, padding, padding) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="marginAndPadding"></param>
        /// <returns></returns>
        private static GUIStyle Empty(int marginAndPadding) {
            return new GUIStyle() { margin = new RectOffset(marginAndPadding, marginAndPadding, marginAndPadding, marginAndPadding), padding = new RectOffset(marginAndPadding, marginAndPadding, marginAndPadding, marginAndPadding) };
        }

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle Text = new GUIStyle(EditorStyles.label) {
            wordWrap = true
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static GUIStyle White(int width, int height) {
            return new GUIStyle("WhiteBackground") {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                fixedWidth = width,
                fixedHeight = height,
                stretchHeight = false,
                stretchWidth = false
            };
        }

        /// <summary>
        /// 
        /// </summary>
        private static GUIStyle Button = new GUIStyle(EditorStyles.miniButton) {
            fixedHeight = 20
        };
    }
}