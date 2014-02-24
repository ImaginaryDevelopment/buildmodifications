using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;

namespace BCustomBuildTasks
{
    // Type: Microsoft.Build.Tasks.ResolveAssemblyReference
    // Assembly: Microsoft.Build.Tasks.v12.0, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
    // MVID: 542F3922-76B2-44AD-A1C6-E5069DDB4D47
    // Assembly location: C:\Program Files (x86)\MSBuild\12.0\Bin\Microsoft.Build.Tasks.v12.0.dll

 
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Versioning;
    using System.Text;
    using System.Xml.Linq;

    
        public class BResolveAssemblyReference : ResolveAssemblyReference
        {
            public override bool Execute()
            {
                Log.LogMessage(MessageImportance.High, "BHook is in");
                var oldBuildEngine = this.BuildEngine;
                this.BuildEngine = new BBuildEngine(oldBuildEngine);
                Log.LogMessage(MessageImportance.High,"BuildEngine hooked");
                
                return base.Execute();
            }
        }
    }


