using System;

namespace CSF.KeyValueSerializer
{
  /// <summary>
  /// Delegate for a simple generic rendering function that renders a value to a string.
  /// </summary>
  /// <returns>
  /// <c>true</c> if the rendering was successful; <c>false</c> otherwise.
  /// </returns>
  /// <param name="input">
  /// The value to be rendered.
  /// </param>
  /// <param name="result">
  /// The <c>System.String</c> output containing the rendered value.
  /// </param>
  /// <remarks>
  /// <para>
  /// If the rendering was a failure (the return value of this delegate is <c>false</c>) then the value of
  /// <paramref name="result"/> is undefined.  Implementors should generally set the result parameter to a null
  /// reference in this scenario.
  /// </para>
  /// <para>
  /// The delegate must return <c>false</c> when rendering is unsuccessful.  Generally speaking, implementors of this
  /// delegate should not raise exceptions.  An exception raised by an implementor of this delegate represents a
  /// critical failure in the rendering mechanism itself, and should not be used to indicate that the input simply could
  /// not be rendered.
  /// </para>
  /// </remarks>
  public delegate bool SimpleRenderer<TParsed>(TParsed input, out string result);
}

