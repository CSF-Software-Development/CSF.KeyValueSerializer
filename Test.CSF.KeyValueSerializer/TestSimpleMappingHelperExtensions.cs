using System;
using NUnit.Framework;
using Moq;
using CSF.KeyValueSerializer;
using CSF.KeyValueSerializer.MappingHelpers;
using CSF.KeyValueSerializer.MappingModel;
using CSF.Entities;

namespace Test.CSF.KeyValueSerializer
{
  [TestFixture]
  public class TestSimpleMappingHelperExtensions
  {
    [Test]
    public void TestDeserializeAsInt32()
    {
      var mapping = new Mock<ISimpleMapping<int>>();
      mapping.SetupProperty(x => x.DeserializationFunction);

      SimpleMappingHelper<Foo,int> helper = new SimpleMappingHelper<Foo,int>(mapping.Object);

      var simple = helper.DeserializeAsInt32();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.Object.DeserializationFunction, "Mapping function is not null.");
      Assert.IsInstanceOfType(typeof(Func<string,int>), mapping.Object.DeserializationFunction, "Mapping function is correct type.");

      Assert.AreEqual(5, mapping.Object.DeserializationFunction("5"), "Mapping function returns correct value.");
    }

    [Test]
    public void TestDeserializeAsEnum()
    {
      var mapping = new Mock<ISimpleMapping<SampleEnum>>();
      mapping.SetupProperty(x => x.DeserializationFunction);

      SimpleMappingHelper<Foo,SampleEnum> helper = new SimpleMappingHelper<Foo,SampleEnum>(mapping.Object);

      var simple = helper.DeserializeAsEnum();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.Object.DeserializationFunction, "Mapping function is not null.");
      Assert.IsInstanceOfType(typeof(Func<string,SampleEnum>), mapping.Object.DeserializationFunction, "Mapping function is correct type.");

      Assert.AreEqual(SampleEnum.Two, mapping.Object.DeserializationFunction("Two"), "Mapping function returns correct value.");
    }

    [Test]
    public void TestDeserializeAsIdentity()
    {
      var mapping = new Mock<ISimpleMapping<IIdentity<EntityType>>>();
      mapping.SetupProperty(x => x.DeserializationFunction);

      SimpleMappingHelper<Foo,IIdentity<EntityType>> helper = new SimpleMappingHelper<Foo,IIdentity<EntityType>>(mapping.Object);

      var simple = helper.DeserializeAsIdentity();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.Object.DeserializationFunction, "Mapping function is not null.");
      Assert.IsInstanceOfType(typeof(Func<string,IIdentity<EntityType>>), mapping.Object.DeserializationFunction, "Mapping function is correct type.");

      Assert.AreEqual(new Identity<EntityType,int>(5), mapping.Object.DeserializationFunction("5"), "Mapping function returns correct value.");
    }
  }
}

