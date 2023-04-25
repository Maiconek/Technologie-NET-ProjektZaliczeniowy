using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Projekt.TagHelpers
{
    [HtmlTargetElement("recommend-movie")]
    public class RecommendMovieHelper : TagHelper
    {
        [HtmlAttributeName("path")]
        public string Path { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) 
        {
            string[] lines = File.ReadAllLines(Path);
            Random rnd = new Random();
            int random = rnd.Next(0, lines.Length);

            output.Content.AppendHtml("<p class='movie-recommendation'>" + lines[random] + "</p>");
        }
    }
}
