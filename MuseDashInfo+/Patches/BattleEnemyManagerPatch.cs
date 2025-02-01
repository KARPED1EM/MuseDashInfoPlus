﻿using HarmonyLib;
using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;
using MDIP.Managers;
using MDIP.Modules;
using MDIP.Utils;

namespace MDIP.Patches;

[HarmonyPatch(typeof(BattleEnemyManager), nameof(BattleEnemyManager.SetPlayResult))]
public class BattleEnemyManagerSetPlayResultPatch
{
	private static void Postfix(int idx, byte result, bool isMulStart = false, bool isMulEnd = false, bool isLeft = false)
	{
		var note = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
		var type = (NoteType)note.noteData.type;

		if (Helper.OutputNoteRecordsToDesktop)
			NoteRecordManager.AddRecord(idx, "SetPlayResult", $"result:{result}");

		switch (result)
		{
			case 4 when type == NoteType.Block:
				GameStatsManager.CountNote(idx, CountNoteAction.Block);
				break;
			case 1 when type == NoteType.Long:
				GameStatsManager.CountNote(idx, CountNoteAction.MissLong, -1, note.isLongPressStart);
				break;
		}

		if (type == NoteType.Mul && result is 3 or 4)
			GameStatsManager.Mashing(note);

		if (type.IsRegularNote() && !note.isLongPressing && type != NoteType.Mul)
		{
			GameStatsManager.CheckMashing();
			TextObjManager.UpdateAllText();
		}
	}
}

// Reference: https://github.com/flustix/AccDisplay/blob/main/AccDisplay/Patches/MissPatches.cs