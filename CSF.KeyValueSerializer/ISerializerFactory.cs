using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Interface for a factory that creates generic <c>ISerializer</c> instances.
  /// </summary>
  public interface ISerializerFactory
  {
    #region methods

    /// <summary>
    /// Builds and returns a reference-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='optionsMap'>
    /// An options-selection action.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a reference type (a class) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, the <paramref name="optionsMap"/> should work additively with any form of default
    /// or pre-configured options.
    /// </para>
    /// </remarks>
    IRefTypeSerializer<T> BuildRefType<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : class;

    /// <summary>
    /// Builds and returns a reference-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a reference type (a class) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, if any default/pre-configured <see cref="SerializerOptions"/> are available, then
    /// they may be used.  If none exist then a default set of options should be created and passed to the serializer.
    /// </para>
    /// </remarks>
    IRefTypeSerializer<T> BuildRefType<T>()
      where T : class;

    /// <summary>
    /// Builds and returns a value-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='optionsMap'>
    /// An options-selection action.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a value type (a struct) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, the <paramref name="optionsMap"/> should work additively with any form of default
    /// or pre-configured options.
    /// </para>
    /// </remarks>
    IValTypeSerializer<T> BuildValType<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : struct;

    /// <summary>
    /// Builds and returns a value-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a value type (a struct) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, if any default/pre-configured <see cref="SerializerOptions"/> are available, then
    /// they may be used.  If none exist then a default set of options should be created and passed to the serializer.
    /// </para>
    /// </remarks>
    IValTypeSerializer<T> BuildValType<T>()
      where T : struct;

    /// <summary>
    /// Builds and returns a reference-type collection serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='optionsMap'>
    /// An options-selection action.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a reference type (a class) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, the <paramref name="optionsMap"/> should work additively with any form of default
    /// or pre-configured options.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    IRefTypeCollSerializer<T> BuildRefColl<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : class;

    /// <summary>
    /// Builds and returns a reference-type collection serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a reference type (a class) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, if any default/pre-configured <see cref="SerializerOptions"/> are available, then
    /// they may be used.  If none exist then a default set of options should be created and passed to the serializer.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    IRefTypeCollSerializer<T> BuildRefColl<T>()
      where T : class;

    /// <summary>
    /// Builds and returns a value-type collection serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='optionsMap'>
    /// An options-selection action.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a value type (a struct) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, the <paramref name="optionsMap"/> should work additively with any form of default
    /// or pre-configured options.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    IValTypeCollSerializer<T> BuildValColl<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : struct;

    /// <summary>
    /// Builds and returns a value-type collection serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a value type (a struct) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, if any default/pre-configured <see cref="SerializerOptions"/> are available, then
    /// they may be used.  If none exist then a default set of options should be created and passed to the serializer.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    IValTypeCollSerializer<T> BuildValColl<T>()
      where T : struct;

    /// <summary>
    /// Builds and returns a reference-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='options'>
    /// A <see cref="SerializerOptions"/> instance.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a reference type (a class) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, serializer must be constructed directly from the <paramref name="options"/>
    /// provided.  No default/pre-configured options may be applied either alongside with, or instead of the options
    /// given.
    /// </para>
    /// </remarks>
    IRefTypeSerializer<T> BuildRefType<T>(SerializerOptions options)
      where T : class;

    /// <summary>
    /// Builds and returns a value-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='options'>
    /// A <see cref="SerializerOptions"/> instance.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a value type (a struct) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, serializer must be constructed directly from the <paramref name="options"/>
    /// provided.  No default/pre-configured options may be applied either alongside with, or instead of the options
    /// given.
    /// </para>
    /// </remarks>
    IValTypeSerializer<T> BuildValType<T>(SerializerOptions options)
      where T : struct;

    /// <summary>
    /// Builds and returns a reference-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='options'>
    /// A <see cref="SerializerOptions"/> instance.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a reference type (a class) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, serializer must be constructed directly from the <paramref name="options"/>
    /// provided.  No default/pre-configured options may be applied either alongside with, or instead of the options
    /// given.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    IRefTypeCollSerializer<T> BuildRefColl<T>(SerializerOptions options)
      where T : class;

    /// <summary>
    /// Builds and returns a value-type serializer instance.
    /// </summary>
    /// <returns>
    /// The serializer.
    /// </returns>
    /// <param name='options'>
    /// A <see cref="SerializerOptions"/> instance.
    /// </param>
    /// <typeparam name='T'>
    /// The type of object that the serializer instance is to work with.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The type <typeparamref name="T"/> must be a value type (a struct) in this overload.
    /// </para>
    /// <para>
    /// Where this overload is used, serializer must be constructed directly from the <paramref name="options"/>
    /// provided.  No default/pre-configured options may be applied either alongside with, or instead of the options
    /// given.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    IValTypeCollSerializer<T> BuildValColl<T>(SerializerOptions options)
      where T : struct;

    #endregion
  }
}

