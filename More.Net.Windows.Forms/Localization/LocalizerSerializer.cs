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
    public class LocalizerSerializer : CodeDomSerializer
    {
        public override Object Deserialize(
            IDesignerSerializationManager manager,
            Object codeDomObject)
        {
            CodeDomSerializer baseSerializer = (CodeDomSerializer)manager
                .GetSerializer(typeof(Component), typeof(CodeDomSerializer));
            return baseSerializer.Deserialize(manager, codeDomObject);
        }

        public override Object Serialize(
            IDesignerSerializationManager manager,
            Object value)
        {
            CodeDomSerializer baseSerializer = (CodeDomSerializer)manager
                .GetSerializer(typeof(Component), typeof(CodeDomSerializer));

            Object codeObject = baseSerializer.Serialize(manager, value);

            if (codeObject is CodeStatementCollection)
            {
                CodeStatementCollection statements = (CodeStatementCollection)codeObject;
                CodeTypeDeclaration classTypeDeclaration =
                    (CodeTypeDeclaration)manager.GetService(typeof(CodeTypeDeclaration));
                CodeExpression typeofExpression = new CodeTypeOfExpression(
                    classTypeDeclaration.Name);
                CodeDirectionExpression outResourceExpression = new CodeDirectionExpression(
                    FieldDirection.Out, new CodeVariableReferenceExpression("resources"));
                CodeExpression rightCodeExpression =
                    new CodeMethodInvokeExpression(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            manager.GetName(value)),
                        "GetResourceManager",
                        new CodeExpression[] { typeofExpression, outResourceExpression });
                statements.Insert(0, new CodeExpressionStatement(rightCodeExpression));
            }
            return codeObject;
        }
    }
}
