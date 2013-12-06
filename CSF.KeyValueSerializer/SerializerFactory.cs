using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Factory type creates generic <c>ISerializer</c> instances.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Instances of this type must be constructed using one of the static <c>Create</c> methods; constructed factories
  /// contain a default instance of <see cref="SerializerOptions"/>.  These default options are provided as the factory
  /// is created, using one of the overloads of the static <c>Create</c> method.
  /// </para>
  /// <para>
  /// These default options stored within the factory are used in different ways depending upon which overload of the
  /// various 'Build' methods is used:
  /// </para>
  /// <list type="bullets">
  /// <item>
  /// When a parameterless 'Build' method is used from a <see cref="SerializerFactory"/> instance, the default options
  /// stored within that factory are used for the creation of the serializer.
  /// </item>
  /// <item>
  /// If a 'Build' method is used that takes an action, maipulating an instance of
  /// <see cref="SerializerOptions.SerializerOptionsBuilder"/> then the modifications performed in that
  /// manipulation-action are applied ADDITIVELY to the options present within the factory.
  /// </item>
  /// <item>
  /// If a 'Build' method is used with an overload that takes a <see cref="SerializerOptions"/> instance then those
  /// options OVERRIDE the options present within the factory.
  /// </item>
  /// </list>
  /// </remarks>
  public sealed class SerializerFactory : ISerializerFactory
  {
    #region fields

    private SerializerOptions _defaultOptions;

    #endregion

    #region convenience overloads

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the action performed by the
    /// <paramref name="optionsMap"/> will be ADDED TO (combined with) the default options stored within the current
    /// factory instance.
    /// </para>
    /// </remarks>
    public IRefTypeSerializer<T> BuildRefType<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : class
    {
      SerializerOptions options = this.BuildOptions(optionsMap, _defaultOptions);
      return this.BuildRefType<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the serializer will be
    /// created with the default serialization options stored within the current factory instance.
    /// </para>
    /// </remarks>
    public IRefTypeSerializer<T> BuildRefType<T>()
      where T : class
    {
      SerializerOptions options = _defaultOptions;
      return this.BuildRefType<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the action performed by the
    /// <paramref name="optionsMap"/> will be ADDED TO (combined with) the default options stored within the current
    /// factory instance.
    /// </para>
    /// </remarks>
    public IValTypeSerializer<T> BuildValType<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : struct
    {
      SerializerOptions options = this.BuildOptions(optionsMap, _defaultOptions);
      return this.BuildValType<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the serializer will be
    /// created with the default serialization options stored within the current factory instance.
    /// </para>
    /// </remarks>
    public IValTypeSerializer<T> BuildValType<T>()
      where T : struct
    {
      SerializerOptions options = _defaultOptions;
      return this.BuildValType<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the action performed by the
    /// <paramref name="optionsMap"/> will be ADDED TO (combined with) the default options stored within the current
    /// factory instance.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    public IRefTypeCollSerializer<T> BuildRefColl<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : class
    {
      SerializerOptions options = this.BuildOptions(optionsMap, _defaultOptions);
      return this.BuildRefColl<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the serializer will be
    /// created with the default serialization options stored within the current factory instance.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    public IRefTypeCollSerializer<T> BuildRefColl<T>()
      where T : class
    {
      SerializerOptions options = _defaultOptions;
      return this.BuildRefColl<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the action performed by the
    /// <paramref name="optionsMap"/> will be ADDED TO (combined with) the default options stored within the current
    /// factory instance.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    public IValTypeCollSerializer<T> BuildValColl<T>(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
      where T : struct
    {
      SerializerOptions options = this.BuildOptions(optionsMap, _defaultOptions);
      return this.BuildValColl<T>(options);
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the serializer will be
    /// created with the default serialization options stored within the current factory instance.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    public IValTypeCollSerializer<T> BuildValColl<T>()
      where T : struct
    {
      SerializerOptions options = _defaultOptions;
      return this.BuildValColl<T>(options);
    }

    #endregion

    #region building serializers

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the <paramref name="options"/>
    /// provided to the parameter REPLACE the default options stored within the current factory instance.
    /// </para>
    /// </remarks>
    public IRefTypeSerializer<T> BuildRefType<T>(SerializerOptions options)
      where T : class
    {
      // TODO: Add the ability to pass options to the constructed serialiser.
      return new ClassKeyValueSerializer<T>();
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the <paramref name="options"/>
    /// provided to the parameter REPLACE the default options stored within the current factory instance.
    /// </para>
    /// </remarks>
    public IValTypeSerializer<T> BuildValType<T>(SerializerOptions options)
      where T : struct
    {
      // TODO: Write this implementation
      throw new NotImplementedException("Value type serializers are not yet implemented.");
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the <paramref name="options"/>
    /// provided to the parameter REPLACE the default options stored within the current factory instance.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    public IRefTypeCollSerializer<T> BuildRefColl<T>(SerializerOptions options)
      where T : class
    {
      // TODO: Add the ability to pass options to the constructed serialiser.
      return new CollectionKeyValueSerializer<T>();
    }

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
    /// See the documentation for the <see cref="SerializerFactory"/>, however in summary the <paramref name="options"/>
    /// provided to the parameter REPLACE the default options stored within the current factory instance.
    /// </para>
    /// <para>
    /// The created serializer is designed for working with collections of the given generic type.  These collections
    /// must implement the generic <c>ICollection</c> for the type being serialized/deserialized.
    /// </para>
    /// </remarks>
    public IValTypeCollSerializer<T> BuildValColl<T>(SerializerOptions options)
      where T : struct
    {
      // TODO: Add the ability to pass options to the constructed serialiser.
      return new ValueTypeCollectionKeyValueSerializer<T>();
    }

    #endregion

    #region private methods

    /// <summary>
    /// Builds a new <see cref="SerializerOptions"/> instance using the given options-selection action.
    /// </summary>
    /// <returns>
    /// A <see cref="SerializerOptions"/> instance.
    /// </returns>
    /// <param name='optionsMap'>
    /// An options-selection action.
    /// </param>
    /// <param name='defaultOptions'>
    /// Optional default options to apply to the options builder first.
    /// </param>
    private SerializerOptions BuildOptions(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap,
                                           SerializerOptions defaultOptions)
    {
      if(optionsMap == null)
      {
        throw new ArgumentNullException("optionsMap");
      }

      SerializerOptions.SerializerOptionsBuilder output = SerializerOptions.CreateBuilder();

      if(defaultOptions != null)
      {
        output.CopyFrom(defaultOptions);
      }

      optionsMap(output);
      return output.Build();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.KeyValueSerializer.SerializerFactory"/> class.
    /// </summary>
    private SerializerFactory() {}

    #endregion

    #region static methods

    /// <summary>
    /// Static factory method creates a new <see cref="SerializerFactory"/> instance with default options.
    /// </summary>
    public static SerializerFactory Create()
    {
      return Create(x => {});
    }

    /// <summary>
    /// Static factory method creates a new <see cref="SerializerFactory"/> instance with the given options.
    /// </summary>
    /// <param name='optionsMap'>
    /// An options-selection action.
    /// </param>
    public static SerializerFactory Create(Action<SerializerOptions.SerializerOptionsBuilder> optionsMap)
    {
      SerializerFactory output = new SerializerFactory();
      output._defaultOptions = output.BuildOptions(optionsMap, null);
      return output;
    }

    /// <summary>
    /// Static factory method creates a new <see cref="SerializerFactory"/> instance with the given options.
    /// </summary>
    /// <param name='options'>
    /// An options instance.
    /// </param>
    public static SerializerFactory Create(SerializerOptions options)
    {
      if(options == null)
      {
        throw new ArgumentNullException("options");
      }

      SerializerFactory output = new SerializerFactory();
      output._defaultOptions = options;
      return output;
    }

    #endregion
  }
}

