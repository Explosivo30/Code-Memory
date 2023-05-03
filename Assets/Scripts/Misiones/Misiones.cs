using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mision", menuName ="Characters/Misions", order =0)]
public class Misiones : ScriptableObject
{
    [SerializeField]
    string[] tasks;
}
