//
//  SimplePropertyMapping.cs
//
//  Author:
//       Craig Fowler <craig@craigfowler.me.uk>
//
//  Copyright (c) 2012 Craig Fowler
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Reflection;
using System.Collections.Generic;

namespace CSF.KeyValueSerializer.MappingModel
{
  /// <summary>
  /// Represents a simple value-to-property mapping.
  /// </summary>
  public class SimpleMapping<TValue> : MappingBase<TValue>, ISimpleMapping
  {
    #region fields

    private Func<string, TValue> _deserializationFunction;
    private Func<TValue, string> _serializationFunction;
    private SimpleParser<TValue> _parser;
    private SimpleRenderer<TValue> _renderer;

    #endregion

    #region ISimpleMapping implementation

    /// <summary>
    ///  Gets or sets the function used to deserialize the property value from a string. 
    /// </summary>
    /// <value>
    ///  A method body containing the deserialization function. 
    /// </value>
    public virtual Func<string, TValue> DeserializationFunction
    {
      get {
        return _deserializationFunction;
      }
      set {
        _deserializationFunction = value;
        _parser = (value != null)? this.CreateDefaultParser(value) : null;
      }
    }

    /// <summary>
    ///  Gets or sets the function used to serialize a string from the property value. 
    /// </summary>
    /// <value>
    ///  A method body containing the serialization function. 
    /// </value>
    public virtual Func<TValue, string> SerializationFunction
    {
      get {
        return _serializationFunction;
      }
      set {
        _serializationFunction = value;
        _renderer = (value != null)? this.CreateDefaultRenderer(value) : null;
      }
    }

    /// <summary>
    /// Gets or sets the parser method body.
    /// </summary>
    /// <value>
    /// The parser.
    /// </value>
    public virtual SimpleParser<TValue> Parser
    {
      get {
        return _parser;
      }
      set {
        _parser = value;
      }
    }

    /// <summary>
    /// Gets or sets the renderer method body.
    /// </summary>
    /// <value>
    /// The renderer.
    /// </value>
    public virtual SimpleRenderer<TValue> Renderer
    {
      get {
        return _renderer;
      }
      set {
        _renderer = value;
      }
    }

    /// <summary>
    ///  Validates this mapping instance. 
    /// </summary>
    public override void Validate()
    {
      base.Validate();

      if(_parser == null && _renderer == null)
      {
        throw new InvalidMappingException("Property mapping does not have either a serialization or " +
                                          "deserialization function - this is invalid (a useless mapping).");
      }
    }

    /// <summary>
    /// Gets the collection key that corresponds to the data for this property. 
    /// </summary>
    /// <param name='collectionIndices'>
    /// Zero or more indices for collection-type mappings that are passed-through when traversing from the root of the
    /// serialization hierarchy to the associated mapping.
    /// </param>
    /// <returns>
    ///  The collection key. 
    /// </returns>
    public override string GetKeyName(params int[] collectionIndices)
    {
      return this.KeyNamingPolicy.GetKeyName(collectionIndices);
    }

    /// <summary>
    ///  Deserialize the specified data as an object instance. 
    /// </summary>
    /// <returns>
    ///  A value that indicates whether deserialization was successful or not. 
    /// </returns>
    /// <param name='data'>
    ///  The dictionary/collection of string data to deserialize from. 
    /// </param>
    /// <param name='result'>
    /// The output/deserialized object instance.  If the return value is false (unsuccessful deserialization) then the
    /// output value of this parameter is undefined.
    /// </param>
    /// <param name='collectionIndices'>
    ///  A collection of integers, indicating the indices of any collection mappings passed-through during the 
    /// </param>
    public override bool Deserialize(IDictionary<string, string> data,
                                    out TValue result,
                                    params int[] collectionIndices)
    {
      bool output = false;

      result = default(TValue);

      this.Validate();

      if(this.SatisfiesFlag(data))
      {
        string keyName = this.GetKeyName(collectionIndices);
        if(data.ContainsKey(keyName))
        {
          output = _parser(data[keyName], out result);
        }
      }

      if(!output && this.Mandatory)
      {
        throw new MandatorySerializationException(this);
      }

      return output;
    }

    /// <summary>
    ///  Serialize the specified data, exposing the serialized results as an output parameter. 
    /// </summary>
    /// <param name='data'>
    ///  The object graph to serialize, the root of which should be an object instance that corresponds to the current
    /// mapping. 
    /// </param>
    /// <param name='result'>
    ///  The dictionary of string values containing the serialized data that is created from the current mapping
    /// instance. 
    /// </param>
    /// <param name='collectionIndices'>
    ///  A collection of integers, indicating the indices of any collection mappings passed-through during the
    /// serialization process. 
    /// </param>
    /// <returns>
    ///  A value that indicates whether or not the serialization was successful. 
    /// </returns>
    public override bool Serialize(TValue data, out IDictionary<string,string> result, int[] collectionIndices)
    {
      bool output = false;
      result = null;
      string serialized = null;

      if(_renderer == null)
      {
        throw new InvalidOperationException("Cannot deserialize - this simple mapping does not provide a serialization function.");
      }

      output = _renderer(data, out serialized);

      if(output)
      {
        result = new Dictionary<string, string>();
        result.Add(this.GetKeyName(collectionIndices), serialized);
        this.WriteFlag(result);
      }
      else if(this.Mandatory)
      {
        throw new MandatorySerializationException(this);
      }

      return output;
    }

    #endregion

    #region methods

    /// <summary>
    /// Creates a default renderer from and old-style serialization function.
    /// </summary>
    /// <returns>
    /// The default renderer.
    /// </returns>
    /// <param name='serializer'>
    /// Serializer.
    /// </param>
    private SimpleRenderer<TValue> CreateDefaultRenderer(Func<TValue, string> serializer)
    {
      if(serializer == null)
      {
        throw new ArgumentNullException("serializer");
      }

      return (TValue input, out string result) => {
        bool success = false;
        result = null;

        try
        {
          result = serializer(input);
          success = (result != null);
        }
        catch(Exception)
        {
          success = false;
        }

        if(!success)
        {
          result = null;
        }

        return success;
      };
    }

    /// <summary>
    /// Creates a new parser function from an old-style deserialization function.
    /// </summary>
    /// <returns>
    /// The parser.
    /// </returns>
    /// <param name='deserializer'>
    /// The old-style deserializer.
    /// </param>
    private SimpleParser<TValue> CreateDefaultParser(Func<string, TValue> deserializer)
    {
      if(deserializer == null)
      {
        throw new ArgumentNullException("deserializer");
      }

      return (string input, out TValue result) => {
        bool success = false;
        result = default(TValue);

        try
        {
          result = deserializer(input);
          success = true;
        }
        catch(Exception)
        {
          success = false;
        }

        if(!success)
        {
          result = default(TValue);
        }

        return success;
      };
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of this simple property-mapping class.
    /// </summary>
    /// <param name='property'>
    /// The property that this instance is associated with.
    /// </param>
    /// <param name='parentMapping'>
    /// The parent mapping.
    /// </param>
    public SimpleMapping(IMapping parentMapping, PropertyInfo property) : base(parentMapping, property, false)
    {
      _deserializationFunction = (strVal => (TValue) Convert.ChangeType(strVal, typeof(TValue)));
      _serializationFunction = (val => (val != null)? val.ToString() : null);

      _parser = this.CreateDefaultParser(_deserializationFunction);
      _renderer = this.CreateDefaultRenderer(_serializationFunction);
    }

    #endregion
  }
}

