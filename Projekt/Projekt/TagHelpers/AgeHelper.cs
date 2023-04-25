using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.TagHelpers
{
    [HtmlTargetElement("age-helper", Attributes = ValueAttribute)]
    public class AgeHelper : TagHelper
    {
        private const string ValueAttribute = "age-helper-value";
        [HtmlAttributeName(ValueAttribute)] public int Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var today = DateTime.Today;
            var age = today.Year - Value;

            //if (Value.Date > today.AddYears(-age)) age--;

            string val = age.ToString();
            output.Content.SetContent(val);
        }
    }
}
