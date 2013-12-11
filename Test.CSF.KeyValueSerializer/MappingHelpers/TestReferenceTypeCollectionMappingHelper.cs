using System;
using NUnit.Framework;
using Moq;
using CSF.KeyValueSerializer.MappingHelpers;
using CSF.KeyValueSerializer.MappingModel;
using System.Collections.Generic;
using System.Linq;

namespace Test.CSF.KeyValueSerializer.MappingHelpers
{
  [TestFixture]
  [Category("Public API")]
  public class TestReferenceTypeCollectionMappingHelper
  {
    #region general methods

    [Test]
    public void TestUsingFactory()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();
      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);

      Func<Bar> factory = () => new Bar("Wibble") { BarProperty = "Test!" };
      helper.UsingFactory(factory);

      Assert.IsNotNull(mapping.MapAs.FactoryMethod, "Factory method not null");
      Assert.AreSame(factory, mapping.MapAs.FactoryMethod, "Factory method is correct");
    }

    [Test]
    public void TestCollectionNamingPolicy()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();
      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);

      helper.CollectionNamingPolicy<TestNamingPolicy>();

      Assert.IsInstanceOfType(typeof(TestNamingPolicy), mapping.KeyNamingPolicy, "Correct naming policy type.");
    }

    [Test]
    public void TestCollectionNamingPolicyWithFactory()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();
      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);

      helper.CollectionNamingPolicy<TestNamingPolicy>(map => new TestNamingPolicy(map) { TestString = "Test!" });

      Assert.IsInstanceOfType(typeof(TestNamingPolicy), mapping.KeyNamingPolicy, "Correct naming policy type.");
    }

    #endregion

    #region mapping the type itself

    [Test]
    public void TestMapAsSimple()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);

      var simple = helper.Simple();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.MapAs.MapAs, "Map-as of parent object is not null");
      Assert.IsInstanceOfType(typeof(SimpleMapping<Bar>), mapping.MapAs.MapAs, "Map-as is of correct type");
    }

    [Test]
    public void TestMapAsComposite()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);

      var simple = helper.Composite();

      Assert.IsNotNull(simple, "Simple mapping helper is not null");
      Assert.IsNotNull(mapping.MapAs.MapAs, "Map-as of parent object is not null");
      Assert.IsInstanceOfType(typeof(CompositeMapping<Bar>), mapping.MapAs.MapAs, "Map-as is of correct type");
    }

    #endregion

    #region adding mappings

    [Test]
    public void TestSimple()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);
      var simple = helper.Simple(x => x.BarProperty);

      Assert.IsNotNull(simple);
      Assert.AreEqual(1, mapping.MapAs.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(SimpleMapping<string>), mapping.MapAs.Mappings.First(), "Mapping is of correct type");
    }

    [Test]
    public void TestSimpleTwice()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);
      var simple = helper.Simple(x => x.BarProperty);
      var simple2 = helper.Simple(x => x.BarProperty);

      Assert.IsNotNull(simple);
      Assert.IsNotNull(simple2);
      Assert.AreEqual(1, mapping.MapAs.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(SimpleMapping<string>), mapping.MapAs.Mappings.First(), "Mapping is of correct type");
    }

    [Test]
    public void TestComposite()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);
      var comp = helper.Composite(x => x.BarProperty);

      Assert.IsNotNull(comp);
      Assert.AreEqual(1, mapping.MapAs.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(CompositeMapping<string>),
                              mapping.MapAs.Mappings.First(),
                              "Mapping is of correct type");
    }

    [Test]
    public void TestCollection()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);
      helper.Collection(x => x.BazCollection, m => {});

      Assert.AreEqual(1, mapping.MapAs.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(ReferenceTypeCollectionMapping<Baz>),
                              mapping.MapAs.Mappings.First(),
                              "Mapping is of correct type");
    }

    [Test]
    public void TestValueCollection()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);
      helper.ValueCollection(x => x.ValueCollection, m => {});

      Assert.AreEqual(1, mapping.MapAs.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(ValueTypeCollectionMapping<DateTime>),
                              mapping.MapAs.Mappings.First(),
                              "Mapping is of correct type");
    }

    [Test]
    public void TestClass()
    {
      var mapping = new ReferenceTypeCollectionMapping<Bar>();

      ReferenceTypeCollectionMappingHelper<Baz,Bar> helper = new ReferenceTypeCollectionMappingHelper<Baz,Bar>(mapping);
      helper.Class(x => x.Foo, m => {});

      Assert.AreEqual(1, mapping.MapAs.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(ClassMapping<Foo>),
                              mapping.MapAs.Mappings.First(),
                              "Mapping is of correct type");
    }

    #endregion
  }
}

