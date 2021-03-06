﻿using System;
using Assets.Scripts.Core.Staff.Helpers;
using Assets.Scripts.UnityExtended;
using Assets.Scripts.UnityExtended.CoreComponents.OWCCursorRenderer;
using Mono.CSharp;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.CoreComponentEditors
{
    [CustomEditor(typeof(OWCCursor))]
    class OWCCursorEditor : UnityEditor.Editor
    {
        #region Fields

        private const float EditorTextureWidth = 200;
        private SerializedProperty _texture;
        private SerializedProperty _offset;
        private Texture2D _offsetPointer;

        #endregion

        private OWCCursor Target { get { return target as OWCCursor; } }

        #region Properties

        private float TextureWidth { get { return Target.Texture.width; } }

        private float TextureHeight { get { return Target.Texture.height; } }

        private float EditorTextureHeight { get { return EditorTextureWidth*TextureHeight/TextureWidth; } }

        private Vector2 EditorTextureSize { get { return new Vector2(EditorTextureWidth, EditorTextureHeight);} }

        private Vector2 EditorTexturePosition
        {
            get
            {
                var rect = EditorGUILayout.GetControlRect();
                rect.x = rect.width / 2 - EditorTextureWidth/2;
                return rect.position;
            }
        }

        private Rect TextureRect { get { return new Rect(EditorTexturePosition, EditorTextureSize);} }

        private Vector2 OffsetPointerTextureSize { get { return new Vector2(EditorTextureWidth / TextureWidth, EditorTextureHeight / TextureHeight); } }
        
        private Rect OffsetPointerRect { get { return new Rect(EditorTexturePosition + Vector2.Scale(OffsetPointerTextureSize, Offset), OffsetPointerTextureSize);} }

        private Vector2 Offset
        {
            get { return _offset.vector2Value; }
            set
            {
                _offset.vector2Value = VectorHelper.Clamp(value, Vector2.zero, Target.TextureSize - Vector2.one);
                _offset.vector2Value = VectorHelper.Ceil(_offset.vector2Value);
            }
        }

        #endregion

        #region Unity events

        protected virtual void OnEnable()
        {
            _offsetPointer = EditorGUIUtility.Load("Textures/Pointer.png") as Texture2D;
        }

        #endregion

        #region Override methods

        public override void OnInspectorGUI()
        {
            _texture = serializedObject.FindProperty("_texture");
            _offset = serializedObject.FindProperty("_offset");
            DrawCoreInspector();
            DrawOffsetEditor();
            DrawAdditionalInspector();
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Private methods

        private void DrawCoreInspector()
        {
            EditorGUILayout.PropertyField(_texture);
            Offset = EditorGUILayout.Vector2Field("Offset", Offset);
        }

        private void DrawOffsetEditor()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("x", GUILayout.Width(10));
            if (GUILayout.Button("+", EditorStyles.miniButtonLeft)) AddToOffset(new Vector2(1, 0));
            if (GUILayout.Button("-", EditorStyles.miniButtonRight)) AddToOffset(new Vector2(-1, 0));
            EditorGUILayout.LabelField("y", GUILayout.Width(10));
            if (GUILayout.Button("+", EditorStyles.miniButtonLeft)) AddToOffset(new Vector2(0, 1));
            if (GUILayout.Button("-", EditorStyles.miniButtonRight)) AddToOffset(new Vector2(0, -1));
            EditorGUILayout.EndHorizontal();
        }

        private void AddToOffset(Vector2 delta)
        {
            Offset += delta;
        }

        private void DrawAdditionalInspector()
        {
            if (Target.Texture == null) return;
            EditorGUILayout.Space();
            var rect = TextureRect;
            var offsetPointer = OffsetPointerRect;
            rect.y += 16;
            EditorGUILayout.LabelField("Preview:", GUILayout.Height(16 + EditorTextureHeight));
            GUI.Box(rect, string.Empty);
            GUI.DrawTexture(rect, Target.Texture);
            GUI.DrawTexture(offsetPointer, _offsetPointer);
        }

        #endregion
    }
}
