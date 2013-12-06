using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Delegate for a simple generic parsing function that parses an input as a value.
  /// </summary>
  /// <returns>
  /// <c>true</c> if the parsing was successful; <c>false</c> otherwise.
  /// </returns>
  /// <param name="input">
  /// The <c>System.String</c> input containing the value to be parsed.
  /// </param>
  /// <param name="result">
  /// The returned parsed result.
  /// </param>
  /// <remarks>
  /// <para>
  /// If the parsing was a failure (the return value of this delegate is <c>false</c>) then the value of
  /// <paramref name="result"/> is undefined.  Implementors should generally use <c>default(TParsed)</c> to create a
  /// a result value when the parsing has failed.
  /// </para>
  /// <para>
  /// The delegate must return <c>false</c> when parsing is unsuccessful.  Generally speaking, implementors of this
  /// delegate should not raise exceptions.  An exception raised by an implementor of this delegate represents a
  /// critical failure in the parsing mechanism itself, and should not be used to indicate that the input simply could
  /// not be parsed.
  /// </para>
  /// </remarks>
  public delegate bool SimpleParser<TParsed>(string input, out TParsed result);
}
