#nullable enable

using System.Runtime.CompilerServices;

namespace UglyToad.PdfPig.Filters.Dct.JpegLibrary.Jpeg
{
    /// <summary>
    /// Represents a 8x8 spatial block.
    /// </summary>
#if NET8_0_OR_GREATER
    [InlineArray(64)]
    internal struct JpegBlock8x8
    {
        private short _firstElement;
        
#else
    public unsafe struct JpegBlock8x8
    {
        private fixed short _data[64];
#endif

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element value.</returns>
        public short this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= 64)
                {
                    ThrowArgumentOutOfRangeException(nameof(index));
                }
                ref short selfRef = ref Unsafe.As<JpegBlock8x8, short>(ref this);
                return Unsafe.Add(ref selfRef, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if ((uint)index >= 64)
                {
                    ThrowArgumentOutOfRangeException(nameof(index));
                }
                ref short selfRef = ref Unsafe.As<JpegBlock8x8, short>(ref this);
                Unsafe.Add(ref selfRef, index) = value;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified position.
        /// </summary>
        /// <param name="x">The row index of the block.</param>
        /// <param name="y">The column index of the block.</param>
        /// <returns>The element value.</returns>
        public short this[int x, int y]
        {
            get => this[y * 8 + x];
            set => this[y * 8 + x] = value;
        }

        private static void ThrowArgumentOutOfRangeException(string paramName)
        {
            throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
