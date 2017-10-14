using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions
{
    public class CALCompletionSource : ICompletionSource
    {
        
        private CALCompletionSourceProvider _sourceProvider;
        private ITextBuffer _textBuffer;

        public CALCompletionSource(CALCompletionSourceProvider sourceProvider, ITextBuffer textBuffer)
        {
            _sourceProvider = sourceProvider;
            _textBuffer = textBuffer;
        }

        void ICompletionSource.AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            //get tracking span
            ITrackingSpan trackingSpan = FindTokenSpanAtPosition(session.GetTriggerPoint(_textBuffer), session);

            //find current indent
            ITextSnapshot snapshot = session.TextView.TextSnapshot;
            SnapshotPoint currentPoint = trackingSpan.GetStartPoint(snapshot);
            ITextSnapshotLine snapshotLine = snapshot.GetLineFromPosition(currentPoint);

            string wordText = trackingSpan.GetText(snapshot);

            string lineText = snapshotLine.GetText();
            int lineStartPos = snapshotLine.Start.Position;
            int lineEndPos = snapshotLine.End.Position;
            int currentPos = currentPoint.Position;
            int indent = currentPos - lineStartPos;

            if (wordText == ".")
                indent++;

            //build completion list
            List<Completion> completionList = new List<Completion>();

            //insert snippets
            foreach (Snippet snippet in Session.Current.Settings.Snippets)
            {
                completionList.Add(new SnippetCompletion(snippet, indent));
            }

            //add completion set if it contains any entries
            if (completionList.Count > 0)
                completionSets.Add(new CompletionSet(
                    "Snippets",    //the non-localized title of the tab
                    "Snippets",    //the display title of the tab
                    trackingSpan,
                    completionList,
                    null));
        }

        private ITrackingSpan FindTokenSpanAtPosition(ITrackingPoint point, ICompletionSession session)
        {
            
            SnapshotPoint currentPoint = (session.TextView.Caret.Position.BufferPosition) - 1;
            ITextStructureNavigator navigator = _sourceProvider.NavigatorService.GetTextStructureNavigator(_textBuffer);
            TextExtent extent = navigator.GetExtentOfWord(currentPoint);
            return currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
        }

        private bool m_isDisposed;
        public void Dispose()
        {
            if (!m_isDisposed)
            {
                GC.SuppressFinalize(this);
                m_isDisposed = true;
            }
        }

    }
}
