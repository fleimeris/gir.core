﻿using System;

namespace GObject
{
    /// <summary>
    /// Define the value of GProperty which can be used at the construct time.
    /// </summary>
    public sealed class ConstructArgument : IDisposable
    {
        #region Properties

        /// <summary>
        /// The GProperty name to set at the construct time.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The value of the property.
        /// </summary>
        public Value? Value { get; }

        #endregion

        #region Constructors

        private ConstructArgument(string name, object? value)
        {
            Name = name;
            Value = value is null ? null : Value.From(value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new construct time parameter, using the given <paramref name="property"/>
        /// with the given <paramref name="value"/>
        /// </summary>
        /// <param name="property">The property to define at the construct time.</param>
        /// <param name="value">The property value.</param>
        /// <typeparam name="T">The type of the value to set in the property.</typeparam>
        /// <returns>
        /// A new instance of <see cref="ConstructArgument"/>, which describe the
        /// property-value pair to use at construct time.
        /// </returns>
        public static ConstructArgument With<T>(Property<T> property, T value) => new ConstructArgument(property.Name, value);

        public static ConstructArgument With(string propertyName, object? value) => new ConstructArgument(propertyName, value);

        public void Dispose()
        {
            Value?.Dispose();
        }

        #endregion
    }
}
