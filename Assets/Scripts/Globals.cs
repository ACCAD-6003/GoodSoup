using Sirenix.OdinInspector;
using System.Collections.Generic;

public class Globals {
    public const float HEAT_THRESHOLD = 150;
    public const float PREFERABLE_HEAT = 100;
    public const float SECONDS_BETWEEN_EMAIL_REPLIES = 5f;
    public const float SECONDS_AMBER_NEEDS_TO_SHOWER_IN_WARM_WATER = 6.5f;
    public const float FLUSH_SHOWER_TEMP_IMPACT = -30f;
    public const float AMBER_GETS_COLD_TEMP = 70f;
    public const float TEMP_INCREASE_MODIFIER = 8 * 3f;
    public const float TEMP_DECREASE_MODIFIER = 2f;
    public const float AMBER_PREFERABLE_SHOWER_TEMP = 100f;
    public static Dictionary<Ending, int> UnlockedEndings = new();
    public static List<Ending> EndingHintChecked = new();
    public static Ending LastEnding;
    public static bool FirstTitleScreen = true;
}