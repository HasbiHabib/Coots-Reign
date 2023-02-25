using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
{
   // option
   public static void saveoption(savedataplayer player)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/saveFile";
      FileStream stream = new FileStream(path, FileMode.Create);

      saveproggresoption data = new saveproggresoption(player);


      formatter.Serialize(stream, data);
      stream.Close();
   }
   public static saveproggresoption Loadoption()
   {
     string path = Application.persistentDataPath + "/saveFile";
     if(File.Exists(path))
     {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

       saveproggresoption data = formatter.Deserialize(stream) as saveproggresoption ;
       stream.Close();

       return data;
     }
     else
     {
      Debug.LogError("SaveFileNotFound" + path);
      return null;
     }
   }

}
