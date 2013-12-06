using System;
using System.Collections.Generic;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Interface for a generic <c>ISerializer</c> that works with generic <c>ICollection</c>s of value types.
  /// </summary>
  public interface IValTypeCollSerializer<T>
    : ISerializer<ICollection<T>>, IValueTypeCollectionKeyValueSerializer<T>
    where T : struct
  {
  }
}

