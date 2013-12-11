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
  public class TestClassMappingHelper
  {
    #region fields

    private ClassMapping<Foo> _fooMapping;
    private ClassMapping<Bar> _barMapping;
    private ClassMapping<Baz> _bazMapping;

    #endregion

    #region setup

    [SetUp]
    public void Setup()
    {
      _fooMapping = new ClassMapping<Foo>();
      _barMapping = new ClassMapping<Bar>();
      _bazMapping = new ClassMapping<Baz>();
    }

    #endregion

    #region general methods

    [Test]
    public void TestUsingFactory()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);

      helper.UsingFactory(() => new Foo() { TestProperty = "This is a test" });

      Assert.IsInstanceOfType(typeof(Func<Foo>), _fooMapping.FactoryMethod, "Factory method is set.");
    }

    [Test]
    public void TestNamingPolicy()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);

      helper.NamingPolicy<TestNamingPolicy>();

      Assert.IsInstanceOfType(typeof(TestNamingPolicy), _fooMapping.KeyNamingPolicy, "Naming policy has been set.");
    }

    [Test]
    public void TestNamingPolicyWithFactory()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);

      helper.NamingPolicy<TestNamingPolicy>(map => new TestNamingPolicy(map) { TestString = "Test!" });

      Assert.IsInstanceOfType(typeof(TestNamingPolicy), _fooMapping.KeyNamingPolicy, "Naming policy has been set.");
    }

    #endregion

    #region _mapping the type itself

    [Test]
    public void TestMapAsSimple()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);

      var simple = helper.Simple();

      Assert.IsNotNull(simple, "Simple _mapping helper is not null");
      Assert.IsNotNull(_fooMapping.MapAs, "Map-as of parent object is not null");
      Assert.IsTrue(_fooMapping.MapAs is SimpleMapping<Foo>, "Map-as is of correct type");
    }

    [Test]
    public void TestMapAsComposite()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);

      var composite = helper.Composite();

      Assert.IsNotNull(composite, "Composite _mapping helper is not null");
      Assert.IsNotNull(_fooMapping.MapAs, "Map-as of parent object is not null");
      Assert.IsTrue(_fooMapping.MapAs is CompositeMapping<Foo>, "Map-as is of correct type");
    }

    #endregion

    #region adding mappings

    [Test]
    public void TestSimple()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);
      var simple = helper.Simple(x => x.TestInteger);

      Assert.IsNotNull(simple);
      Assert.AreEqual(1, _fooMapping.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(SimpleMapping<int>), _fooMapping.Mappings.First(), "Mapping is of correct type");
    }

    [Test]
    public void TestSimpleTwice()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);
      var simple = helper.Simple(x => x.TestInteger);
      var simple2 = helper.Simple(x => x.TestInteger);

      Assert.IsNotNull(simple);
      Assert.IsNotNull(simple2);
      Assert.AreEqual(1,
                      _fooMapping.Mappings.Count,
                      "Correct count of contained mappings, only one _mapping was created.");
      Assert.IsInstanceOfType(typeof(SimpleMapping<int>), _fooMapping.Mappings.First(), "Mapping is of correct type");
    }

    [Test]
    public void TestComposite()
    {
      ClassMappingHelper<Foo> helper = new ClassMappingHelper<Foo>(_fooMapping);
      var comp = helper.Composite(x => x.TestDateTime);

      Assert.IsNotNull(comp);
      Assert.AreEqual(1, _fooMapping.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(CompositeMapping<DateTime>),
                              _fooMapping.Mappings.First(),
                              "Mapping is of correct type");
    }

    [Test]
    public void TestCollection()
    {
      ClassMappingHelper<Baz> helper = new ClassMappingHelper<Baz>(_bazMapping);
      helper.Collection(x => x.TestCollection, m => {});

      Assert.AreEqual(1, _bazMapping.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(ReferenceTypeCollectionMapping<Bar>),
                              _bazMapping.Mappings.First(),
                              "Mapping is of correct type");
    }

    [Test]
    public void TestValueCollection()
    {
      ClassMappingHelper<Baz> helper = new ClassMappingHelper<Baz>(_bazMapping);
      helper.ValueCollection(x => x.TestValueCollection, m => {});

      Assert.AreEqual(1, _bazMapping.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(ValueTypeCollectionMapping<DateTime>),
                              _bazMapping.Mappings.First(),
                              "Mapping is of correct type");
    }

    [Test]
    public void TestClass()
    {
      ClassMappingHelper<Bar> helper = new ClassMappingHelper<Bar>(_barMapping);
      helper.Class(x => x.Foo, m => {});

      Assert.AreEqual(1, _barMapping.Mappings.Count, "Correct count of contained mappings");
      Assert.IsInstanceOfType(typeof(ClassMapping<Foo>),
                              _barMapping.Mappings.First(),
                              "Mapping is of correct type");
    }

    #endregion
  }
}

