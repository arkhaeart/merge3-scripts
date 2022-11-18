using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attribute used to generate dropdown menu of strings from 
/// scriptable object, which implements IDropDownFiller
/// and is stored in path folder in Assets/Resources
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class StringDropDownAttribute : PropertyAttribute 
{
    public string path;
}
public interface IDropDownFiller
{
    string[] GetEntries();
}
