﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Make a drawer for the editor so an array can have labeled indices
[CustomPropertyDrawer (typeof(NamedArrayAttribute)) ] public class NamedArrayDrawer : PropertyDrawer {
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.ObjectField(rect, property, new GUIContent(((NamedArrayAttribute)attribute).names[pos]));
        }
        catch
        {
            EditorGUI.ObjectField(rect, property, label);
        }
    }
}
