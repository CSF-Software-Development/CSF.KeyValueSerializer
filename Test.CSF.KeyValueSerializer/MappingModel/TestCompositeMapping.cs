using System;
using NUnit.Framework;
using Moq;
using CSF.KeyValueSerializer.MappingModel;
using CSF.Reflection;
using System.Collections.Generic;

namespace Test.CSF.KeyValueSerializer.MappingModel
{
  [TestFixture]
  public class TestCompositeMapping
  {
    #region general tests

    [Test]
    public void TestGetKeyName()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("Foo");

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var componentMap = new CompositeComponentMapping<DateTime>(mapping, "Year");
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);
      mapping.Components.Add("Year", componentMap);

      Assert.AreEqual("FooYear", mapping.GetKeyName("Year", new int[0]));
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(ArgumentException))]
    public void TestGetKeyNameMissingComponent()
    {
      var parent = new Mock<IMapping>();
      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      mapping.GetKeyName("Year", new int[0]);
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(InvalidMappingException),
                       ExpectedMessage = "A composite mapping must contain at least one component to be valid.")]
    public void TestValidateEnoughComponents()
    {
      var parent = new Mock<IMapping>();
      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      mapping.Validate();
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(InvalidMappingException),
                       ExpectedMessage = "This composite mapping is 'useless'.  It must either expose a deserialization function or all of its components must expose serialization functions.")]
    public void TestValidateUseless()
    {
      var parent = new Mock<IMapping>();

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var componentMap = new CompositeComponentMapping<DateTime>(mapping, "Year");
      mapping.Components.Add("Year", componentMap);
      mapping.Validate();
    }

    #endregion

    #region serialization

    [Test]
    public void TestSerializeSuccess()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);
      DateTime date = DateTime.Today;

      yearComponent.Renderer = (DateTime data, out string rendered) => {
        rendered = data.Year.ToString();
        return true;
      };
      monthComponent.Renderer = (DateTime data, out string rendered) => {
        rendered = data.Month.ToString();
        return true;
      };
      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);

      IDictionary<string,string> result;
      bool success = mapping.Serialize(date, out result, new int[0]);

      Assert.IsTrue(success, "Success");
      Assert.IsNotNull(result, "Result nullability");
      Assert.AreEqual(2, result.Count, "Result count");
      Assert.IsTrue(result["TestDateTimeYear"] == date.Year.ToString(), "First component");
      Assert.IsTrue(result["TestDateTimeMonth"] == date.Month.ToString(), "Second component");
    }

    [Test]
    public void TestSerializeAndWriteFlag()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);
      DateTime date = DateTime.Today;

      yearComponent.Renderer = (DateTime data, out string rendered) => {
        rendered = data.Year.ToString();
        return true;
      };
      monthComponent.Renderer = (DateTime data, out string rendered) => {
        rendered = data.Month.ToString();
        return true;
      };
      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);
      mapping.FlagKey = "date";

      IDictionary<string,string> result;
      bool success = mapping.Serialize(date, out result, new int[0]);

      Assert.IsTrue(success, "Success");
      Assert.IsNotNull(result, "Result nullability");
      Assert.AreEqual(3, result.Count, "Result count");
      Assert.IsTrue(result["TestDateTimeYear"] == date.Year.ToString(), "First component");
      Assert.IsTrue(result["TestDateTimeMonth"] == date.Month.ToString(), "Second component");
      Assert.IsTrue(result["date"] == Boolean.TrueString, "Flag");
    }

    [Test]
    public void TestSerializeComponentFailure()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);
      DateTime date = DateTime.Today;

      yearComponent.Renderer = (DateTime data, out string rendered) => {
        rendered = data.Year.ToString();
        return true;
      };
      monthComponent.Renderer = (DateTime data, out string rendered) => {
        rendered = null;
        return false;
      };
      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);

      IDictionary<string,string> result;
      bool success = mapping.Serialize(date, out result, new int[0]);

      Assert.IsFalse(success, "Success");
      Assert.IsNull(result, "Result nullability");
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(InvalidMappingException))]
    public void TestSerializeNoComponents()
    {
      var parent = new Mock<IMapping>();
      DateTime date = DateTime.Today;

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));

      IDictionary<string,string> result;
      mapping.Serialize(date, out result, new int[0]);
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(InvalidOperationException))]
    public void TestSerializeNoSerializationFunction()
    {
      var yearComponent = new Mock<CompositeComponentMapping<DateTime>>();
      var monthComponent = new Mock<CompositeComponentMapping<DateTime>>();
      var parent = new Mock<IMapping>();
      DateTime date = DateTime.Today;

      yearComponent.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("FooYear");
      yearComponent.SetupGet(x => x.SerializationFunction).Returns(d => d.Year.ToString());
      yearComponent.SetupGet(x => x.ComponentIdentifier).Returns("Year");
      monthComponent.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("FooMonth");
      monthComponent.SetupGet(x => x.SerializationFunction).Returns((Func<DateTime,string>) null);
      monthComponent.SetupGet(x => x.ComponentIdentifier).Returns("Month");

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      mapping.Components.Add("Year", yearComponent.Object);
      mapping.Components.Add("Month", monthComponent.Object);

      IDictionary<string,string> result;
      mapping.Serialize(date, out result, new int[0]);
    }

    #endregion

    #region deserialization

    [Test]
    public void TestDeserializeSuccess()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      DateTime date = DateTime.Today;

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);
      mapping.Parser = (IDictionary<object,string> components, out DateTime result) => {
        bool outcome;

        try
        {
          result = new DateTime(Int32.Parse(components["Year"]), Int32.Parse(components["Month"]), 1);
          outcome = true;
        }
        catch(Exception)
        {
          outcome = false;
          result = default(DateTime);
        }

        return outcome;
      };
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);

      IDictionary<string,string> data = new Dictionary<string, string>();
      data.Add("TestDateTimeYear", date.Year.ToString());
      data.Add("TestDateTimeMonth", date.Month.ToString());

      DateTime output;
      bool success = mapping.Deserialize(data, out output, new int[0]);

      Assert.IsTrue(success, "Success");
      Assert.AreEqual(date.Year, output.Year, "Year");
      Assert.AreEqual(date.Month, output.Month, "Month");
    }

    [Test]
    public void TestDeserializeFlagFailure()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      DateTime date = DateTime.Today;

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);
      mapping.Parser = (IDictionary<object,string> components, out DateTime result) => {
        bool outcome;

        try
        {
          result = new DateTime(Int32.Parse(components["Year"]), Int32.Parse(components["Month"]), 1);
          outcome = true;
        }
        catch(Exception)
        {
          outcome = false;
          result = default(DateTime);
        }

        return outcome;
      };
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);
      mapping.FlagKey = "flag";

      IDictionary<string,string> data = new Dictionary<string, string>();
      data.Add("TestDateTimeYear", date.Year.ToString());
      data.Add("TestDateTimeMonth", date.Month.ToString());

      DateTime output;
      bool success = mapping.Deserialize(data, out output, new int[0]);

      Assert.IsFalse(success, "Success");
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(InvalidMappingException))]
    public void TestDeserializeNoComponents()
    {
      var parent = new Mock<IMapping>();
      DateTime date = DateTime.Today;

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      mapping.DeserializationFunction = dict => new DateTime(Int32.Parse(dict["Year"]), Int32.Parse(dict["Month"]), 1);

      IDictionary<string,string> data = new Dictionary<string, string>();
      data.Add("FooYear", date.Year.ToString());
      data.Add("FooMonth", date.Month.ToString());

      DateTime output;
      mapping.Deserialize(data, out output, new int[0]);
    }

    [Test]
    public void TestDeserializeAllComponentsMissing()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);
      mapping.Parser = (IDictionary<object,string> components, out DateTime result) => {
        bool outcome;

        try
        {
          result = new DateTime(Int32.Parse(components["Year"]), Int32.Parse(components["Month"]), 1);
          outcome = true;
        }
        catch(Exception)
        {
          outcome = false;
          result = default(DateTime);
        }

        return outcome;
      };
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);

      IDictionary<string,string> data = new Dictionary<string, string>();

      DateTime output;
      bool success = mapping.Deserialize(data, out output, new int[0]);

      Assert.IsFalse(success, "Success");
    }

    [Test]
    public void TestDeserializeSomeComponentsMissing()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      DateTime date = DateTime.Today;

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);
      mapping.Parser = (IDictionary<object,string> components, out DateTime result) => {
        bool outcome;

        try
        {
          result = new DateTime(Int32.Parse(components["Year"]), Int32.Parse(components["Month"]), 1);
          outcome = true;
        }
        catch(Exception)
        {
          outcome = false;
          result = default(DateTime);
        }

        return outcome;
      };
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);

      IDictionary<string,string> data = new Dictionary<string, string>();
      data.Add("TestDateTimeYear", date.Year.ToString());

      DateTime output;
      bool success = mapping.Deserialize(data, out output, new int[0]);

      Assert.IsFalse(success, "Success");
    }

    [Test]
    [ExpectedException]
    public void TestDeseralizeException()
    {
      var parent = new Mock<IMapping>();
      var namingPolicy = new Mock<IKeyNamingPolicy>();
      DateTime date = DateTime.Today;

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      var yearComponent = new CompositeComponentMapping<DateTime>(mapping, "Year");
      var monthComponent = new CompositeComponentMapping<DateTime>(mapping, "Month");
      namingPolicy.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("TestDateTime");

      mapping.Components.Add("Year", yearComponent);
      mapping.Components.Add("Month", monthComponent);
      mapping.Parser = (IDictionary<object,string> components, out DateTime result) => {
        throw new Exception("This is a test exception");
      };
      mapping.AttachKeyNamingPolicy(x => namingPolicy.Object);

      IDictionary<string,string> data = new Dictionary<string, string>();
      data.Add("TestDateTimeYear", date.Year.ToString());
      data.Add("TestDateTimeMonth", date.Month.ToString());

      DateTime output;
      bool success = mapping.Deserialize(data, out output, new int[0]);

      Assert.IsFalse(success, "Success");
    }

    [Test]
    [ExpectedException(ExceptionType = typeof(MandatorySerializationException))]
    public void TestDeserializeMandatoryFailure()
    {
      var yearComponent = new Mock<CompositeComponentMapping<DateTime>>();
      var monthComponent = new Mock<CompositeComponentMapping<DateTime>>();
      var parent = new Mock<IMapping>();
      DateTime date = DateTime.Today;

      yearComponent.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("FooYear");
      yearComponent.SetupGet(x => x.ComponentIdentifier).Returns("Year");
      monthComponent.Setup(x => x.GetKeyName(It.IsAny<int[]>())).Returns("FooMonth");
      monthComponent.SetupGet(x => x.ComponentIdentifier).Returns("Month");

      CompositeMapping<DateTime> mapping = new CompositeMapping<DateTime>(parent.Object,
                                                                          Reflect.Property<Foo>(x => x.TestDateTime));
      mapping.Components.Add("Year", yearComponent.Object);
      mapping.Components.Add("Month", monthComponent.Object);
      mapping.DeserializationFunction = dict => new DateTime(Int32.Parse(dict["Year"]), Int32.Parse(dict["Month"]), 1);
      mapping.Mandatory = true;

      IDictionary<string,string> data = new Dictionary<string, string>();
      data.Add("FooYear", date.Year.ToString());

      DateTime output;
      mapping.Deserialize(data, out output, new int[0]);
    }

    #endregion
  }
}

