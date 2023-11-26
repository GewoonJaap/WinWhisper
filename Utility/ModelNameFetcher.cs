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
            GgmlType.LargeV2 => "large-v2",
            GgmlType.LargeV1 => "large-v1",
            GgmlType.LargeV3 => "large-v3",
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