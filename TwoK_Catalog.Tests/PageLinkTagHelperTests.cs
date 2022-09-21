using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using TwoK_Catalog.Infrastructure;
using TwoK_Catalog.Models.ViewModels;

namespace TwoK_Catalog.Tests
{
   public class PageLinkTagHelperTests
   {
        [Fact]
        public void CanGeneratePageLinks()
        {
            //Организация
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PageInfo { 
                    CurrentPage = 2, 
                    ItemsPerPage = 10, 
                    TotalItems = 28 
                },
                PageAction = "Test"
            };

            TagHelperContext context = new TagHelperContext(
                new TagHelperAttributeList(), 
                new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            //Действие
            helper.Process(context, output);

            //Утверждение
            Assert.Equal(@"<a href=""Test/Page1"">1</a><a href=""Test/Page2"">2</a><a href=""Test/Page3"">3</a>",
                output.Content.GetContent());
        }
   }
}
