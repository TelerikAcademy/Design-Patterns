namespace ConsoleWebServer.Tests
{
    using ConsoleWebServer.Framework;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ActionDescriptorTests
    {
        [TestMethod]
        public void ConstructorWithNullRequestStringShouldSetDefaultNames()
        {
            var routeDescriptor = new ActionDescriptor(null);
            Assert.AreEqual(ActionDescriptor.DefaultControllerName, routeDescriptor.ControllerName);
            Assert.AreEqual(ActionDescriptor.DefaultActionName, routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithEmptyRequestStringShouldSetDefaultNames()
        {
            var routeDescriptor = new ActionDescriptor(string.Empty);
            Assert.AreEqual(ActionDescriptor.DefaultControllerName, routeDescriptor.ControllerName);
            Assert.AreEqual(ActionDescriptor.DefaultActionName, routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashShouldSetDefaultNames()
        {
            var routeDescriptor = new ActionDescriptor("/");
            Assert.AreEqual(ActionDescriptor.DefaultControllerName, routeDescriptor.ControllerName);
            Assert.AreEqual(ActionDescriptor.DefaultActionName, routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashStringShouldSetCorrectValues()
        {
            var routeDescriptor = new ActionDescriptor("/test");
            Assert.AreEqual("test", routeDescriptor.ControllerName);
            Assert.AreEqual(ActionDescriptor.DefaultActionName, routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashStringSlashShouldSetCorrectValues()
        {
            var routeDescriptor = new ActionDescriptor("/test/");
            Assert.AreEqual("test", routeDescriptor.ControllerName);
            Assert.AreEqual(ActionDescriptor.DefaultActionName, routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashStringSlashStringShouldSetCorrectValues()
        {
            var routeDescriptor = new ActionDescriptor("/test/method");
            Assert.AreEqual("test", routeDescriptor.ControllerName);
            Assert.AreEqual("method", routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashStringSlashStringSlashShouldSetCorrectValues()
        {
            var routeDescriptor = new ActionDescriptor("/test/method/");
            Assert.AreEqual("test", routeDescriptor.ControllerName);
            Assert.AreEqual("method", routeDescriptor.ActionName);
            Assert.AreEqual(string.Empty, routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashStringSlashStringSlashIntShouldSetCorrectValues()
        {
            var routeDescriptor = new ActionDescriptor("/test/method/inputdata");
            Assert.AreEqual("test", routeDescriptor.ControllerName);
            Assert.AreEqual("method", routeDescriptor.ActionName);
            Assert.AreEqual("inputdata", routeDescriptor.Parameter);
        }

        [TestMethod]
        public void ConstructorWithSlashStringSlashStringSlashIntSlashShouldSetCorrectValues()
        {
            var routeDescriptor = new ActionDescriptor("/test/method/inputdata/");
            Assert.AreEqual("test", routeDescriptor.ControllerName);
            Assert.AreEqual("method", routeDescriptor.ActionName);
            Assert.AreEqual("inputdata", routeDescriptor.Parameter);
        }
    }
}
