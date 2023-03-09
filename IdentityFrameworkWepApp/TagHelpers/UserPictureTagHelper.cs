﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IdentityFrameworkWepApp.TagHelpers
{
    public class UserPictureTagHelper:TagHelper
    {
        public string PictureUrl { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            if (string.IsNullOrEmpty(PictureUrl))
            {
                output.Attributes.SetAttribute("src", "/Images/default.png");
            }
            else 
            {
                output.Attributes.SetAttribute("src", $"/Images/{PictureUrl}");
            }    
           
        }
    }
}
