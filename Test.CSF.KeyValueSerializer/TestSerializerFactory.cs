using System;
using NUnit.Framework;
using CSF.KeyValueSerializer;
using Moq;

namespace Test.CSF.KeyValueSerializer
{
  [TestFixture]
  public class TestSerializerFactory
  {
    #region testing simple creation of serializers

    [Test]
    public void TestBuildRefType()
    {
      var options = new Mock<SerializerOptions>();
      SerializerFactory factory = SerializerFactory.Create(options.Object);
      var serializer = factory.BuildRefType<Foo>();

      Assert.IsNotNull(serializer, "Serializer nullability.");
      Assert.IsInstanceOfType(typeof(ClassKeyValueSerializer<Foo>), serializer, "Serializer is correct type");
    }

    [Test]
    [Ignore("This functionality is not yet created.")]
    public void TestBuildValType()
    {
      var options = new Mock<SerializerOptions>();
      SerializerFactory factory = SerializerFactory.Create(options.Object);
      var serializer = factory.BuildValType<DateTime>();

      Assert.IsNotNull(serializer, "Serializer nullability.");
//      Assert.IsInstanceOfType(typeof(ValueTypeSerializer<DateTime>), serializer, "Serializer is correct type");
    }

    [Test]
    public void TestBuildRefColl()
    {
      var options = new Mock<SerializerOptions>();
      SerializerFactory factory = SerializerFactory.Create(options.Object);
      var serializer = factory.BuildRefColl<Foo>();

      Assert.IsNotNull(serializer, "Serializer nullability.");
      Assert.IsInstanceOfType(typeof(CollectionKeyValueSerializer<Foo>), serializer, "Serializer is correct type");
    }

    [Test]
    public void TestBuildValColl()
    {
      var options = new Mock<SerializerOptions>();
      SerializerFactory factory = SerializerFactory.Create(options.Object);
      var serializer = factory.BuildValColl<DateTime>();

      Assert.IsNotNull(serializer, "Serializer nullability.");
      Assert.IsInstanceOfType(typeof(ValueTypeCollectionKeyValueSerializer<DateTime>),
                              serializer,
                              "Serializer is correct type");
    }

    #endregion
  }
}

