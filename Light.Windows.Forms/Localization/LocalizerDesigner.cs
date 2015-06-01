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
    public class LocalizerDesigner : ComponentDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            IDesignerHost dh = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (dh == null)
                return;

            PropertyInfo innerListProperty = dh.Container.Components.GetType().GetProperty(
                "InnerList",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            ArrayList innerList = innerListProperty.GetValue(
                dh.Container.Components, null) as ArrayList;

            if (innerList == null)
                return;
            if (innerList.IndexOf(component) <= 1)
                return;

            innerList.Remove(component);
            innerList.Insert(0, component);
        }
    }
}
