using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Interface for a generic <c>ISerializer</c> that works with value types.
  /// </summary>
  public interface IValTypeSerializer<T>
    : ISerializer<T>
    where T : struct
  {
  }
}

