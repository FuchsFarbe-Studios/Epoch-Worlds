// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using System.Diagnostics.CodeAnalysis;

namespace EpochApp.Kit.Utils
{
    /// <summary>
    ///     Specifies a description for a property or event.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        /// <summary>
        ///     Specifies the default value for the <see cref='System.ComponentModel.DescriptionAttribute' />, which is an empty
        ///     string (""). This <see langword='static' /> field is read-only.
        /// </summary>
        public static readonly DescriptionAttribute Default = new DescriptionAttribute();

        public DescriptionAttribute() : this(string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref='System.ComponentModel.DescriptionAttribute' /> class.
        /// </summary>
        public DescriptionAttribute(String description)
        {
            DescriptionValue = description;
        }

        /// <summary>
        ///     Gets the description stored in this attribute.
        /// </summary>
        public virtual String Description => DescriptionValue;

        /// <summary>
        ///     Read/Write property that directly modifies the string stored in the description attribute. The default
        ///     implementation of the <see cref="Description" /> property simply returns this value.
        /// </summary>
        protected String DescriptionValue { get; set; }

        public override Boolean Equals([NotNullWhen(true)] Object obj)
        {
            return obj is DescriptionAttribute other && other.Description == Description;
        }

        public override Int32 GetHashCode()
        {
            return Description?.GetHashCode() ?? 0;
        }

        public override Boolean IsDefaultAttribute()
        {
            return Equals(Default);
        }
    }
}