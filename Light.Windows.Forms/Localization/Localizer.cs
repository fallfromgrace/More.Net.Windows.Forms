using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;
using System.ComponentModel.Design;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace More.Net.Common.Forms.Localization
{
    /// <summary>
    /// A windows forms component that aids in localization by supporting the use of files in 
    /// addition to assemblies.
    /// </summary>
    [Designer(typeof(LocalizerDesigner))]
    [DesignerSerializer(typeof(LocalizerSerializer), typeof(CodeDomSerializer))]
    public class Localizer : Component
    {
        /// <summary>
        /// Gets or sets the directory path to the localized resources.
        /// </summary>
        public String Path
        {
            get;
            set;
        }

        public Localizer()
            : base()
        {
            Path = String.Empty;
        }

        static Localizer()
        {

        }

        public void GetResourceManager(Type type, out ComponentResourceManager resourceManager)
        {
            if (Directory.Exists(Path))
            {
                foreach (String directory in Directory.EnumerateDirectories(Path))
                {
                    foreach (String file in Directory.EnumerateFiles(directory))
                    {
                        if (file.Contains(type.Name))
                        {
                            resourceManager = new ResXResourceManager(
                                type.Name, Path, type.Assembly);
                            return;
                        }
                    }
                }
            }

            resourceManager = new ComponentResourceManager(type);
        }
    }
}
