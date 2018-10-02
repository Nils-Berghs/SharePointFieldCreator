using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace be.nibe.SharePointFieldCreator.Classes
{
    /// <summary>
    /// Class to create a sharepoint Choice field
    /// </summary>
    [XmlRoot("Field")]
    public class ChoiceFieldCreationInformation : FieldCreationInformation
    {
        /// <summary>
        /// The list of choices
        /// </summary>
        [XmlArray("CHOICES")]
        [XmlArrayItem("CHOICE")]
        public List<string> Choices { get; set; }

        /// <summary>
        /// Constructor for a choice field
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="choices"></param>
        /// <param name="allowMultiple"></param>
        /// <param name="required"></param>
        public ChoiceFieldCreationInformation(string displayName, IEnumerable<string> choices, bool allowMultiple, bool required = false) : base(displayName, FieldType.Choice, required)
        {
            if (allowMultiple)
                FieldType = Microsoft.SharePoint.Client.FieldType.MultiChoice;
            Choices = choices.ToList();
        }

        /// <summary>
        /// Creates a ChoiceFieldCreationInformation Class 
        /// </summary>
        public ChoiceFieldCreationInformation() : base()
        {
            //empty constructor required for serialization
        }
    }
}
