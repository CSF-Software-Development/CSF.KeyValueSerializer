using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Immutable type represents the options that may be passed to an instance of a generic <c>ISerializer</c>.
  /// </summary>
  public partial class SerializerOptions
  {
    #region fields



    #endregion

    #region properties



    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.KeyValueSerializer.SerializerOptions"/> class.
    /// </summary>
    protected SerializerOptions()
    {
      
    }

    #endregion

    #region static methods

    /// <summary>
    /// Creates and returns a 'builder' instance that may be used to dynamically construct new
    /// <see cref="SerializerOptions"/> instances.
    /// </summary>
    /// <returns>
    /// The builder instance.
    /// </returns>
    public static SerializerOptionsBuilder CreateBuilder()
    {
      return new SerializerOptionsBuilder();
    }

    #endregion
  }
}

