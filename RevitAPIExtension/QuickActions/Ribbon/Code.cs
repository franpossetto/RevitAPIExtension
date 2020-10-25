using EnvDTE;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Task = System.Threading.Tasks.Task;

namespace RevitAPIExtension.QuickActions.Ribbon
{
    internal class Code : ISuggestedAction
    {
        private string m_display;
        public bool HasActionSets
        {
            get { return false; }
        }
        public string DisplayText
        {
            get { return m_display; }
        }
        public ImageMoniker IconMoniker
        {
            get { return default; }
        }
        public string IconAutomationText
        {
            get
            {
                return null;
            }
        }
        public string InputGestureText
        {
            get
            {
                return null;
            }
        }
        public bool HasPreview
        {
            get { return true; }
        }
        public string ClassName { get; set; }
        public Code(string className)
        {
            ClassName = className;
            m_display = $"Create button for {className}";
        }
        public Task<object> GetPreviewAsync(CancellationToken cancellationToken)
        {
            var textBlock = new TextBlock
            {
                Padding = new Thickness(5)
            };
            textBlock.Inlines.Add(new Run() { Text =  $"Generate code to create the button {ClassName} in the Revit Ribbon bar"});
            return Task.FromResult<object>(textBlock);
        }
        public Task<IEnumerable<SuggestedActionSet>> GetActionSetsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<SuggestedActionSet>>(null);
        }
        public void Invoke(CancellationToken cancellationToken)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            object result = null;
            dte.Commands.Raise("6f929c1e-9ddd-444e-b4b9-fdbf5a2c2253", 4129, null, ref result);
        }
        public void Dispose()
        {
        }
        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Parse(RevitAPIExtensionPackage.PackageGuidString);
            return true;
        }
    }
}
