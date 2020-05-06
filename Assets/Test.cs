using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MakeTheThing();
    }

    void MakeTheThing()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/charactersave.save", FileMode.Open);
        CharacterSave save = new CharacterSave();
        DataContractSerializer bf = new DataContractSerializer(save.GetType());
        save = (CharacterSave)bf.ReadObject(file);

        Instantiate(save.character, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("GOAL DISTANCE IS " + save.distance + " AND GO NAME IS " + save.character.name);
    }
}
