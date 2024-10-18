using UglyToad.PdfPig.Tokens;

namespace UglyToad.PdfPig.Filters.Dct.JpegLibrary.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var parsingOption = new ParsingOptions()
            {
                UseLenientParsing = true,
                SkipMissingFonts = true,
                FilterProvider = MyFilterProvider.Instance
            };

            using (var doc = PdfDocument.Open("test.pdf", parsingOption))
            {
                int i = 0;
                foreach (var page in doc.GetPages())
                {
                    foreach (var pdfImage in page.GetImages())
                    {
                        Assert.True(pdfImage.TryGetPng(out var bytes));

                        File.WriteAllBytes($"image_{i++}.jpeg", bytes);
                    }
                }
            }
        }

        [Fact]
        public void Test2()
        {
            var parsingOption = new ParsingOptions()
            {
                UseLenientParsing = true,
                SkipMissingFonts = true,
                FilterProvider = MyFilterProvider.Instance
            };

            using (var doc = PdfDocument.Open("68-1990-01_A.pdf", parsingOption))
            {
                int i = 0;
                foreach (var page in doc.GetPages())
                {
                    foreach (var pdfImage in page.GetImages())
                    {
                        if (pdfImage.ImageDictionary.TryGet(NameToken.Filter, out NameToken filter))
                        {
                            if (!filter.Data.ToUpper().Contains("DCT"))
                            {
                                continue;
                            }
                        }

                        Assert.True(pdfImage.TryGetPng(out var bytes));

                        File.WriteAllBytes($"image_{i++}.jpeg", bytes);
                    }
                }
            }
        }

        public sealed class MyFilterProvider : BaseFilterProvider
        {
            /// <summary>
            /// The single instance of this provider.
            /// </summary>
            public static readonly IFilterProvider Instance = new MyFilterProvider();

            /// <inheritdoc/>
            private MyFilterProvider() : base(GetDictionary())
            {
            }

            private static Dictionary<string, IFilter> GetDictionary()
            {
                var ascii85 = new Ascii85Filter();
                var asciiHex = new AsciiHexDecodeFilter();
                var ccitt = new CcittFaxDecodeFilter();
                var dct = new JpegLibraryDctDecodeFilter();
                var flate = new FlateFilter();
                var jbig2 = new Jbig2DecodeFilter();
                var jpx = new JpxDecodeFilter();
                var runLength = new RunLengthFilter();
                var lzw = new LzwFilter();

                return new Dictionary<string, IFilter>
                {
                    { NameToken.Ascii85Decode.Data, ascii85 },
                    { NameToken.Ascii85DecodeAbbreviation.Data, ascii85 },
                    { NameToken.AsciiHexDecode.Data, asciiHex },
                    { NameToken.AsciiHexDecodeAbbreviation.Data, asciiHex },
                    { NameToken.CcittfaxDecode.Data, ccitt },
                    { NameToken.CcittfaxDecodeAbbreviation.Data, ccitt },
                    { NameToken.DctDecode.Data, dct },
                    { NameToken.DctDecodeAbbreviation.Data, dct },
                    { NameToken.FlateDecode.Data, flate },
                    { NameToken.FlateDecodeAbbreviation.Data, flate },
                    { NameToken.Jbig2Decode.Data, jbig2 },
                    { NameToken.JpxDecode.Data, jpx },
                    { NameToken.RunLengthDecode.Data, runLength },
                    { NameToken.RunLengthDecodeAbbreviation.Data, runLength },
                    { NameToken.LzwDecode.Data, lzw },
                    { NameToken.LzwDecodeAbbreviation.Data, lzw }
                };
            }
        }
    }
}