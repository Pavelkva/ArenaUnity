using Abilities;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public static class Deserializer
{
   public static List<Ability> GetAllAbilities(string path)
   {
        XmlSerializer serializer = new XmlSerializer(typeof(Ability));
        
        List<Ability> abilities = new List<Ability>();
        string[] files = Directory.GetFiles(path, "*.xml");
        foreach (string file in files)
        {
            StreamReader reader = new StreamReader(file);
            Ability ability = (Ability)serializer.Deserialize(reader.BaseStream);
            abilities.Add(ability);
        }
        return abilities;
   }
}
