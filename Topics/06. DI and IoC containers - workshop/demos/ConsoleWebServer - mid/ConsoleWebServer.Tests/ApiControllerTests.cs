namespace ConsoleWebServer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ConsoleWebServer.Application.Controllers;
    using ConsoleWebServer.Framework;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ApiControllerTests
    {
        [TestMethod]
        public void ReturnMeActionReturnsParameterInTheJsonObject()
        {
            const string Parameter = "someParam123";

            var request = new Mock<IHttpRequest>();
            request.Setup(x => x.ProtocolVersion).Returns(new Version(1, 1));

            var actionResult = new ApiController(request.Object);
            var resultBody = actionResult.ReturnMe(Parameter).GetResponse().Body;

            Assert.AreEqual(string.Format("{{\"param\":\"{0}\"}}", Parameter), resultBody);
        }

        [TestMethod]
        public void ReturnMeActionReturnsJsonContentType()
        {
            const string Parameter = "someParam123";

            var request = new Mock<IHttpRequest>();
            request.Setup(x => x.ProtocolVersion).Returns(new Version(1, 1));

            var actionResult = new ApiController(request.Object);
            var contentType = actionResult.ReturnMe(Parameter).GetResponse().Headers["Content-Type"].First();

            Assert.AreEqual("application/json", contentType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateWithCorsActionThrowsAnExceptionWhenNotGivenReferer()
        {
            const string Parameter = "somedomain";

            var request = new Mock<IHttpRequest>();
            request.Setup(x => x.ProtocolVersion).Returns(new Version(1, 1));
            var headers = new Dictionary<string, ICollection<string>>();
            request.SetupGet(x => x.Headers).Returns(headers);

            var actionResult = new ApiController(request.Object);
            actionResult.GetDateWithCors(Parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateWithCorsActionThrowsAnExceptionWhenGivenEmptyReferer()
        {
            const string Parameter = "somedomain";
            var request = this.GetMockedRequestWithRefererHeader(string.Empty);

            var actionResult = new ApiController(request);
            actionResult.GetDateWithCors(Parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateWithCorsActionThrowsAnExceptionWhenGivenWrongRefererAsParameter()
        {
            const string Parameter = "somedomain";
            var request = this.GetMockedRequestWithRefererHeader("otherdomain");

            var actionResult = new ApiController(request);
            actionResult.GetDateWithCors(Parameter);
        }

        [TestMethod]
        public void GetDateWithCorsActionReturnsJsonContentType()
        {
            const string Parameter = "somedomain";
            var request = this.GetMockedRequestWithRefererHeader("somedomain");

            var actionResult = new ApiController(request);
            var contentType = actionResult.GetDateWithCors(Parameter).GetResponse().Headers["Content-Type"].First();

            Assert.AreEqual("application/json", contentType);
        }

        [TestMethod]
        public void GetDateWithCorsActionShouldReturnValidJson()
        {
            const string Parameter = "somedomain";
            var request = this.GetMockedRequestWithRefererHeader("somedomain.com");

            var expectedJson =
                string.Format(
                    "{{\"date\":\"{0:yyyy-MM-dd}\",\"moreInfo\":\"Data available for somedomain\"}}",
                    DateTime.Now);

            var actionResult = new ApiController(request);
            var responseBody = actionResult.GetDateWithCors(Parameter).GetResponse().Body;

            Assert.AreEqual(expectedJson, responseBody);
        }

        private IHttpRequest GetMockedRequestWithRefererHeader(string refererHeader)
        {
            var request = new Mock<IHttpRequest>();
            request.Setup(x => x.ProtocolVersion).Returns(new Version(1, 1));
            var headers = new Dictionary<string, ICollection<string>>
                              {
                                  {
                                      "Referer",
                                      new List<string> { refererHeader }
                                  }
                              };
            request.SetupGet(x => x.Headers).Returns(headers);
            return request.Object;
        }
    }
}
