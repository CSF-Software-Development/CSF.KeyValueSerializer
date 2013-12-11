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
  public class TestValueTypeCollectionMappingHelper
  {
    [Test]
    public void TestSimple()
    {
      var mapping = new ValueTypeCollectionMapping<DateTime>();
      ValueTypeCollectionMappingHelper<Bar,DateTime> helper = new ValueTypeCollectionMappingHelper<Bar,DateTime>(mapping);

      helper.Simple();

      Assert.IsNotNull(mapping.MapAs, "Map-as not null");
      Assert.IsInstanceOfType(typeof(SimpleMapping<DateTime>), mapping.MapAs, "Correct type");
    }

    [Test]
    public void TestComposite()
    {
      var mapping = new ValueTypeCollectionMapping<DateTime>();
      ValueTypeCollectionMappingHelper<Bar,DateTime> helper = new ValueTypeCollectionMappingHelper<Bar,DateTime>(mapping);

      helper.Composite();

      Assert.IsNotNull(mapping.MapAs, "Map-as not null");
      Assert.IsInstanceOfType(typeof(CompositeMapping<DateTime>), mapping.MapAs, "Correct type");
    }
  }
}

