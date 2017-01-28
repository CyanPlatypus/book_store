using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsBooks
{
    public class Serializer<T>
    {
        //public Serializer(){}

        public bool Serialize(string filepath, T whatToSerialize, out string message)
        {
            message = string.Empty; 

            //using (FileStream fs = new FileStream(filepath, FileMode.Create))
            using (StreamWriter fs = new StreamWriter(new FileStream(filepath, FileMode.Create), Encoding.UTF8))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces nS = new XmlSerializerNamespaces();
                    nS.Add(string.Empty, string.Empty);

                    serializer.Serialize(fs, whatToSerialize, nS);
                    return true;
                }
                catch (Exception e)
                {
                    message = "Can't serialize.";
                    return false; 
                }
            }
        }

        public bool Deserialize(string filepath, ref T whereToDeserialize, out string message) 
        {
            message = string.Empty;

            if (FileExists(filepath))
                //using (FileStream fs = new FileStream(filepath, FileMode.Open))
                using (StreamReader fs = new StreamReader(new FileStream(filepath, FileMode.Open), Encoding.UTF8))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));

                        whereToDeserialize = (T)serializer.Deserialize(fs);
                        //storeDataGridView.DataSource = Store.StoreBooksBindingList;
                        return true;
                    }
                    catch (Exception e)
                    { 
                        message = "Can't deserialize.";
                        return false; 
                    }
                }
            message = "File doesn't exist.";
            return false;
        }

        private bool FileExists(string path) 
        {
            return File.Exists(path);
        }
    }
}
