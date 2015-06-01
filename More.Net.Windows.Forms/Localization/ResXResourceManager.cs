using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace More.Net.Common.Forms.Localization
{
    public class ResXResourceManager : ComponentResourceManager
    {
        public ResXResourceManager(String baseName, String directory, Assembly assembly)
        {
            directoryField = directory;
            baseNameField = baseName;
            assemblyField = assembly;
            ResourceSets = new Hashtable();
        }

        /// <summary>
        /// Retrieves resources depending on the current culture
        /// </summary>
        /// <remarks>
        /// When retrieving a new resource, this method first looks for the control/form's 
        /// localized external resx file.  If no file exists, it will use the assembly's embedded 
        /// resources.  It cannot, however, use both external and embedded resources with the same
        /// language tag, so it defaults to using the external file instead.
        /// </remarks>
        /// <param name="cultureInfo">The culture of the resource.</param>
        /// <param name="createIfNotExists">Ignored, assumed true</param>
        /// <param name="tryParents">Ignored, assumed true</param>
        /// <returns></returns>
        protected override ResourceSet InternalGetResourceSet(
            CultureInfo cultureInfo, Boolean createIfNotExists, Boolean tryParents)
        {
            if (ResourceSets.Contains(cultureInfo.Name))
            {
                return ResourceSets[cultureInfo.Name] as ResourceSet;
            }
            else
            {
                String fileName;
                ResourceSet resourceSet = null;
                fileName = String.Format("{0}\\{1}\\{2}.{3}", directoryField,
                    cultureInfo.Name, baseNameField, "resx");
                var asd = assemblyField.GetManifestResourceNames();
                if (File.Exists(fileName))
                    resourceSet = new ResXResourceSet(fileName);
                else
                {
                    Stream manifestResourceStream = assemblyField.GetManifestResourceStream(
                        "More.Net.UI." + baseNameField + ".resources");
                    if (manifestResourceStream != null)
                        resourceSet = new ResourceSet(manifestResourceStream);
                }
                if (resourceSet != null)
                    ResourceSets.Add(cultureInfo.Name, resourceSet);
                return resourceSet;
            }
        }

        public override void ApplyResources(Object value, String objectName, CultureInfo culture)
        {
            try
            {
                List<CultureInfo> cultures = new List<CultureInfo>();
                if (culture == null)
                    culture = Thread.CurrentThread.CurrentUICulture;
                cultures.Add(culture);
                while (culture.Name != "")
                {
                    culture = culture.Parent;
                    cultures.Add(culture);
                }

                for (Int32 i = cultures.Count - 1; i >= 0; i--)
                {
                    culture = cultures[i];
                    ResourceSet resourceSet = GetResourceSet(culture, true, true);
                    foreach (DictionaryEntry entry in resourceSet)
                    {
                        String key = entry.Key as String;
                        Int32 predicateIndex = key.IndexOf('.');
                        String objectKey = key.Substring(0, predicateIndex).TrimStart('>');
                        if (objectKey == objectName)
                        {
                            String predicateKey = key.Substring(predicateIndex + 1);
                            String propertyKey;
                            PropertyInfo propertyKeyInfo;
                            Object propertyValue = value;
                            while (predicateKey.Any(c => c == '.'))
                            {
                                predicateIndex = predicateKey.IndexOf('.');
                                propertyKey = predicateKey.Substring(0, predicateIndex);
                                propertyKeyInfo = propertyValue.GetType().
                                    GetProperties(BindingFlags.Instance | BindingFlags.Public).
                                    FirstOrDefault(p => p.Name == propertyKey);
                                propertyValue = propertyKeyInfo.GetValue(propertyValue);
                                predicateKey = predicateKey.Substring(predicateIndex + 1);
                            }
                            propertyKey = predicateKey;
                            propertyKeyInfo = propertyValue.GetType().GetProperty(propertyKey);
                            if (propertyKeyInfo != null && entry.Value != null &&
                                propertyKeyInfo.PropertyType.IsAssignableFrom(
                                    entry.Value.GetType()))
                                propertyKeyInfo.SetValue(propertyValue, entry.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                base.ApplyResources(value, objectName, culture);
            }
        }

        private Int32 maxDepth = 3;
        private Assembly assemblyField;
        private String directoryField;
        private String baseNameField;
    }
}
