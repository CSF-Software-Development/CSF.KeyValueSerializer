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
    public static ISimpleMappingHelper<TObject,string> DeserializeAsString<TObject>(this ISimpleMappingHelper<TObject,string> helper)
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
    public static ISimpleMappingHelper<TObject,int> DeserializeAsInt32<TObject>(this ISimpleMappingHelper<TObject,int> helper)
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
    public static ISimpleMappingHelper<TObject,short> DeserializeAsInt16<TObject>(this ISimpleMappingHelper<TObject,short> helper)
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
    public static ISimpleMappingHelper<TObject,long> DeserializeAsInt64<TObject>(this ISimpleMappingHelper<TObject,long> helper)
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
    public static ISimpleMappingHelper<TObject,uint> DeserializeAsUInt32<TObject>(this ISimpleMappingHelper<TObject,uint> helper)
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
    public static ISimpleMappingHelper<TObject,ushort> DeserializeAsUInt16<TObject>(this ISimpleMappingHelper<TObject,ushort> helper)
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
    public static ISimpleMappingHelper<TObject,ulong> DeserializeAsUInt64<TObject>(this ISimpleMappingHelper<TObject,ulong> helper)
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
    public static ISimpleMappingHelper<TObject,TValue> DeserializeAsEnum<TObject,TValue>(this ISimpleMappingHelper<TObject,TValue> helper)
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
    public static ISimpleMappingHelper<TObject,IIdentity<TEntity>> DeserializeAsIdentity<TObject,TEntity>(this ISimpleMappingHelper<TObject,IIdentity<TEntity>> helper)
      where TObject : class
      where TEntity : IEntity
    {
      return helper.Deserialize(val => IdentityParser.Parse<TEntity>(val));
    }

    #endregion
  }
}

