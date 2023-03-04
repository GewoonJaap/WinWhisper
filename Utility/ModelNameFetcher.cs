using Whisper.net.Ggml;

namespace Utility;

public static class ModelNameFetcher
{
    public static string GgmlTypeToString(GgmlType modelType)
    {
        var modelTypeString = modelType switch
        {
            GgmlType.Base => "base",
            GgmlType.BaseEn => "base-en",
            GgmlType.Large => "large",
            GgmlType.LargeV1 => "lage-v1",
            GgmlType.Medium => "medium",
            GgmlType.MediumEn => "medium-en",
            GgmlType.Small => "small",
            GgmlType.SmallEn => "small-en",
            GgmlType.Tiny => "tiny",
            GgmlType.TinyEn => "tiny-en",
            _ => "unknown"
        };

        return $"ggml-{modelTypeString}.bin";
    }
}