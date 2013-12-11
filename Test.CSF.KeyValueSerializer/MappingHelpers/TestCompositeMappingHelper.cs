using System;
using NUnit.Framework;
using Moq;
using CSF.KeyValueSerializer.MappingHelpers;
using CSF.KeyValueSerializer.MappingModel;
using System.Collections.Generic;
using CSF.Reflection;

namespace Test.CSF.KeyValueSerializer.MappingHelpers
{
  [TestFixture]
  [Category("Public API")]
  public class TestCompositeMappingHelper
  {
    [Test]
    public void TestComponent()
    {
      var mapping = new CompositeMapping<DateTime>(new ClassMapping<Baz>(), Reflect.Property<Baz>(x => x.BazProperty));

      CompositeMappingHelper<Baz,DateTime> helper = new CompositeMappingHelper<Baz,DateTime>(mapping);

      helper.Component("Year", m => {});

      Assert.AreEqual(1, mapping.Components.Count, "Correct count of composite mappings");
      Assert.IsTrue(mapping.Components.ContainsKey("Year"), "Component contained with correct key");
    }
  }
}

