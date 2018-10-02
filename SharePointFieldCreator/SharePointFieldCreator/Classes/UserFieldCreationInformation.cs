using Microsoft.SharePoint.Client;
using System.Xml.Serialization;

namespace be.nibe.SharePointFieldCreator.Classes
{
    /// <summary>
    /// Helper to create a user field in a sharepoint list
    /// </summary>
    [XmlRoot("Field")]
    public class UserFieldCreationInformation:FieldCreationInformation
    {
        /// <summary>
        /// The mode to select: people only, or people and groups.
        /// </summary>
        [XmlAttribute("UserSelectionMode")]
        public FieldUserSelectionMode FieldUserSelectionMode { get; set; }

        /// <summary>
        /// Creates a UserFieldCreationInformation
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="fieldUserSelectionMode"></param>
        /// <param name="required"></param>
        public UserFieldCreationInformation(string displayName, FieldUserSelectionMode fieldUserSelectionMode, bool required = false) : base (displayName, FieldType.User, required)
        {
            FieldUserSelectionMode = fieldUserSelectionMode;
        }

        /// <summary>
        /// Creates a UserFieldCreationInformation
        /// </summary>
        public UserFieldCreationInformation() : base()
        {
            //empty constructor required for serialization
        }
    }
}
