using System.Runtime.Serialization;
using UnityEngine;

[DataContract]
public class CharacterSave 
{
    [DataMember]
    public GameObject character;
    [DataMember]
    public string distance;
}
