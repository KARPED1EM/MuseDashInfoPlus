﻿namespace MDIP.Managers;

public static class TextDataManager
{
	private static readonly Dictionary<string, string> _cachedValues = new();
	private static readonly Dictionary<string, string> _formattedTexts = new();

	private static void UpdateCachedValue(string key, string value)
	{
		if (_cachedValues.ContainsKey(key) && _cachedValues[key] == value)
			return;

		_cachedValues[key] = value;
		InvalidateFormattedTexts();
	}

	public static void UpdateConstants()
	{
		UpdateCachedValue("{hiScore}", GameStatsManager.SavedHighScore.ToString());
		UpdateCachedValue("{total}", ((int)GameStatsManager.AccuracyTotal).ToString());
		UpdateCachedValue("{song}", GameUtils.MusicName.TruncateByWidth(45));
		UpdateCachedValue("{diff}", GameUtils.MusicDiffStr);
		UpdateCachedValue("{level}", GameUtils.MusicLevel);
		UpdateCachedValue("{author}", GameUtils.MusicAuthor);
	}

	public static void UpdateValues()
	{
		UpdateCachedValue("{acc}", GameStatsManager.FormatAccuracy());
		UpdateCachedValue("{overview}", GameStatsManager.FormatOverview());
		UpdateCachedValue("{stats}", GameStatsManager.FormatStats());
		UpdateCachedValue("{gap}", GameStatsManager.FormatScoreGap());
		UpdateCachedValue("{hit}", ((int)GameStatsManager.AccuracyCounted).ToString());
	}

	private static void InvalidateFormattedTexts()
		=> _formattedTexts.Clear();

	public static string GetFormattedText(string originalText)
	{
		if (string.IsNullOrWhiteSpace(originalText)) return string.Empty;

		if (_formattedTexts.TryGetValue(originalText, out var formatted))
			return formatted;

		if (!ContainsAnyPlaceholder(originalText))
			return originalText;

		var result = originalText;
		foreach (var pair in _cachedValues.Where(pair => result.Contains(pair.Key)))
			result = result.Replace(pair.Key, pair.Value);

		var trimChars = new[] { '|', '\\', '-', '/', '~', '_', '=', '+' };
		result = result.Trim().Trim(trimChars).Trim();

		_formattedTexts[originalText] = result;
		return result;
	}

	private static bool ContainsAnyPlaceholder(string text)
		=> text.Contains('{') && text.Contains('}');
}