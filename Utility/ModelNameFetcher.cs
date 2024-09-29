using Data.Enum;
using Data.Exceptions;
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
            _ => throw new WinWhisperModelNotFoundException(modelType.ToString())
        };

        return $"ggml-{modelTypeString}.bin";
    }

    public static WinWhisperModelType StringToModelType(string modelType)
    {
        return modelType switch
        {
            "base" => WinWhisperModelType.Base,
            "base-en" => WinWhisperModelType.BaseEn,
            "large-v2" => WinWhisperModelType.LargeV2,
            "large-v1" => WinWhisperModelType.LargeV1,
            "large-v3" => WinWhisperModelType.LargeV3,
            "medium" => WinWhisperModelType.Medium,
            "medium-en" => WinWhisperModelType.MediumEn,
            "small" => WinWhisperModelType.Small,
            "small-en" => WinWhisperModelType.SmallEn,
            "tiny" => WinWhisperModelType.Tiny,
            "tiny-en" => WinWhisperModelType.TinyEn,
            _ => throw new WinWhisperModelNotFoundException(modelType)
        };
    }

    public static GgmlType ModelTypeToGgmlType(WinWhisperModelType modelType)
    {
        return modelType switch
        {
            WinWhisperModelType.Base => GgmlType.Base,
            WinWhisperModelType.BaseEn => GgmlType.BaseEn,
            WinWhisperModelType.LargeV2 => GgmlType.LargeV2,
            WinWhisperModelType.LargeV1 => GgmlType.LargeV1,
            WinWhisperModelType.LargeV3 => GgmlType.LargeV3,
            WinWhisperModelType.Medium => GgmlType.Medium,
            WinWhisperModelType.MediumEn => GgmlType.MediumEn,
            WinWhisperModelType.Small => GgmlType.Small,
            WinWhisperModelType.SmallEn => GgmlType.SmallEn,
            WinWhisperModelType.Tiny => GgmlType.Tiny,
            WinWhisperModelType.TinyEn => GgmlType.TinyEn,
            _ => throw new WinWhisperModelNotFoundException(modelType.ToString())
        };
    }
}