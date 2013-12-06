using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Common generic interface for a key/value serializer.
  /// </summary>
  public interface ISerializer<T> : IKeyValueSerializer<T>
  {
  }
}

