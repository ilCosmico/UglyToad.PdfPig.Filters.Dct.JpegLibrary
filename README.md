# UglyToad.PdfPig.Filters.Dct.JpegLibrary

PdfPig implementation of the DCT (Jpeg) filter, based on [JpegLibrary](https://github.com/yigolden/JpegLibrary) (MIT License - See [LICENCE](https://github.com/BobLd/UglyToad.PdfPig.Filters.Dct.JpegLibrary/blob/master/UglyToad.PdfPig.Filters.Dct.JpegLibrary/Jpeg/LICENSE) and [THIRD-PARTY-NOTICES](https://github.com/BobLd/UglyToad.PdfPig.Filters.Dct.JpegLibrary/blob/master/UglyToad.PdfPig.Filters.Dct.JpegLibrary/Jpeg/THIRD-PARTY-NOTICES.md))

## Usage
```csharp
// Create your filter provider
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
        var dct = new JpegLibraryDctDecodeFilter(); // new filter
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
            { NameToken.DctDecode.Data, dct }, // new filter
            { NameToken.DctDecodeAbbreviation.Data, dct }, // new filter
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

```
