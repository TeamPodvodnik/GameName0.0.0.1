public static class PlayerBonus
{
    public static float DamageMultiplier { get; private set; } = 1f;

    public static void AddDamageBonus(float bonus)
    {
        DamageMultiplier += bonus;
    }
}