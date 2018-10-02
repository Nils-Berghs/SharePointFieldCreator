# SharePointFieldCreator

This is a helper for SharePoint Addins to create fields in a custom list.

## Problem

When working with SharePoint addins it is sometimes required to create lists or list fields in the hostweb. Microsoft provides the [a SPFieldCollection.AddFieldAsXml "method"](https://msdn.microsoft.com/library/office/microsoft.sharepoint.client.fieldcollection.addfieldasxml.aspx) for this. I'm no fan of this 'method', I'll try to say this politely:

*If you give a monkey a keyboard and let him press random keys, chances are high that the result will be superior to AddFieldsAsXml*

There, I think I didn't insult the monkey too much.

The AddFieldAsXml method has an XML/CAML string as parameter that contains all settings for the field to be created. There are several things wrong with principle and its implementation
- Its very verbose, and reusability is none existing
- There is no compile time checking 
- Implementation is inconsitent across fields
- XML/CAML is case sensitive for some properties.

Some examples
- A XML/CAML string may look like this: *Field ID="62073696-9849-4894-a4eb-eea9923a314c" DisplayName="FieldName" Type="Text" Required="FALSE"*
- For Choice field, the type is different between single an multi select fields.
- For lookup filed, the type is different between single an multi select fields, but there is also an extra parameter required *Mult="TRUE"* (not 'Mult', not 'Multi')
- Booleans must be in upper case TRUE/FALSE (false, true, False and True will not work).

## Solution

The solution consists of a set of classes, for the different fieldtypes, that can generate the required XML. The advantages are:
- Compile time checking
- Code reuse
- Consistent implementation (the library tries to hide inconsistenties)

I take no credit for the idea, I based this on the work of [a Vadim Gremyachev](https://stackoverflow.com/questions/34657838/create-field-in-sharepoint-programmatically-using-csom-not-with-xml) 

An example of how to create a list and some fields:

```c#
using (ClientContext cc = ... )
{
    //create list
    string listName = "MyListName";
    ListCreationInformation listInfo = new ListCreationInformation();
    listInfo.Title = listName;
    listInfo.Url = listName;
    listInfo.TemplateType = (int)ListTemplateType.GenericList;
    List newList = cc.Web.Lists.Add(listInfo);
    cc.ExecuteQuery();

    //add a required text field
    newList.Fields.Add(new FieldCreationInformation("TextField", FieldType.Text, true);
    
    //add a choice field with multi select
    string[] options = {"option1", "option2", "option3"};
    newList.Fields.Add(new ChoiceFieldCreationInformation("Option", options, true));
    
    //add a people selector field, people only
    newList.Fields.Add(new UserFieldCreationInformation("Person", FieldUserSelectionMode.PeopleOnly));

    //add lookup field, for a field 'SomeField' for 'OtherList' (we need the Id of the other list)
    var otherList = cc.Web.Lists.GetByTitle("Otherlist");
    cc.Load(otherList, l => l.Id);
    cc.ExecuteQuery();
    newList.Fields.Add(new LookupFieldCreationInformation("Lookup", otherList.Id, false, false, "SomeField"));
    newList.Fields.Add(new LookupFieldCreationInformation("Department", depList.Id, true, false));
    cc.ExecuteQuery();
}
```

## Todo
- Not all field types have an implementation
- The implementation is dirty at times, there are two main issues:
+ The serialization of booleans is lower case by default, a simple replace now fixes this (there might be a better solution
* The FieldType enum is used to set the Type but this enum does not contain a value for LookupMulti, so once again this is fixed using string.replace (I don't see a better solution)

