using System;
using NUnit.Framework;
using CSF.KeyValueSerializer.MappingModel;
using Moq;
using CSF.Reflection;

namespace Test.CSF.KeyValueSerializer.MappingModel
{
  [TestFixture]
  public class TestCompositeComponentMapping
  {
    [Test]
    public void TestGetKeyName()
    {
      var root = new ClassMapping<Foo>();
      var parent = new CompositeMapping<DateTime>(root, Reflect.Property<Foo>(x => x.TestDateTime));
      var policy = new Mock<IKeyNamingPolicy>();
      CompositeComponentMapping<DateTime> mapping;

      mapping = new CompositeComponentMapping<DateTime>(parent, "Year");

      parent.AttachKeyNamingPolicy(x => policy.Object);

//      parent.Setup(x => x.KeyNamingPolicy).Returns(policy.Object);
      policy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDate");

      Assert.AreEqual("TestDateYear", mapping.GetKeyName());
    }
  }
}

