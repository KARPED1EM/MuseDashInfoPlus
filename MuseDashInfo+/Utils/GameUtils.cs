﻿using Il2CppAssets.Scripts.Database;

namespace MDIP.Utils;

public static class GameUtils
{
    public static bool IsOthersMode { get; set; }
    public static bool IsSpellMode { get; set; }
    public static bool IsWisadelMode { get; set; }

    public static string MusicName => GlobalDataBase.s_DbBattleStage.selectedMusicName;
    public static int MusicDiff => GlobalDataBase.s_DbBattleStage.selectedDifficulty;
    public static string MusicLevel => GlobalDataBase.s_DbBattleStage.selectedMusicInfo.GetMusicLevelStringByDiff(MusicDiff);
    public static string MusicAuthor => GlobalDataBase.s_DbBattleStage.selectedMusicInfo.author;
    public static string ChartUniqueID => $"{GlobalDataBase.s_DbMusicTag.CurMusicInfo().uid}-{MusicDiff}";

    public static string MusicDiffStr => (MusicDiff switch
    {
        1 => Configs.Main.TextDiff1,
        2 => Configs.Main.TextDiff2,
        3 => Configs.Main.TextDiff3,
        4 => Configs.Main.TextDiff4,
        5 => Configs.Main.TextDiff5,
        _ => "Unknown"
    }).ToUpper();

    public static void Reset()
    {
        IsOthersMode = false;
        IsSpellMode = false;
        IsWisadelMode = false;
    }
}