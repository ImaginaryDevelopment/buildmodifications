using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace BCustomBuildTasks
{
    class BBuildEngine : IBuildEngine
    {
        private readonly IBuildEngine _buildEngine;

        public BBuildEngine(IBuildEngine buildEngine)
        {
            _buildEngine = buildEngine;
        }

        public bool BuildProjectFile(string projectFileName, string[] targetNames, System.Collections.IDictionary globalProperties, System.Collections.IDictionary targetOutputs)
        {
            return _buildEngine.BuildProjectFile(projectFileName, targetNames, globalProperties, targetOutputs);
        }

        public int ColumnNumberOfTaskNode
        {
            get { return _buildEngine.ColumnNumberOfTaskNode; }
        }

        public bool ContinueOnError
        {
            get { return _buildEngine.ContinueOnError; }
        }

        public int LineNumberOfTaskNode
        {
            get { return _buildEngine.LineNumberOfTaskNode; }
        }

        public void LogCustomEvent(CustomBuildEventArgs e)
        {
            _buildEngine.LogCustomEvent(e);
        }

        public void LogErrorEvent(BuildErrorEventArgs e)
        {

            _buildEngine.LogErrorEvent(e);
        }

        private string lastInterestingMessageIndent;
        public void LogMessageEvent(BuildMessageEventArgs e)
        {

            if (e.Importance != MessageImportance.High)
            {
                var interesting = new[]
                {"Unified primary", "chosen", "conflict", "Unified Dependency", "Could not resolve this reference"};

                if (interesting.Any(i=>e.Message.Contains(i)) ||
                
                    (lastInterestingMessageIndent != null && e.Message.StartsWith(lastInterestingMessageIndent)) || lastInterestingMessageIndent==string.Empty)
                {
                    var importanceField = e.GetType()
                        .GetField("importance", BindingFlags.NonPublic | BindingFlags.Instance);
                    Debug.WriteLine("^"+e.Message);
                    //_buildEngine.LogMessageEvent(new BuildMessageEventArgs("about to bump importance of message:" + e.Message, "B", e.SenderName,MessageImportance.High));

                    importanceField.SetValue(e, MessageImportance.High);
                    var indents = Regex.Match(e.Message, @"^(\s+)");
                    if (lastInterestingMessageIndent == null && indents.Success == false) //special condition, last message was interesting but had no indentation
                    {
                        lastInterestingMessageIndent = string.Empty;
                    } else if (string.IsNullOrEmpty(lastInterestingMessageIndent) && indents.Success)
                    {
                        lastInterestingMessageIndent = indents.Groups[1].Value;
                        _buildEngine.LogMessageEvent(
                            new BuildMessageEventArgs(
                                "Setting indent bumping to length:" + indents.Groups[1].Value.Length.ToString(), "B",
                                "B", MessageImportance.High));
                    }
                    


                }
                else
                {
                    Debug.WriteLine("_" + e.Message);
                    lastInterestingMessageIndent = null;
                }
            }
           
            _buildEngine.LogMessageEvent(e);

        }

        public void LogWarningEvent(BuildWarningEventArgs e)
        {
            _buildEngine.LogWarningEvent(e);
        }

        public string ProjectFileOfTaskNode
        {
            get { return _buildEngine.ProjectFileOfTaskNode; }
        }
    }
}
