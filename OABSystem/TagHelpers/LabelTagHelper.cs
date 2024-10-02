using Microsoft.AspNetCore.Razor.TagHelpers;
using OABSystem.Util;

namespace OABSystem.TagHelpers
{
    [HtmlTargetElement( "label")]
    [HtmlTargetElement(Attributes= "asp-label-for")]
    public class LabelTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var forAttribute = output.Attributes["asp-label-for"];
            if (forAttribute != null)
            {
                var propertyName = forAttribute.Value.ToString();
                var labelText = propertyName.ToSeparatedString();
                output.Attributes.SetAttribute("for", labelText);
                output.Content.SetContent(labelText);
            }
        }
    }
}