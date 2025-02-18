﻿using MDIP.Attributes;

namespace MDIP.Modules.Configs;

public class AdvancedConfigs : ConfigBase
{
    [ConfigCommentZh("数据刷新间隔限制（毫秒）")]
    [ConfigCommentEn("Data refresh interval limit (milliseconds)")]
    public int DataRefreshIntervalLimit { get; set; } = 123;

    [ConfigCommentZh("显示准确率计算数据")]
    [ConfigCommentEn("Output accuracy calculation data")]
    public bool OutputAccuracyCalculationData { get; set; } = false;

    [ConfigCommentZh("输出Note记录到桌面")]
    [ConfigCommentEn("Output Note records to desktop")]
    public bool OutputNoteRecordsToDesktop { get; set; } = false;
}