using System;
using System.Collections.Generic;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Interface for a generic <c>ISerializer</c> that works with generic <c>ICollection</c>s of reference types.
  /// </summary>
  public interface IRefTypeCollSerializer<T>
    : ISerializer<ICollection<T>>, ICollectionKeyValueSerializer<T>
    where T : class
  {
  }
}

