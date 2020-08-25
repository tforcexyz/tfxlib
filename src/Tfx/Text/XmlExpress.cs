using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Xyz.TForce.Text
{

  public class XmlExpress
  {

    public static string SerializeObject(object obj, Dictionary<string, string> namespaces = null)
    {
      string result;
      XmlSerializer serializer = new XmlSerializer(obj.GetType());
      StringBuilder stringBuilder = new StringBuilder();
      using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings { Encoding = Encoding.UTF8 }))
      {
        if (namespaces == null)
        {
          serializer.Serialize(xmlWriter, obj);
        }
        else
        {
          XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
          foreach (string prefix in namespaces.Keys)
          {
            string @namespace = namespaces[prefix];
            xns.Add(prefix, @namespace);
          }
          serializer.Serialize(xmlWriter, obj, xns);
        }
        result = stringBuilder.ToString();
      }
      return result;
    }

    public static TResult DeserializeObject<TResult>(string xml)
    {
      TResult result;
      XmlSerializer serializer = new XmlSerializer(typeof(TResult));
      using (TextReader stringReader = new StringReader(xml))
      {
        using (XmlReader xmlReader = new XmlTextReader(stringReader))
        {
          result = (TResult)serializer.Deserialize(xmlReader);
        }
      }
      return result;
    }
  }
}
