﻿using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GroceryStore.Web.Infrastructure.Helpers.Messages
{
    public class MessageTagHelper : TagHelper
    {
        public MessageType Type { get; set; }

        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var result = new StringBuilder();
            result
                .Append($"<div class=\"alert alert-{Type.ToString().ToLower()}\">")
                .Append("<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>")
                .Append(Message)
                .Append("</div>");

            output.Content.SetHtmlContent(result.ToString());
        }
    }
}
