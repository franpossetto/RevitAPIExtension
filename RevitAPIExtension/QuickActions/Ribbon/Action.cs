using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RevitAPIExtension.QuickActions.Ribbon
{
    internal class Action : ISuggestedActionsSource
    {
        private readonly Provider m_factory;
        private readonly ITextBuffer m_textBuffer;
        private readonly ITextView m_textView;
        public Action(Provider testSuggestedActionsSourceProvider, ITextView textView, ITextBuffer textBuffer)
        {
            m_factory = testSuggestedActionsSourceProvider;
            m_textBuffer = textBuffer;
            m_textView = textView;
        }
        private bool TryGetWordUnderCaret(out TextExtent wordExtent)
        {
            ITextCaret caret = m_textView.Caret;
            SnapshotPoint point;

            if (caret.Position.BufferPosition > 0)
            {
                point = caret.Position.BufferPosition - 1;
            }
            else
            {
                wordExtent = default;
                return false;
            }
            ITextStructureNavigator navigator = m_factory.NavigatorService.GetTextStructureNavigator(m_textBuffer);

            wordExtent = navigator.GetExtentOfWord(point);
            return true;
        }
        public Task<bool> HasSuggestedActionsAsync(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                string text = range.GetText();
                var regex = new Regex(@"^\s*\w*\s*class\s*(?'className'\w+)\s*:\s*IExternalCommand", RegexOptions.Multiline);
                var match = regex.Match(text);
                return match.Success;
            });
        }
        public IEnumerable<SuggestedActionSet> GetSuggestedActions(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            string text = range.GetText();
            var regex = new Regex(@"^\s*\w*\s*class\s*(?'className'\w+)\s*:\s*IExternalCommand", RegexOptions.Multiline);
            var match = regex.Match(text);
            if (match.Success)
            {
                string className = match.Groups["className"].Value;
                var codeGenAction = new Code(className);
                return new SuggestedActionSet[] { new SuggestedActionSet(new ISuggestedAction[] { codeGenAction }) };
            }
            return Enumerable.Empty<SuggestedActionSet>();
        }
        public event EventHandler<EventArgs> SuggestedActionsChanged;
        public void Dispose()
        {
        }

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            // This is a sample provider and doesn't participate in LightBulb telemetry
            telemetryId = Guid.Parse(RevitAPIExtensionPackage.PackageGuidString);
            return true;
        }
    }
}
