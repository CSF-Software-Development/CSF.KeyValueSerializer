using System;
using NUnit.Framework;
using Moq;
using CSF.KeyValueSerializer;
using CSF.KeyValueSerializer.MappingHelpers;
using CSF.KeyValueSerializer.MappingModel;
using CSF.Entities;
using CSF.Reflection;

namespace Test.CSF.KeyValueSerializer
{
  [TestFixture]
  [Category("Public API")]
  public class TestSimpleMappingHelperExtensions
  {
    [Test]
    public void TestDeserializeAsInt32()
    {
      var parent = new Mock<IMapping>();
      var prop = Reflect.Property<Foo>(x => x.TestInteger);
      var mapping = new SimpleMapping<int>(parent.Object, prop);

      SimpleMappingHelper<Foo,int> helper = new SimpleMappingHelper<Foo,int>(mapping);

      var simple = helper.DeserializeAsInt32();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.DeserializationFunction, "Mapping function is not null.");
      Assert.IsInstanceOfType(typeof(Func<string,int>), mapping.DeserializationFunction, "Mapping function is correct type.");

      Assert.AreEqual(5, mapping.DeserializationFunction("5"), "Mapping function returns correct value.");
    }

    [Test]
    public void TestDeserializeAsEnum()
    {
      var parent = new Mock<IMapping>();
      var prop = Reflect.Property<Foo>(x => x.TestPropertyTwo);
      var mapping = new SimpleMapping<SampleEnum>(parent.Object, prop);

      SimpleMappingHelper<Foo,SampleEnum> helper = new SimpleMappingHelper<Foo,SampleEnum>(mapping);

      var simple = helper.DeserializeAsEnum();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.DeserializationFunction, "Mapping function is not null.");
      Assert.IsInstanceOfType(typeof(Func<string,SampleEnum>), mapping.DeserializationFunction, "Mapping function is correct type.");

      Assert.AreEqual(SampleEnum.Two, mapping.DeserializationFunction("Two"), "Mapping function returns correct value.");
    }

    [Test]
    public void TestDeserializeAsIdentity()
    {
      var parent = new Mock<IMapping>();
      var prop = Reflect.Property<Foo>(x => x.TestPropertyTwo);
      var mapping = new SimpleMapping<IIdentity<EntityType>>(parent.Object, prop);

      SimpleMappingHelper<Foo,IIdentity<EntityType>> helper = new SimpleMappingHelper<Foo,IIdentity<EntityType>>(mapping);

      var simple = helper.DeserializeAsIdentity();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.DeserializationFunction, "Mapping function is not null.");
      Assert.IsInstanceOfType(typeof(Func<string,IIdentity<EntityType>>), mapping.DeserializationFunction, "Mapping function is correct type.");

      Assert.AreEqual(new Identity<EntityType,int>(5), mapping.DeserializationFunction("5"), "Mapping function returns correct value.");
    }
  }
}

