using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Joernaal
{
    public static class HtmlDocumentExtensions
    {
        public static void ReplaceReferences(this HtmlDocument doc, string elementName, string attributeName, Dictionary<string, string> mapping)
        {
            try
            {
                foreach (var node in doc.DocumentNode.SelectNodes($"//{elementName}[@{attributeName}]"))
                {
                    var value = node.GetAttributeValue(attributeName, null);
                    if (value.StartsWith("/"))
                    {
                        value = value.Substring(1);
                    }
                    
                    if (mapping.ContainsKey(value))
                    {
                        var newValue = mapping[value];
                        // TODO: Make the following optional, since a offline website doesn't understand default documents
                        //if (newValue.EndsWith("/index.html"))
                        //{
                        //    newValue = newValue.Substring(0, newValue.Length - "index.html".Length);
                        //}
                        node.SetAttributeValue(attributeName, newValue);
                    }
                }
            }
            catch (NullReferenceException)
            {
                // The framework throws an exception when elements can't be found. Go figure.
            }
        }
    }
}