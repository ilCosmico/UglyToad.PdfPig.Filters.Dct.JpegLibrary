﻿#nullable enable

namespace UglyToad.PdfPig.Filters.Dct.JpegLibrary.Jpeg
{
    internal sealed class JpegHuffmanDecodingComponent
    {
        public int ComponentIndex { get; internal set; }
        public byte HorizontalSamplingFactor { get; internal set; }
        public byte VerticalSamplingFactor { get; internal set; }
        internal int DcPredictor { get; set; }
        internal JpegHuffmanDecodingTable? DcTable { get; set; }
        internal JpegHuffmanDecodingTable? AcTable { get; set; }
        internal JpegQuantizationTable QuantizationTable { get; set; }
        internal int HorizontalSubsamplingFactor { get; set; }
        internal int VerticalSubsamplingFactor { get; set; }
    }
}
