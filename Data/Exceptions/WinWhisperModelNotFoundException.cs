namespace Data.Exceptions;

public class WinWhisperModelNotFoundException(string modelName) : Exception($"WinWhisper Model '{modelName}' not found.");
