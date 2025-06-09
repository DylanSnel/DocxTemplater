using System.Globalization;

namespace DocxTemplater
{
    public class ProcessSettings
    {

        /// <summary>
        /// Output culture of the document
        /// </summary>
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentUICulture;

        public BindingErrorHandling BindingErrorHandling { get; set; } = BindingErrorHandling.ThrowException;

        /// <summary>
        /// When enabled, this option removes leading or trailing newlines around template directives (e.g., {{#...}}, {{/}}) 
        /// from the final output. This allows templates to be more readable without affecting rendered formatting.
        /// default: false
        /// </summary>
        public bool IgnoreLineBreaksAroundTags { get; set; }

        /// <summary>
        /// When enabled, paragraphs that only contained template blocks and no other content are removed from the final output.
        /// This ensures that empty paragraphs aren't left behind when template blocks are processed and removed.
        /// default: true
        /// </summary>
        public bool RemoveParagraphsContainingOnlyBlocks { get; set; } = true;

        public static ProcessSettings Default { get; } = new();
    }
}
