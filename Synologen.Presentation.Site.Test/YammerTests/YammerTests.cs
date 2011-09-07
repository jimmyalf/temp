using System;
using System.Linq;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Test.YammerTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.YammerTests.TestHelpers;
using Shouldly;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.YammerTests
{
    [TestFixture]
    [Category("YammerTester")]
    public class When_loading_list_of_messages_in_view : YammerTestbase
    {
        private readonly JsonMessageModel _objects;

        public When_loading_list_of_messages_in_view()
        {
            _objects = JsonConvert.DeserializeObject<JsonMessageModel>(YammerFactory.GetJson());
            Context = () =>
            {
                MockedService.Setup(x => x.GetJson(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).Returns(YammerFactory.GetJson);
                MockedService.Setup(x => x.GetJson(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(YammerFactory.GetJson);
                MockedView.Setup(x => x.NumberOfMessages).Returns(10);
            };
            Because = presenter => presenter.View_Load(null, new EventArgs());
        }

        [Test]
        public void Json_is_parsed_into_yammer_list_items()
        {
            AssertUsing(view => view.Model.Messages.And(_objects.messages).Do((viewItem, parsedItem) =>
            {
                var author = _objects.references.FirstOrDefault(x => x.id == parsedItem.sender_id);

                viewItem.Content.ShouldBe(parsedItem.body.plain);
                viewItem.AuthorName.ShouldBe(author.full_name);
                viewItem.AuthorImageUrl.ShouldBe(author.mugshot_url);
            }));
        }
    }

    [TestFixture]
    [Category("YammerTester")]
    public class When_loading_list_of_messages_in_view_with_empty_json : YammerTestbase
    {
        public When_loading_list_of_messages_in_view_with_empty_json()
        {
            Context = () =>
            {
                MockedService.Setup(x => x.GetJson(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).Returns(String.Empty);
                MockedView.Setup(x => x.NumberOfMessages).Returns(10);
            };
            Because = presenter => presenter.View_Load(null, new EventArgs());
        }

        [Test]
        public void Json_is_parsed_into_yammer_list_items()
        {
            AssertUsing(view => view.Model.Messages.Count().ShouldBe(0));
        }
    }
}
