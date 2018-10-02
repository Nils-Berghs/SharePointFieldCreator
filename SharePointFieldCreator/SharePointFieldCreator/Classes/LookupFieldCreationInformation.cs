using Microsoft.SharePoint.Client;
using System;
using System.Xml.Serialization;

namespace be.nibe.SharePointFieldCreator.Classes
{
    /// <summary>
    /// Helper to create a lookup field in a sharepoint list
    /// </summary>
    [XmlRoot("Field")]
    public class LookupFieldCreationInformation : FieldCreationInformation
    {
        /// <summary>
        /// The Identifier of the list in which information must be looked up
        /// </summary>
        [XmlAttribute("List")]
        public Guid ListGuid { get; set; }

        /// <summary>
        /// The name of the field in the lookuplist to display
        /// </summary>
        [XmlAttribute("ShowField")]
        public string FieldName { get; set; }

        /// <summary>
        /// Indicates if the lookupfield allows multiple selection
        /// </summary>
        [XmlAttribute("Mult")]
        public bool AllowMultiple
        {
            get; set;
        }

        /// <summary>
        /// Creates a LookupFieldCreationInformation
        /// </summary>
        /// <param name="displayName">The (display)name of the field to create</param>
        /// <param name="listGuid">The GUID list in which we must lookup </param>
        /// <param name="allowMultiple"></param>
        /// <param name="required">Sets if the property is marked as required</param>
        /// <param name="fieldName">The name of the field in the lookup list to display (default = title)</param>
        public LookupFieldCreationInformation(string displayName, Guid listGuid, bool allowMultiple, bool required = false, string fieldName = "Title") : base(displayName, FieldType.Lookup, required)
        {
            
            AllowMultiple = allowMultiple;
            ListGuid = listGuid;
            FieldName = fieldName;
        }

        /// <summary>
        /// Creates a LookupFieldCreationInformation Class
        /// </summary>
        public LookupFieldCreationInformation():base()
        {
            //empty constructor required for serialization
        }

        /// <inheritdoc />
        public override string ToXml()
        {
            string xml = base.ToXml();

            //Extremely dirty hack to fix the missing 'lookupmulti' from the fieldtype enum
            if (AllowMultiple)
                xml = xml.Replace("Type=\"Lookup\"", "Type=\"LookupMulti\"");
            return xml;
        }
    }

    /*
     <Field 
        ColName="int2" 
        StaticName="BusinessUnit" 
        SourceID="{2b95f14a-1581-4790-91f4-696f398328bc}" 
        Name="BusinessUnit" 
        DisplayName="BusinessUnit" 
        Type="Lookup" 
        RowOrdinal="0" 
        ID="{ad20c839-7b7e-4332-b92e-7c0f458330de}" 
        Required="FALSE" 
        ShowField="Title" 
        EnforceUniqueValues="FALSE" 
        RelationshipDeleteBehavior="None" 
        UnlimitedLengthInDocumentLibrary="FALSE" 
        List="{fa8f3777-d1e1-4a63-bbaf-fca8f403bcbb}"/>
     */
}
