using System;
using CSF.KeyValueSerializer.MappingHelpers;
using CSF.Entities;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Extension methods for mapping helpers that implement the simple mapping helper type.
  /// </summary>
  public static class SimpleMappingHelperExtensions
  {
    #region extension methods

    /// <summary>
    /// Deserializes the value as a plain string.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,string> DeserializeAsString<TObject>(this SimpleMappingHelper<TObject,string> helper)
      where TObject : class
    {
      return helper.Deserialize(val => val);
    }

    /// <summary>
    /// Deserializes the value as a 32-bit integer.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,int> DeserializeAsInt32<TObject>(this SimpleMappingHelper<TObject,int> helper)
      where TObject : class
    {
      return helper.Deserialize(val => Int32.Parse(val));
    }

    /// <summary>
    /// Deserializes the value as a 16-bit integer.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,short> DeserializeAsInt16<TObject>(this SimpleMappingHelper<TObject,short> helper)
      where TObject : class
    {
      return helper.Deserialize(val => Int16.Parse(val));
    }

    /// <summary>
    /// Deserializes the value as a 64-bit integer.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,long> DeserializeAsInt64<TObject>(this SimpleMappingHelper<TObject,long> helper)
      where TObject : class
    {
      return helper.Deserialize(val => Int64.Parse(val));
    }

    /// <summary>
    /// Deserializes the value as an unsigned 32-bit integer.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,uint> DeserializeAsUInt32<TObject>(this SimpleMappingHelper<TObject,uint> helper)
      where TObject : class
    {
      return helper.Deserialize(val => UInt32.Parse(val));
    }

    /// <summary>
    /// Deserializes the value as an unsigned 16-bit integer.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,ushort> DeserializeAsUInt16<TObject>(this SimpleMappingHelper<TObject,ushort> helper)
      where TObject : class
    {
      return helper.Deserialize(val => UInt16.Parse(val));
    }

    /// <summary>
    /// Deserializes the value as an unsigned 64-bit integer.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,ulong> DeserializeAsUInt64<TObject>(this SimpleMappingHelper<TObject,ulong> helper)
      where TObject : class
    {
      return helper.Deserialize(val => UInt64.Parse(val));
    }

    /// <summary>
    /// Deserializes the value as an enumeration constant.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    /// <typeparam name='TValue'>
    /// The type of enumeration to deserialize.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,TValue> DeserializeAsEnum<TObject,TValue>(this SimpleMappingHelper<TObject,TValue> helper)
      where TObject : class
      where TValue : struct
    {
      return helper.Deserialize(val => val.ParseAs<TValue>());
    }

    /// <summary>
    /// Deserializes the value as an entity identity.
    /// </summary>
    /// <returns>
    /// The mapping helper.
    /// </returns>
    /// <param name='helper'>
    /// A suitable mapping helper instance.
    /// </param>
    /// <typeparam name='TObject'>
    /// The type of the reference type object being deserialized.
    /// </typeparam>
    /// <typeparam name='TEntity'>
    /// The type of entity for which an identity is desired.
    /// </typeparam>
    public static SimpleMappingHelper<TObject,IIdentity<TEntity>> DeserializeAsIdentity<TObject,TEntity>(this SimpleMappingHelper<TObject,IIdentity<TEntity>> helper)
      where TObject : class
      where TEntity : IEntity
    {
      return helper.Deserialize(val => IdentityParser.Parse<TEntity>(val));
    }

    #endregion
  }
}

