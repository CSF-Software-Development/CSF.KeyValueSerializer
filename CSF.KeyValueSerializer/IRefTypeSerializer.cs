using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Interface for a generic <c>ISerializer</c> that works with reference types.
  /// </summary>
  public interface IRefTypeSerializer<T>
    : ISerializer<T>, IClassKeyValueSerializer<T>
    where T : class
  {
  }
}

