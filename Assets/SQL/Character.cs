using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank
{
    public class CharacterData
    {
        public string _id;
        public string _type;
        public GameObject _character;
        public string _dateCreated;

        public CharacterData(string id, string type, GameObject character)
        {
            _id = id;
            _type = type;
            _character = character;
            _dateCreated = "";
        }

        public CharacterData(string id, string type, GameObject character, string dateCreated)
        {
            _id = id;
            _type = type;
            _character = character;
            _dateCreated = dateCreated;
        }
    }
}

