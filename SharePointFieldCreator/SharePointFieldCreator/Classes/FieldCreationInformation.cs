using Microsoft.SharePoint.Client;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace be.nibe.SharePointFieldCreator.Classes
{
    /// <summary>
    /// This class fixes the mess that Microsoft implemented to create columns.
    /// It is a wrapper class that can generate the required XML for the AddFieldAsXml abomination 
    /// </summary>
    [XmlRoot("Field")]
    public class FieldCreationInformation
    {
        [XmlAttribute("ID")]
        public Guid Id { get; set; }

        [XmlAttribute()]
        public string DisplayName { get; set; }

        [XmlAttribute("Name")]
        public string InternalName { get; set; }

        [XmlIgnore()]
        public bool AddToDefaultView { get; set; }


        //public IEnumerable<KeyValuePair<string, string>> AdditionalAttributes { get; set; }

        /// <summary>
        /// The fieldtype
        /// </summary>
        [XmlAttribute("Type")]
        public FieldType FieldType { get; set; }

        [XmlAttribute()]
        public string Group { get; set; }

        /// <summary>
        /// Indicates if the field is required
        /// </summary>
        [XmlAttribute()]
        public bool Required { get; set; }


        /// <summary>
        /// This method is used to create the XML string required for the AddFieldsAsXml 
        /// 
        /// It creates the XML and changes all boolean fields to upper case
        /// </summary>
        /// <returns></returns>
        public virtual string ToXml()
        {
            string xml = null;
            var serializer = new XmlSerializer(GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;

            settings.OmitXmlDeclaration = true;
            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, this, emptyNamepsaces);
                xml = stream.ToString();
            }
            //Dirty hack to change the booleans to upper case
            if (xml != null)
                xml = xml.Replace("\"true\"", "\"TRUE\"").Replace("\"false\"", "\"FALSE\"");

            return xml;

        }


        /// <summary>
        /// Creates a FieldCreationInformation
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="fieldType"></param>
        /// <param name="required"></param>
        public FieldCreationInformation(string displayName, FieldType fieldType, bool required = false) : this()
        {
            DisplayName = displayName;
            FieldType = fieldType;
            Required = required;
        }

        /// <summary>
        /// Creates a FieldCreationInformation
        /// </summary>
        public FieldCreationInformation()
        {
            //empty constructor required for serialization
            Id = Guid.NewGuid();
        }


    }
}

