using System.Data;
using UnityEngine;

namespace DataBank
{
    public class CharacterDB : SQLite
    {
        private const string Tag = "CharacterDB:\t";

        private const string TABLE_NAME = "Characters";
        private const string KEY_ID = "id";
        private const string KEY_TYPE = "type";
        private const string KEY_CHARACTER = "character";
        private const string KEY_DATE = "date";
        private string[] COLUMNS = new string[] { KEY_ID, KEY_TYPE, KEY_CHARACTER, KEY_DATE };

        public CharacterDB() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " TEXT PRIMARY KEY, " +
                KEY_TYPE + " TEXT, " +
                KEY_CHARACTER + " TEXT, " +
                KEY_DATE + " DATETIME DEFAULT CURRENT_TIMESTAMP )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(CharacterData data)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_TYPE + ", "
                + KEY_CHARACTER + " ) "

                + "VALUES ( '"
                + data._id + "', '"
                + data._type + "', '"
                + data._character + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(Tag + "Getting Character: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id)
        {
            Debug.Log(Tag + "Deleting Character: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteDataById(int id)
        {
            base.deleteDataById(id);
        }

        public override void deleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }
    }
}



