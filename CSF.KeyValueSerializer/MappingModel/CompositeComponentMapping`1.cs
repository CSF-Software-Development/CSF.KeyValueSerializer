//
//  CompositePropertyComponentMapping.cs
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

namespace CSF.KeyValueSerializer.MappingModel
{
  /// <summary>
  /// Implementation of a mapping for a composite component.
  /// </summary>
  public class CompositeComponentMapping<TValue>
  {
    #region fields

    private CompositeMapping<TValue> _parentMapping;
    private object _componentIdentifier;
    private Func<TValue, string> _serializationFunction;
    private SimpleRenderer<TValue> _renderer;

    #endregion

    #region properties

    /// <summary>
    ///  Gets the 'parent' composite mapping. 
    /// </summary>
    /// <value>
    ///  The parent mapping. 
    /// </value>
    public virtual CompositeMapping<TValue> ParentMapping
    {
      get {
        return _parentMapping;
      }
      private set {
        if(value == null)
        {
          throw new ArgumentNullException("value");
        }

        _parentMapping = value;
      }
    }

    /// <summary>
    ///  Gets the component identifier for this mapping. 
    /// </summary>
    /// <value>
    ///  The component identifier. 
    /// </value>
    public virtual object ComponentIdentifier
    {
      get {
        return _componentIdentifier;
      }
      private set {
        if(value == null)
        {
          throw new ArgumentNullException("value");
        }

        _componentIdentifier = value;
      }
    }

    /// <summary>
    /// Gets or sets the function used to serialize a single string value (corresponding to this component) from the
    /// property value. 
    /// </summary>
    /// <value>
    /// A method body containing the serialization function. 
    /// </value>
    public virtual Func<TValue, string> SerializationFunction
    {
      get {
        return _serializationFunction;
      }
      set {
        _serializationFunction = value;
        if(value != null)
        {
          _renderer = this.CreateDefaultRenderer(value);
        }
        else
        {
          _renderer = null;
        }
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

    #endregion

    #region methpds

    /// <summary>
    /// Gets the collection key that corresponds to the data for this component. 
    /// </summary>
    /// <param name='collectionIndices'>
    /// Zero or more indices for collection-type mappings that are passed-through when traversing from the root of the
    /// serialization hierarchy to the associated mapping.
    /// </param>
    /// <returns>
    ///  The collection key. 
    /// </returns>
    public virtual string GetKeyName(params int[] collectionIndices)
    {
      return String.Concat(this.ParentMapping.KeyNamingPolicy.GetKeyName(collectionIndices),
                           this.ComponentIdentifier.ToString());
    }

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
          success = true;
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

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the component mapping type.
    /// </summary>
    /// <param name='parentMapping'>
    /// Parent mapping.
    /// </param>
    /// <param name='componentIdentifier'>
    /// Component identifier.
    /// </param>
    public CompositeComponentMapping(CompositeMapping<TValue> parentMapping, object componentIdentifier)
    {
      _serializationFunction = null;
      _renderer = null;
      _parentMapping = parentMapping;
      _componentIdentifier = componentIdentifier;
    }

    #endregion
  }
}

