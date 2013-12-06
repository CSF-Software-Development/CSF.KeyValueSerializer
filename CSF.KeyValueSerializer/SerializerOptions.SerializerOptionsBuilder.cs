using System;

namespace CSF.KeyValueSerializer
{
  public partial class SerializerOptions
  {
    /// <summary>
    /// Builder type, used to dynamically build an instance of <see cref="SerializerOptions"/>.
    /// </summary>
    public class SerializerOptionsBuilder
    {
      #region fluent configuration

      #endregion

      #region building and copying

      /// <summary>
      /// Overwrites the options stored within the current builder instance with the given options.
      /// </summary>
      /// <returns>
      /// The current, modified options builder instance.
      /// </returns>
      /// <param name='options'>
      /// The options with which to overwrite the current instance.
      /// </param>
      /// <remarks>
      /// <para>
      /// This method is useful where you wish to use the given options as the basis for a new set of options.  This
      /// method should be used before any other methods are called; it will copy the configuration of the given options
      /// into the current instance.  Other methods may then manipulate and modify that configuration.
      /// </para>
      /// </remarks>
      public SerializerOptionsBuilder CopyFrom(SerializerOptions options)
      {
        // TODO: Write this implementation
        throw new NotImplementedException();
      }

      /// <summary>
      /// Build the options held within the current instance and return an instance of <see cref="SerializerOptions"/>.
      /// </summary>
      public SerializerOptions Build()
      {
        // TODO: Write this implementation
        throw new NotImplementedException();
      }

      #endregion

      #region constructor

      #endregion
    }
  }
}

