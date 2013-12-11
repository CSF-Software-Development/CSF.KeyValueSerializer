//
//  IKeyValueSerializer.cs
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
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using CSF.KeyValueSerializer.MappingHelpers;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Interface for a key/value serializer that serializes/deserializes an instance of a class-like type.
  /// </summary>
  /// <typeparam name='TOutput'>
  /// The type of the 'root' object that this serializer instance will work with.
  /// </typeparam>
  public interface IClassKeyValueSerializer<TOutput>
    : IKeyValueSerializer<TOutput>
    where TOutput : class
  {
    /// <summary>
    /// Adds one or more of mappings to the serializer instance using a mapping method.
    /// </summary>
    /// <param name='mappings'>
    /// An action (possibly a pointer to a delegate or an anonymous method) that expresses the mappings to serialize
    /// and/or deserialize objects
    /// </param>
    void Map(Action<ClassMappingHelper<TOutput>> mappings);
  }
}

