using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension.QuickActions.Ribbon
{
    [Export(typeof(ISuggestedActionsSourceProvider))]
    [Name("CodeGen")]
    [ContentType("code")]
    internal class Provider : ISuggestedActionsSourceProvider
    {
        [Import(typeof(ITextStructureNavigatorSelectorService))]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }
        public ISuggestedActionsSource CreateSuggestedActionsSource(ITextView textView, ITextBuffer textBuffer)
        {
            return textBuffer == null || textView == null ? null : new Action(this, textView, textBuffer);
        }
    }
}
