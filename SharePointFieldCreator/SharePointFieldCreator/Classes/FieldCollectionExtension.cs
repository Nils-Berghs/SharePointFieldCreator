using Microsoft.SharePoint.Client;

namespace be.nibe.SharePointFieldCreator.Classes
{
    /// <summary>
    /// This class fixes the mess that Microsoft implemented to create columns.
    /// It hides the AddFieldAsXml abomination (can't call it a method, even if I wanted to)
    /// </summary>
    public static class FieldCollectionExtension
    {

        /// <summary>
        /// Add a field to a fieldCollection based on a FieldCreationInformation object
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Field Add(this FieldCollection fields, FieldCreationInformation info)
        {
            var fieldSchema = info.ToXml();
            return fields.AddFieldAsXml(fieldSchema, info.AddToDefaultView, AddFieldOptions.AddFieldToDefaultView);
        }
    }
}
